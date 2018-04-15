using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DDCCI
{
    using static dxva2;
    using static User32;

    public class DisplayService : IDisplayService
    {
        public IEnumerable<MonitorInfo> GetMonitors()
        {
            var monitors = new List<MonitorInfo>();

            bool monitorEnumProc(IntPtr hDesktop, IntPtr hdc, ref User32.Rect prect, int d)
            {
                monitors.Add(new MonitorInfo()
                {
                    Desktop = hDesktop,
                    Handle = hdc
                });

                return true;
            }

            if (!EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, monitorEnumProc, 0))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            foreach (var monitor in monitors)
            {
                var info = new MonitorInfoEx();
                GetMonitorInfo(new HandleRef(null, monitor.Desktop), info);
                monitor.Name = new string(info.szDevice).TrimEnd('\0');
            }

            return monitors;
        }

        private static PHYSICAL_MONITOR[] GetPhysicalMonitors(IntPtr hMonitor)
        {
            uint dwNumberOfPhysicalMonitors;

            if (!GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, out dwNumberOfPhysicalMonitors))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            PHYSICAL_MONITOR[] physicalMonitorArray = new PHYSICAL_MONITOR[dwNumberOfPhysicalMonitors];
            if (!GetPhysicalMonitorsFromHMONITOR(hMonitor, dwNumberOfPhysicalMonitors, physicalMonitorArray))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return physicalMonitorArray;
        }

        public string GetCapabilities(MonitorInfo monitorInfo)
        {
            uint length = 0;
            var monitor = GetPhysicalMonitors(monitorInfo.Desktop);

            if (!GetCapabilitiesStringLength(monitor[0].hPhysicalMonitor, out length))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error() + "\r\nmonitor" + monitor[0].hPhysicalMonitor + "\r\n\r\nscreen" + monitorInfo.Desktop);

            }

            var sb = new StringBuilder((int)length);
            if (!CapabilitiesRequestAndCapabilitiesReply(monitor[0].hPhysicalMonitor, sb, (uint)sb.Capacity))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return sb.ToString();
        }

        public void SetVCPCapability(MonitorInfo monitorInfo, char code, int val)
        {
            var monitor = GetPhysicalMonitors(monitorInfo.Desktop);
            SetVCPFeature(monitor[0].hPhysicalMonitor, code, (uint)val);
        }

        public IEnumerable<VCPCapability> GetVCPCapabilities(MonitorInfo monitorInfo)
        {
            var physicalMonitor = GetPhysicalMonitors(monitorInfo.Desktop);

            ITokenizer tokenizer = new CapabilitiesTokenizer();
            IParser parser = new CapabilitiesParser();
            INodeFormatter formatter = new NodeFormatter();

            var capabilities = GetCapabilities(monitorInfo);
            var tokens = tokenizer.GetTokens(capabilities);
            var node = parser.Parse(tokens);

            var vcpNode = node.Nodes.RecursiveSelect(n => n.Nodes)
                .Single(n => n.Value == "vcp");

            foreach (var capabilityNode in vcpNode.Nodes.Where(c => c.Nodes == null))
            {
                var optCode = (char)int.Parse(capabilityNode.Value, System.Globalization.NumberStyles.HexNumber);
                var formattedName = formatter.FormatNode(capabilityNode);

                if (formattedName == null)
                    continue;

                var name = formattedName + $" (0x{capabilityNode.Value})";

                uint currentValue = 0, maxValue = 0;
                if (!GetVCPFeatureAndVCPFeatureReply(physicalMonitor[0].hPhysicalMonitor, optCode, IntPtr.Zero, out currentValue, out maxValue))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                if (maxValue == 0)
                    continue;

                yield return new VCPCapability()
                {
                    Name = name,
                    OptCode = optCode,
                    Value = currentValue,
                    MaxValue = maxValue
                };
            }
        }
    }
}
