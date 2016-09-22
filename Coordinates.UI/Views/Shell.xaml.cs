using Windows.UI.Xaml;
using Template10.Controls;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Controls;
using Coordinates.UI.ViewModels;

namespace Coordinates.UI.Views
{
    public sealed partial class Shell : Page
    {

        public Shell()
        {
            Instance = this;
            InitializeComponent();
        }

        public Shell(INavigationService navigationService, IConnectionSetupViewModel connectionSetupViewModel) : this()
        {
            SetNavigationService(navigationService);
            ConnectionSetupViewModel = connectionSetupViewModel;
        }

        public static Shell Instance { get; set; }
        public static HamburgerMenu HamburgerMenu => Instance.MyHamburgerMenu;
        public IConnectionSetupViewModel ConnectionSetupViewModel { get; set; }

        public void SetNavigationService(INavigationService navigationService)
        {
            MyHamburgerMenu.NavigationService = navigationService;
        }

        private void ConnectionBar_OnLoaded(object sender, RoutedEventArgs e)
        {
            (sender as CommandBar).DataContext = ConnectionSetupViewModel;
        }

        private void Instrukcje_Tapped(object sender, RoutedEventArgs e)
        {
            var isMaszynaButtonVisible = MaszynaButton.Visibility == Visibility.Visible;
            
            MaszynaButton.Visibility = MetrologiaButton.Visibility =
                isMaszynaButtonVisible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void MyHamburgerMenu_OnLoaded(object sender, RoutedEventArgs e)
        {
            MyHamburgerMenu.NavigationService.Navigate(typeof(MeasurementsPage));
        }
    }
}