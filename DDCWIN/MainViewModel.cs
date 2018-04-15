using DDCCI;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace DDCWIN
{
    [Export]
    public class MainViewModel : IMainViewModel, INotifyPropertyChanged
    {
        [Import]
        public IDisplayService DisplayService { get; private set; }

        #region Properties
        private ObservableCollection<MonitorViewModel> monitors = new ObservableCollection<MonitorViewModel>();
        public ObservableCollection<MonitorViewModel> Monitors
        {
            get
            {
                return monitors;
            }
            set
            {
                if (monitors != value)
                {
                    monitors = value;
                    RaisePropertyChanged(nameof(Monitors));
                }
            }

        }

        private MonitorViewModel selectedMonitor;
        public MonitorViewModel SelectedMonitor
        {
            get
            {
                return selectedMonitor;
            }
            set
            {
                if (selectedMonitor != value)
                {
                    selectedMonitor = value;
                    RaisePropertyChanged(nameof(SelectedMonitor));
                    UpdateSelectedMonitor();
                }
            }
        }

        #endregion

        public void Load()
        {
            //ITokenizer tokenizer = new CapabilitiesTokenizer();
            //IParser parser = new CapabilitiesParser();

            var results = DisplayService.GetMonitors();
            foreach (var monitor in results)
            {

                //var capabilities = DisplayService.GetCapabilities(monitor);
                //var tokens = tokenizer.GetTokens(capabilities);
                //var node = parser.Parse(tokens);

                //var vcpNode = node.Nodes.RecursiveSelect(n => n.Nodes)
                // .Single(n => n.Value == "vcp");

                //var hasBrightness = vcpNode.Nodes.FirstOrDefault(n => n.Value == "10");

                var vcpCapabilities = DisplayService.GetVCPCapabilities(monitor);
                monitors.Add(new MonitorViewModel
                {
                    MonitorInfo = monitor,
                    VCPCapabilities = vcpCapabilities
                //Capabilities = capabilities,
                //HasBrightness = hasBrightness != null
            });
        }
    }

    private void UpdateSelectedMonitor()
    {
        //IsBrightnessSelected = selectedMonitor.HasBrightness;
    }

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    public void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    #endregion
}
}
