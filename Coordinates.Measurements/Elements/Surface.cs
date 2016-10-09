using System;
using System.Collections.Generic;

namespace Coordinates.Measurements.Elements
{
    public class Surface : BaseElement
    {
        public override int RequiredMeasurementCount { get; } = 5;

        public override bool CanCalculate()
        {
            return SelectedPositions.Count >= RequiredMeasurementCount;
        }

        public override object Calculate()
        {
            double a = 0, b = 0, c = 0, d = 0, e = 0, f = 0, g = 0, h = 0, i = 0;
            double w = 0, wa1 = 0, wa2 = 0, wa3 = 0;
            double a1 = 0, a2 = 0, a3 = 0;
            double n = SelectedPositions.Count;
            foreach (var pos in SelectedPositions)
            {
                a = a + pos.X;
                b = b + pos.Y;
                c = c + pos.Z;
                d = d + pos.X * pos.Y;
                e = e + pos.Y * pos.Z;
                f = f + pos.X * pos.Z;
                g = g + pos.X * pos.X;
                h = h + pos.Y * pos.Y;
                i = i + pos.Z * pos.Z;
            }
            if (Plane == PlaneEnum.XY)
            {
                w = n * g * h + a * b * d + a * b * d - b * b * g - a * a * h - n * d * d;
                wa1 = c * g * h + f * d * b + a * d * e - e * g * b - f * a * h - c * d * d;
                wa2 = n * f * h + a * b * e + b * c * d - b * b * f - a * c * h - n * e * d;
                wa3 = n * g * e + a * c * d + a * b * f - b * c * g - a * a * e - n * d * f;
            }
            if (Plane == PlaneEnum.YZ)
            {
                w = n * h * i + b * e * c + b * e * c - c * c * h - b * b * i - n * e * e;
                wa1 = a * h * i + d * e * c + b * e * f - f * h * c - d * b * i - a * e * e;
                wa2 = n * d * i + b * c * f + c * a * e - c * c * d - b * a * i - n * f * e;
                wa3 = n * h * f + b * a * e + b * c * d - c * a * h - b * b * f - n * e * d;
            }
            if (Plane == PlaneEnum.ZX)
            {
                w = n * i * g + c * f * a + c * f * a - a * a * i - c * c * g - n * f * f;
                wa1 = b * i * g + e * f * a + c * f * d - d * i * a - e * c * g - b * f * f;
                wa2 = n * e * g + c * a * d + a * b * f - a * a * e - c * b * g - n * d * f;
                wa3 = n * i * d + c * b * f + c * a * e - a * b * i - c * c * d - n * f * e;
            }
            a1 = wa1 / w;
            a2 = wa2 / w;
            a3 = wa3 / w;
            var result = new List<object>();
            result.Add(a1);
            result.Add(a2);
            result.Add(a3);
            return result;
        }
    }
}
