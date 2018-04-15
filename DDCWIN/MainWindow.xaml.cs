using DDCCI;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace DDCWIN
{
    public partial class MainWindow : Window
    {
        [Import]
        public IDisplayService DisplayService { get; private set; }

        [Import]
        public MainViewModel MainViewModel { get; private set; }

        public MainWindow()
        {
            App.InitializeComposition(this);

            InitializeComponent();

            DataContext = MainViewModel;

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainViewModel.Load();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            MainViewModel.SelectedMonitor = (sender as RadioButton).DataContext as MonitorViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DisplayService.SetVCPCapability(MainViewModel.SelectedMonitor.MonitorInfo, (char)0x60, 11);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            var dataContext = slider?.DataContext;
            if (dataContext is VCPCapability vcpCapability)
            {
                DisplayService.SetVCPCapability(MainViewModel.SelectedMonitor.MonitorInfo, vcpCapability.OptCode, (int)slider.Value);
            }
        }
    }
}
