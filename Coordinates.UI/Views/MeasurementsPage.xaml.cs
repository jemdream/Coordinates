using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Coordinates.ExternalDevices.Devices;
using Template10.Mvvm;

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
            var newView = (e.AddedItems.FirstOrDefault() as PivotItem)?.DataContext as ViewModelBase;
            var oldView = (e.RemovedItems.FirstOrDefault() as PivotItem)?.DataContext as ViewModelBase;

            newView?.OnNavigatedToAsync(null, NavigationMode.Refresh, null);
            oldView?.OnNavigatedFromAsync(null, false);
        }
    }
}
