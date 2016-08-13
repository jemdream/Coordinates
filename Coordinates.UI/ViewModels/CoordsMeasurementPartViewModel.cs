using Coordinates.Measurements;
using Coordinates.UI.Messages;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class CoordsMeasurementPartViewModel : ViewModelBase, ICoordsMeasurementPartViewModel
    {
        private readonly IMeasurementManager _measurementManager;

        public CoordsMeasurementPartViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
        {
            _measurementManager = measurementManager;

            eventAggregator
                .GetEvent<NewMeasurementMessage>()
                .Subscribe(_ => InitializeNewMeasurement());
        }

        private void InitializeNewMeasurement()
        {
            
        }
    }
}