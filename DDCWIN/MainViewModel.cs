using DDCCI;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;

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
                }
            }
        }

        #endregion

        public void Load()
        {
            var displayMonitors = DisplayService.GetMonitors();
            foreach (var monitor in displayMonitors)
            {
                var vcpCapabilities = DisplayService.GetVCPCapabilities(monitor);
                var capabilities = DisplayService.GetCapabilities(monitor);
                monitors.Add(new MonitorViewModel
                {
                    MonitorInfo = monitor,
                    VCPCapabilities = vcpCapabilities,
                    Capabilities = capabilities,
                });
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }

    public interface IMainViewModel
    {
    }
}
