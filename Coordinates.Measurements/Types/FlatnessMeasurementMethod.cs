namespace Coordinates.Measurements.Types
{
    public class FlatnessMeasurementMethod : IMeasurementMethod
    {
        public bool CanExecute()
        {
            return true;
        }

        public object Execute()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "Płaskość";
        }
    }
}
