using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Coordinates.UI.ViewModels.Interfaces;
using Coordinates.UI.ViewModels.MeasurementViewModels;
using Prism.Events;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementSelectionCalculationViewModel : IMeasurementViewModelBase
    {
        IMeasurementMethodViewModel MeasurementMethodViewModel { get; }
    }

    public class MeasurementSelectionCalculationViewModel : MeasurementViewModelBase, IMeasurementSelectionCalculationViewModel
    {
        public MeasurementSelectionCalculationViewModel(IEventAggregator eventAggregator,
            IMeasurementMethodViewModel measurementMethodViewModel) : base(eventAggregator)
        {
            MeasurementMethodViewModel = measurementMethodViewModel;
        }

        public override string Title { get; } = "Wybór pomiarów i obliczenia";

        public IMeasurementMethodViewModel MeasurementMethodViewModel { get; }

        protected override bool CanOnNext() => false;
        
        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            UpdateNavigationCommands();

            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
