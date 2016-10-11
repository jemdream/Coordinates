using Coordinates.Models.Helpers;

namespace Coordinates.Measurements.Types
{
    public enum MeasurementMethodEnum
    {
        [Description("Jeden otwór")]
        OneHoleMeasurementMethod,

        [Description("Dwa otwory")]
        TwoHolesMeasurementMethod,

        [Description("Płaszczyzny - równoległość")]
        SurfaceParalellismMeasurementMethod,

        [Description("Płaszczyzny - prostopadłość")]
        SurfacePerpendicularityMeasurementMethod
    }
}
