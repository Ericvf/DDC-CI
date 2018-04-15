using DDCCI;
using System.Collections.Generic;

namespace DDCWIN
{
    public class MonitorViewModel
    {
        public MonitorInfo MonitorInfo { get; set; }

        public string Capabilities { get; set; }

        public IEnumerable<VCPCapability> VCPCapabilities { get; set; }
    }
}
