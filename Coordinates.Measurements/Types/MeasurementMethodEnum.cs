using System.Reflection;
using System.ComponentModel;

namespace Coordinates.Measurements.Types
{
    public enum MeasurementMethodEnum
    {
        [Description("Płaszczyzny - równoległoś")]
        OneHoleMeasurementMethod,
        SurfaceParalellismMeasurementMethod,
        SurfacePerpendicularityMeasurementMethod,
        TwoHolesMeasurementMethod
    }
}
