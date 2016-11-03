using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Types;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Helpers.Serialization
{
    public static class ReadableStringHelpers
    {
        public static StringBuilder AsReadableString(this BaseMeasurementMethod baseMeasurementMethod)
        {
            var sB = new StringBuilder();

            sB.AppendLine($"Type: {baseMeasurementMethod}");
            sB.AppendLine($"Plane: {baseMeasurementMethod.Plane}");
            sB.AppendLine("Result:");
            sB.AppendLine($"{baseMeasurementMethod.Calculate()}");
            sB.AppendLine("Elements:");
            sB.AppendLine();

            var indexElements = baseMeasurementMethod.Elements.OfType<BaseElement>()
                .Select((el, i) => new { index = i, element = el.AsReadableString() });

            foreach (var indexElement in indexElements)
            {
                sB.AppendLine($"[{indexElement.index}] {indexElement.element}");
            }

            return sB;
        }

        public static StringBuilder AsReadableString(this BaseElement baseElement)
        {
            var sB = new StringBuilder();

            sB.AppendLine($"Type: {baseElement}");
            sB.AppendLine($"Plane: {baseElement.Plane}");
            sB.AppendLine("Result:");
            sB.AppendLine($"{baseElement.Calculate()}");
            sB.AppendLine("Element points:");
            sB.AppendLine($"{baseElement.Positions.AsReadableString()}");

            return sB;
        }

        public static string AsReadableString(this IEnumerable<Position> positions)
        {
            return string.Join(Environment.NewLine, positions);
        }
    }
}