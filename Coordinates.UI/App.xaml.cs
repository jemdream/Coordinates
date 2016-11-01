using System.Diagnostics;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Coordinates.UI.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Coordinates.ExternalDevices.Connections;
using Coordinates.ExternalDevices.DataSources;
using Coordinates.ExternalDevices.Devices;
using Coordinates.ExternalDevices.Models;
using Coordinates.Measurements;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Export;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;
using Coordinates.UI.Services;
using Coordinates.UI.Services.ServiceLocator;
using Coordinates.UI.ViewModels;
using Coordinates.UI.ViewModels.Interfaces;
using Coordinates.UI.ViewModels.MeasurementFlow;
using Coordinates.UI.ViewModels.MeasurementViewModels;
using Coordinates.UI.Views;
using Template10.Controls;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
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
            //MeasurementDevelopment();

            InitializeComponent();
            _myContainer = SetupContainer();
            SplashFactory = (e) => new Splash(e);

            // SettingsService setup
            var settings = _myContainer.Resolve<SettingsService>();
            RequestedTheme = settings.AppTheme;
            CacheMaxDuration = settings.CacheMaxDuration;
            ShowShellBackButton = settings.UseShellBackButton;
        }
        
        private static void MeasurementDevelopment()
        {
            // caly pomiar
            var measurements = new SurfaceParalellismMeasurementMethod();

            #region First Element

            var firstElement = measurements.ActivateNextElement();
            firstElement.Plane = PlaneEnum.YZ;
            var mockoweZaznaczoneDaneFirstElement = new[]
            //{
            //    new Position(1, 1, 3, true), new Position(0.5, 3, 4, true),
            //    new Position(1, 6, 8, true), new Position(1, 10, 12, true),
            //    new Position(0.5, 5, 14, true)
            //};
            //{
            //    new Position(1, 8, 7, true), new Position(5, 7.3, 3, true),
            //    new Position(3, 7.5, 4, true), new Position(4, 7.1, 5, true),
            //    new Position(6, 7.3, 6, true)
            //};
            {
                new Position(1, 1, 1, true), new Position(1, 100, 1, true),
                new Position(1, 1, 100, true), new Position(1, 50, 51, true),
                new Position(1, 100, 100, true)
            };

            foreach (var position in mockoweZaznaczoneDaneFirstElement)
                firstElement.SelectedPositions.Add(position);

            var canCalculateFirstElement = firstElement.CanCalculate();
            var calculateFirstElement = firstElement.Calculate();


            //Debugger.Break();

            #endregion

            #region Second Element

            var secondElement = measurements.ActivateNextElement();
            secondElement.Plane = PlaneEnum.YZ;
            var mockoweZaznaczoneDaneSecondElement = new[]
            {
                new Position(0.3, 0.1, 1.0, true), new Position(0.6, 0.4, 0.5, true),
                new Position(1.2, 1.0, 1.0, true), new Position(13.0, 5.0, 1.0, true),
                new Position(1.5, 11.3, 0.5, true)
            };

            foreach (var position in mockoweZaznaczoneDaneSecondElement)
                secondElement.SelectedPositions.Add(position);

            var canCalculateSecondElement = secondElement.CanCalculate();
            var calculateSecondElement = secondElement.Calculate();

            //Debugger.Break();

            #endregion

            var canCalculate = measurements.CanCalculate();
            var calculate = measurements.Calculate();

            Debugger.Break();
            // TODO TERMINATE
        }

        public override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            Debug.WriteLine(ApplicationData.Current.LocalFolder.Path);

            var view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode) view.ExitFullScreenMode();

            view.SetPreferredMinSize(new Size(1366, 768));
            ApplicationView.PreferredLaunchViewSize = new Size(1366, 768);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

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
            // TODO: modify projects so IDeviceService is unreachable in UI: provide IDeviceManager, where IDeviceService implementations should be internal
            //container.RegisterType<IConnectionService, MockDeviceService>(new ContainerControlledLifetimeManager());
            //container.RegisterType<IDataSource<GaugePositionDTO>, MockDeviceService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IConnectionService, SerialDeviceService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDataSource<GaugePositionDTO>, SerialDeviceService>(new ContainerControlledLifetimeManager());

            // Measurement layer
            container.RegisterType<IMeasurementManager, MeasurementManager>(new ContainerControlledLifetimeManager());

            // UI 

            // Registering Services
            container.RegisterType<IMeasurementsExporter, MeasurementsExporter>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISettingsService, SettingsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDataExportService, DataExportService>(new ContainerControlledLifetimeManager());
            
            // Registering ViewModels
            container.RegisterType<IMainPageViewModel, MainPageViewModel>();
            container.RegisterType<IDetailPageViewModel, DetailPageViewModel>();
            container.RegisterType<ISettingsPartViewModel, SettingsPartViewModel>();
            container.RegisterType<IAboutPartViewModel, AboutPartViewModel>();
            container.RegisterType<ISettingsPageViewModel, SettingsPageViewModel>();

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
            serviceLocator?.SetupContainer(_myContainer);
        }
    }
}