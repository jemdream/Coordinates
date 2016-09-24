using Coordinates.Measurements;
using Prism.Events;

namespace Coordinates.UI.ViewModels.MeasurementViewModels
{
    public interface IMeasurementMethodViewModel
    {
        
    }
    public class MeasurementMethodViewModel : IMeasurementMethodViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMeasurementManager _measurementManager;

        public MeasurementMethodViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
        {
            _eventAggregator = eventAggregator;
            _measurementManager = measurementManager;
        }


    }
}
