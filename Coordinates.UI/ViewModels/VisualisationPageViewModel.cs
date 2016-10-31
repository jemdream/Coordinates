using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
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
        private bool _renderCharts;

        public VisualisationPageViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager,
            IMeasurementMethodViewModel measurementMethodViewModel)
        {
            _eventAggregator = eventAggregator;
            _measurementManager = measurementManager;
            MeasurementMethodViewModel = measurementMethodViewModel;
        }

        public bool IsAvailable { get; } = true;
        public IMeasurementMethodViewModel MeasurementMethodViewModel { get; }

        public bool RenderCharts
        {
            get { return _renderCharts; }
            set { Set(ref _renderCharts, value); }
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> pageState, bool suspending)
        {
            RenderCharts = false;
            return base.OnNavigatedFromAsync(pageState, suspending);
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            RenderCharts = true;
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}