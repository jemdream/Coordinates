using Coordinates.UI.ViewModels.Interfaces;

namespace Coordinates.UI.ViewModels.MeasurementViewModels
{
    public class RoundnessMeasurementViewModel : IMeasurementTypeViewModel
    {
        public string MeasurementName { get; } = "Okrągłość";
        public override string ToString()
        {
            return MeasurementName;
        }
    }
}