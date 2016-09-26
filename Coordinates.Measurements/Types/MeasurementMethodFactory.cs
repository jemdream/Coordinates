namespace Coordinates.Measurements.Types
{
    public class MeasurementMethodFactory
    {
        public IMeasurementMethod GetMeasurementMethod(MeasurementMethodEnum method)
        {
            switch (method)
            {
                case MeasurementMethodEnum.OneHoleMeasurementMethod:
                    return new OneHoleMeasurementMethod();
                case MeasurementMethodEnum.TwoHolesMeasurementMethod:
                    return new TwoHolesMeasurementMethod();
                case MeasurementMethodEnum.SurfaceParalellismMeasurementMethod:
                    return new SurfaceParalellismMeasurementMethod();
                case MeasurementMethodEnum.SurfacePerpendicularityMeasurementMethod:
                    return new SurfacePerpendicularityMeasurementMethod();
                default:
                    return null;
            }
        }
    }
}
