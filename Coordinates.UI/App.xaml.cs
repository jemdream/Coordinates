using System.Diagnostics;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Diagnostics;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Coordinates.ExternalDevices.Connections;
using Coordinates.ExternalDevices.DataSources;
using Coordinates.ExternalDevices.Devices;
using Coordinates.ExternalDevices.Models;
using Coordinates.Measurements;
using Coordinates.Measurements.Export;
using Coordinates.UI.Services;
using Coordinates.UI.Services.ServiceLocator;
using Coordinates.UI.Services.SettingsServices;
using Coordinates.UI.ViewModels;
using Coordinates.UI.ViewModels.Interfaces;
using Coordinates.UI.ViewModels.MeasurementFlow;
using Coordinates.UI.ViewModels.MeasurementViewModels;
using Coordinates.UI.Views;
using Template10.Controls;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace Coordinates.UI
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    sealed partial class App : Template10.Common.BootStrapper
    {
        private readonly IUnityContainer _container;

        public App()
        {
            InitializeComponent();
            _container = SetupContainer();

            SplashFactory = (e) => new Splash(e);

            // SettingsService setup
            var settings = _container.Resolve<SettingsService>();
            RequestedTheme = settings.AppTheme;
            CacheMaxDuration = settings.CacheMaxDuration;
            ShowShellBackButton = settings.UseShellBackButton;
        }
        
        public override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            Debug.WriteLine(ApplicationData.Current.LocalFolder.Path);

            // Logger
            await _container.Resolve<IFileLogger>().InitiateLogger();

            // Setup type of window
            var view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode) view.ExitFullScreenMode();

            var size = new Size(1366, 768);

            view.SetPreferredMinSize(size);
            ApplicationView.PreferredLaunchViewSize = size;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            
            // Setup main window
            if (!(Window.Current.Content is ModalDialog))
            {
                // create a new frame and register it
                var navigationService = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);
                _container.RegisterInstance(navigationService, new ContainerControlledLifetimeManager());

                // create modal root
                Window.Current.Content = new ModalDialog
                {
                    DisableBackButtonWhenModal = true,
                    Content = _container.Resolve<Shell>(),
                    ModalContent = _container.Resolve<Busy>()
                };
            }

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

            // External devices layer
            // TODO: [couldBe] modify projects so IDeviceService is unreachable in UI: provide IDeviceManager, where IDeviceService implementations should be internal
            //container.RegisterType<IConnectionService, MockDeviceService>(new ContainerControlledLifetimeManager());
            //container.RegisterType<IDataSource<GaugePositionDTO>, MockDeviceService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IConnectionService, SerialDeviceService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDataSource<GaugePositionDTO>, SerialDeviceService>(new ContainerControlledLifetimeManager());

            // Measurement layer
            container.RegisterType<IMeasurementManager, MeasurementManager>(new ContainerControlledLifetimeManager());

            // UI 

            // Logger
            container.RegisterType<IFileLogger, FileLogger>(new ContainerControlledLifetimeManager());
            container.RegisterInstance(typeof(ILoggingSession), container.Resolve<IFileLogger>().LoggingSession, new ContainerControlledLifetimeManager());

            // Registering Services
            container.RegisterType<IMeasurementsExporter, MeasurementsExporter>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISettingsService, SettingsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDataExportService, DataExportService>(new ContainerControlledLifetimeManager());

            // Registering ViewModels
            container.RegisterType<IMainPageViewModel, MainPageViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDetailPageViewModel, DetailPageViewModel>();
            container.RegisterType<ISettingsPartViewModel, SettingsPartViewModel>();
            container.RegisterType<IAboutPartViewModel, AboutPartViewModel>();
            container.RegisterType<ISettingsPageViewModel, SettingsPageViewModel>();
            container.RegisterType<IInstructionsViewModel, InstructionsViewModel>();

            // Menu 
            container.RegisterType<IMeasurementsPageViewModel, MeasurementsPageViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICodingPlaygroundViewModel, CodingPlaygroundViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<IVisualisationPageViewModel, VisualisationPageViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<IConnectionSetupViewModel, ConnectionSetupViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMeasurementMethodViewModel, MeasurementMethodViewModel>(new ContainerControlledLifetimeManager());

            // Register Measurement Flow ViewModels
            container.RegisterType<IMeasurementSelectionCalculationViewModel, MeasurementSelectionCalculationViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMeasurementCalibrationViewModel, MeasurementCalibrationViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMeasurementProcessViewModel, MeasurementProcessViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMeasurementElementSelectionViewModel, MeasurementElementSelectionViewModel>(new ContainerControlledLifetimeManager());

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
            serviceLocator?.SetupContainer(_container);
        }
    }
}