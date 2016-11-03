using System.Collections.Generic;

namespace Coordinates.Measurements.Export
{
    public interface IMeasurementsExporter
    {
        IEnumerable<MeasurementExportFormat> Formats { get; }
    }

    public class MeasurementsExporter : IMeasurementsExporter
    {
        public IEnumerable<MeasurementExportFormat> Formats { get; }
            = new MeasurementExportFormat[] { new JsonExportFormat(), new TxtExportFormat() };
    }
}