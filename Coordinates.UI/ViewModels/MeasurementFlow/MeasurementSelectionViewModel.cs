using Coordinates.Measurements;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementSelectionViewModel : IMeasurementViewModelBase
    {
    }

    public class MeasurementSelectionViewModel : MeasurementViewModelBase, IMeasurementSelectionViewModel
    {
        public MeasurementSelectionViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager) 
            : base(eventAggregator, measurementManager)
        {
        }
        public override string Title { get; } = "Wybór pomiarów";
    }
}
