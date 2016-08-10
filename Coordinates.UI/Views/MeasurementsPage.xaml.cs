using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Coordinates.ExternalDevices.Devices;

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
    }
}
