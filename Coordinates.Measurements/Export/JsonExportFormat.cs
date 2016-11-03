using Newtonsoft.Json;

namespace Coordinates.Measurements.Export
{
    public class JsonExportFormat : MeasurementExportFormat
    {
        public JsonExportFormat()
        {
            Extension = ".json";
            Description = "Plik json (*.json)";
        }

        public override string Serialize<T>(T toSerialize)
            => JsonConvert.SerializeObject(toSerialize, Formatting.Indented);
    }
}
