using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Coordinates.UI.Views
{
    public sealed partial class MeasurementsPage : Page
    {
        public MeasurementsPage()
        {
            InitializeComponent();
            //NavigationCacheMode = NavigationCacheMode.Disabled;
        }

        private void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            Pivot1.SelectedIndex = 1;
        }
    }
}
