using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Navigation;
using Coordinates.UI.Messages;
using Coordinates.UI.ViewModels.Interfaces;
using Coordinates.UI.ViewModels.MeasurementFlow;
using Template10.Mvvm;
using Prism.Events;

namespace Coordinates.UI.ViewModels
{
    public class MeasurementsPageViewModel : ViewModelBase, IMeasurementsPageViewModel
    {
        private int _selectedTabIndex;

        public MeasurementsPageViewModel(
            IMeasurementCalibrationViewModel measurementCalibrationViewModel,
            IMeasurementProcessViewModel measurementProcessViewModel,
            IMeasurementSelectionCalculationViewModel measurementSelectionCalculationViewModel,
            IMeasurementElementSelectionViewModel measurementElementSelectionViewModel,
            IEventAggregator eventAggregator)
        //, IConnectionService mockConnectionService/* TODO MOCK CONNECTION */)
        {
            EventAggregator = eventAggregator;

            MeasurementFlowViewModels = new List<IMeasurementViewModelBase>
            {
                 measurementElementSelectionViewModel,
                 measurementCalibrationViewModel,
                 measurementProcessViewModel,
                 measurementSelectionCalculationViewModel
            };

            // Listening to navigation requests from MeasurementFlowViewModels elements
            EventAggregator
                .GetEvent<GoBackMeasurementMsg>()
                .Subscribe(sender =>
                {
                    var index = FindIndex(sender);
                    if (index > 0) NavigationWorkaround(SelectedTabIndex - 1);
                });

            EventAggregator
                .GetEvent<GoNextMeasurementMsg>()
                .Subscribe(sender =>
                {
                    var index = FindIndex(sender);
                    if (index < MeasurementFlowViewModels.Count - 1) NavigationWorkaround(SelectedTabIndex + 1);
                });

            EventAggregator
                .GetEvent<ResetMeasurement>()
                .Subscribe(_ => SelectedTabIndex = 0);

            //MockingDataService = (MockDeviceService)mockConnectionService; // TODO MOCK CONNECTION
        }


        public IEventAggregator EventAggregator { get; }
        public List<IMeasurementViewModelBase> MeasurementFlowViewModels { get; }

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { Set(ref _selectedTabIndex, value); }
        }

        private void NavigationWorkaround(int newIndex)
        {
            // Navigation workaround
            var newViewModel = MeasurementFlowViewModels.ElementAt(newIndex) as MeasurementViewModelBase;
            var oldViewModel = MeasurementFlowViewModels.ElementAt(SelectedTabIndex) as MeasurementViewModelBase;
            oldViewModel?.OnNavigatedFromAsync(null, false);
            newViewModel?.OnNavigatedToAsync(oldViewModel, NavigationMode.Refresh, null);

            // Changing index
            SelectedTabIndex = newIndex;
        }

        private int FindIndex(System.Type sender) => MeasurementFlowViewModels.FindIndex(x => x.GetType() == sender);
    }
}