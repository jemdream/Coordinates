namespace Coordinates.Measurements.Export
{
    public abstract class MeasurementExportFormat
    {
        public string Extension { get; protected set; }
        public string Description { get; protected set; }
        public abstract string Serialize<T>(T toSerialize);
    }
}
