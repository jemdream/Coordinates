using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Coordinates.Measurements;
using Coordinates.Measurements.Types;
using Prism.Events;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementElementSelectionViewModel : IMeasurementViewModelBase
    {
        IEnumerable<MeasurementMethodEnum> AvailableMeasurementMethods { get; }
        MeasurementMethodEnum? SelectedMeasurementMethod { get; set; }
    }

    public class MeasurementElementSelectionViewModel : MeasurementViewModelBase, IMeasurementElementSelectionViewModel
    {
        private readonly IMeasurementManager _measurementManager;
        private MeasurementMethodEnum? _selectedMeasurementMethod;

        public MeasurementElementSelectionViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager) : base(eventAggregator)
        {
            _measurementManager = measurementManager;
            AvailableMeasurementMethods = _measurementManager.AvailableMeasurementMethods;
        }

        public override string Title { get; } = "Element";

        public IEnumerable<MeasurementMethodEnum> AvailableMeasurementMethods { get; }

        public MeasurementMethodEnum? SelectedMeasurementMethod
        {
            get { return _selectedMeasurementMethod; }
            set
            {
                Set(ref _selectedMeasurementMethod, value);
                UpdateNavigationCommands();
            }
        }

        // Measurement process navigation
        protected override async Task<bool> OnNext()
        {
            return await Task.FromResult(_measurementManager.SetupMeasurementMethod(SelectedMeasurementMethod ?? 0));
        }

        protected override bool CanOnNext() => SelectedMeasurementMethod != null;
        protected override bool CanOnPrevious() => false;

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            SelectedMeasurementMethod = _measurementManager.SelectedMeasurementMethod;
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
