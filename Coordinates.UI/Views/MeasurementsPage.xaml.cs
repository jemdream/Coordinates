using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Coordinates.ExternalDevices.Devices;
using Coordinates.UI.ViewModels.MeasurementFlow;

namespace Coordinates.UI.Views
{
    public sealed partial class MeasurementsPage : Page
    {
        public MeasurementsPage()
        {
            InitializeComponent();
            //NavigationCacheMode = NavigationCacheMode.Disabled;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ((MockDeviceService)((Button)sender).DataContext).PushMockedValues();
        }

        /// <summary>
        /// Supporting ViewModelBase.OnNavigated for Pivot Items
        /// </summary>
        private void Pivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newViewModel = e.AddedItems.FirstOrDefault() as MeasurementViewModelBase;
            var oldViewModel = e.RemovedItems.FirstOrDefault() as MeasurementViewModelBase;
            
            BackButton.Command = newViewModel?.GoBackCommand;
            NextButton.Command = newViewModel?.GoNextCommand;

            newViewModel?.OnNavigatedToAsync(null, NavigationMode.Refresh, null);
            oldViewModel?.OnNavigatedFromAsync(null, false);
        }
    }
}
