using System.Collections.Generic;

namespace DDCCI
{
    public interface IDisplayService
    {
        IEnumerable<MonitorInfo> GetMonitors();

        string GetCapabilities(MonitorInfo monitorInfo);

        IEnumerable<VCPCapability> GetVCPCapabilities(MonitorInfo monitorInfo);

        void SetVCPCapability(MonitorInfo monitorInfo, char code, int val);
    }
}
