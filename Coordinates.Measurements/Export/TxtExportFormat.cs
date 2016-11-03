using Coordinates.Measurements.Helpers.Serialization;
using Coordinates.Measurements.Types;

namespace Coordinates.Measurements.Export
{
    public class TxtExportFormat : MeasurementExportFormat
    {
        public TxtExportFormat()
        {
            Extension = ".txt";
            Description = "Plik tekstowy (*.txt)";
        }

        public override string Serialize<T>(T toSerialize) =>
            (toSerialize as BaseMeasurementMethod)?.AsReadableString().ToString();
    }
}
