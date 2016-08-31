using System.Collections.Generic;
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
            IMeasurementCalculationsViewModel measurementCalculationsViewModel,
            IMeasurementCalibrationViewModel measurementCalibrationViewModel,
            IMeasurementProcessViewModel measurementProcessViewModel,
            IMeasurementSelectionViewModel measurementSelectionViewModel,
            IMeasurementTypeSelectionViewModel measurementTypeSelectionViewModel,
            IEventAggregator eventAggregator)
        //, IConnectionService mockConnectionService/* TODO MOCK CONNECTION */)
        {
            EventAggregator = eventAggregator;

            MeasurementFlowViewModels = new List<IMeasurementViewModelBase>
            {
                 measurementTypeSelectionViewModel,
                 measurementCalibrationViewModel,
                 measurementProcessViewModel,
                 measurementSelectionViewModel,
                 measurementCalculationsViewModel
            };

            // Listening to navigation requests from MeasurementFlowViewModels elements
            EventAggregator
                .GetEvent<GoBackMeasurementMsg>()
                .Subscribe(sender => { if (FindIndex(sender) > 0) SelectedTabIndex--; });

            EventAggregator
                .GetEvent<GoNextMeasurementMsg>()
                .Subscribe(sender => { if (FindIndex(sender) < MeasurementFlowViewModels.Count - 1) SelectedTabIndex++; });

            //MockingDataService = (MockDeviceService)mockConnectionService; // TODO MOCK CONNECTION
        }

        private int FindIndex(System.Type sender) => MeasurementFlowViewModels.FindIndex(x => x.GetType() == sender);

        public IEventAggregator EventAggregator { get; }
        public List<IMeasurementViewModelBase> MeasurementFlowViewModels { get; }

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { Set(ref _selectedTabIndex, value); }
        }
    }
}