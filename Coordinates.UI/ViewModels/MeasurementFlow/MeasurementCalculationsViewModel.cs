using Coordinates.Measurements;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementCalculationsViewModel : IMeasurementViewModelBase
    {
    }

    public class MeasurementCalculationsViewModel : MeasurementViewModelBase, IMeasurementCalculationsViewModel
    {
        public MeasurementCalculationsViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
            : base(eventAggregator, measurementManager)
        {
        }

        public override string Title { get; } = "Obliczenia";

        protected override bool CanOnNext() => false;
    }
}
