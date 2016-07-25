namespace Coordinates.UI.ViewModels.MeasurementViewModels
{
    public class FlatnessMeasurementViewModel : IMeasurementTypeViewModel
    {
        public string MeasurementName { get; } = "Płaskość";
        
        public override string ToString()
        {
            return MeasurementName;
        }
    }
}