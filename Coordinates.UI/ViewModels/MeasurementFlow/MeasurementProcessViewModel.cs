using Coordinates.Measurements;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementProcessViewModel : IMeasurementViewModelBase
    {
    }
    public class MeasurementProcessViewModel : MeasurementViewModelBase, IMeasurementProcessViewModel
    {
        public MeasurementProcessViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
            : base(eventAggregator, measurementManager)
        {
        }
        public override string Title { get; } = "Pomiar";
    }
}
