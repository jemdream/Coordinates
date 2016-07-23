using Windows.UI.Xaml;
using System.Threading.Tasks;
using Coordinates.UI.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Coordinates.Services;
using Coordinates.Services.Connection;
using Coordinates.UI.Services.ServiceLocator;
using Coordinates.UI.ViewModels;
using Coordinates.UI.ViewModels.Interfaces;
using Coordinates.UI.Views;
using Template10.Controls;
using Microsoft.Practices.Unity;
using Prism.Events;
using Template10.Services.NavigationService;

namespace Coordinates.UI
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    sealed partial class App : Template10.Common.BootStrapper
    {
        private readonly IUnityContainer _myContainer;

        public App()
        {
            InitializeComponent();
            _myContainer = SetupContainer();
            SplashFactory = (e) => new Splash(e);

            // SettingsService setup
            var settings = _myContainer.Resolve<SettingsService>();
            RequestedTheme = settings.AppTheme;
            CacheMaxDuration = settings.CacheMaxDuration;
            ShowShellBackButton = settings.UseShellBackButton;
        }

        public override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            if (!(Window.Current.Content is ModalDialog))
            {
                // create a new frame and register it
                var navigationService = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
                _myContainer.RegisterInstance(typeof(INavigationService), navigationService);

                // create modal root
                Window.Current.Content = new ModalDialog
                {
                    DisableBackButtonWhenModal = true,
                    Content = _myContainer.Resolve<Shell>(),
                    ModalContent = _myContainer.Resolve<Busy>()
                };
            }

            // Entering fullscreen mode
            var view = ApplicationView.GetForCurrentView();
            if (!ApplicationView.GetForCurrentView().IsFullScreenMode)
                if (view.TryEnterFullScreenMode())
                    ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;

            //comment out 2 lines below to run in fullscreen mode
            view.ExitFullScreenMode();
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
            // The SizeChanged event will be raised when the exit from full-screen mode is complete.

            await Task.CompletedTask;
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // long-running startup tasks go here
            SetupServiceLocator();
            NavigationService.Navigate(typeof(MainPage));

            await Task.CompletedTask;
        }

        /// <summary>
        /// Container builder
        /// </summary>
        /// <returns>New container instance with types registered</returns>
        private static IUnityContainer SetupContainer()
        {
            var container = new UnityContainer();

            // _ new ContainerControlledLifetimeManager() means it's singleton (the same instance each resolve)

            // Registering Services
            container.RegisterType<ISettingsService, SettingsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IConnectionService, SerialPortConnectionService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());

            // Registering ViewModels
            container.RegisterType<IMainPageViewModel, MainPageViewModel>();
            container.RegisterType<IDetailPageViewModel, DetailPageViewModel>();
            container.RegisterType<ISettingsPartViewModel, SettingsPartViewModel>();
            container.RegisterType<IAboutPartViewModel, AboutPartViewModel>();
            container.RegisterType<ISettingsPageViewModel, SettingsPageViewModel>();
            container.RegisterType<IMeasurementsPageViewModel, MeasurementsPageViewModel>();
            container.RegisterType<ICodingPlaygroundViewModel, CodingPlaygroundViewModel>();
            container.RegisterType<IVisualisationPageViewModel, VisualisationPageViewModel>();
            container.RegisterType<ICoordsOriginPartViewModel, CoordsOriginPartViewModel>();
            container.RegisterType<ICoordsComputationPartViewModel, CoordsComputationPartViewModel>();
            container.RegisterType<IConnectionSetupViewModel, ConnectionSetupViewModel>(new ContainerControlledLifetimeManager());

            // Registering Views 
            container.RegisterType(typeof(Shell));
            container.RegisterType(typeof(Busy));

            return container;
        }

        /// <summary>
        /// Injecting Unity cointainer into Service Locator,
        /// that is saved in App.xaml as Static Resource.
        /// </summary>
        private void SetupServiceLocator()
        {
            var serviceLocator = Application.Current.Resources["Locator"] as ViewModelLocator;
            serviceLocator?.SetupContainer(_myContainer);
        }
    }
}