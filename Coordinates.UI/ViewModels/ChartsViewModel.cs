using Coordinates.Measurements;
using Coordinates.UI.ViewModels.MeasurementViewModels;
using Prism.Events;

namespace Coordinates.UI.ViewModels
{
    public interface IChartsViewModel
    {
        bool IsAvailable { get; }    
    }

    public class ChartsViewModel : IChartsViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMeasurementManager _measurementManager;
        private readonly IMeasurementMethodViewModel _measurementMethodViewModel;

        public ChartsViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager,
            IMeasurementMethodViewModel measurementMethodViewModel)
        {
            _eventAggregator = eventAggregator;
            _measurementManager = measurementManager;
            _measurementMethodViewModel = measurementMethodViewModel;
        }

        public bool IsAvailable { get; } = true;
    }
}