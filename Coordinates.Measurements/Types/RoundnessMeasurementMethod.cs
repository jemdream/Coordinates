namespace Coordinates.Measurements.Types
{
    public class RoundnessMeasurementMethod : IMeasurementMethod
    {
        public bool CanExecute()
        {
            return true;
        }

        public object Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
