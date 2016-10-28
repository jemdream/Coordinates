using Coordinates.Measurements;
using Coordinates.UI.ViewModels.Interfaces;
using Coordinates.UI.ViewModels.MeasurementViewModels;
using Prism.Events;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class VisualisationPageViewModel : ViewModelBase, IVisualisationPageViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMeasurementManager _measurementManager;

        public VisualisationPageViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager,
            IMeasurementMethodViewModel measurementMethodViewModel)
        {
            _eventAggregator = eventAggregator;
            _measurementManager = measurementManager;
            MeasurementMethodViewModel = measurementMethodViewModel;
        }

        public bool IsAvailable { get; } = true;
        public IMeasurementMethodViewModel MeasurementMethodViewModel { get; }
    }
}