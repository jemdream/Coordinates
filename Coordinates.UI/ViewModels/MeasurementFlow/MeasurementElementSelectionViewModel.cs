using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Coordinates.Measurements;
using Coordinates.Measurements.Types;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementElementSelectionViewModel : IMeasurementViewModelBase
    {
        IEnumerable<IMeasurementMethod> AvailableMeasurementMethods { get; }
        IMeasurementMethod SelectedMeasurementMethod { get; set; }
    }

    public class MeasurementElementSelectionViewModel : MeasurementViewModelBase, IMeasurementElementSelectionViewModel
    {
        private IMeasurementMethod _selectedMeasurementMethod;

        public MeasurementElementSelectionViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager) : base(eventAggregator, measurementManager)
        {
            AvailableMeasurementMethods = MeasurementManager.AvailableMeasurementMethods;
        }

        public IEnumerable<IMeasurementMethod> AvailableMeasurementMethods { get; }

        public IMeasurementMethod SelectedMeasurementMethod
        {
            get { return _selectedMeasurementMethod; }
            set
            {
                Set(ref _selectedMeasurementMethod, value);
                UpdateCommands();
            }
        }

        public override string Title { get; } = "Element";

        // Measurement process navigation
        protected override async Task<bool> OnNext() => await Task.FromResult(MeasurementManager.SetupMeasurementMethod(SelectedMeasurementMethod));
        protected override bool CanOnNext() => SelectedMeasurementMethod != null;
        protected override bool CanOnPrevious() => false;

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            SelectedMeasurementMethod = MeasurementManager.SelectedMeasurementMethod;
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
