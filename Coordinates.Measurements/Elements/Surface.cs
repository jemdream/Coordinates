using System;
using Coordinates.Measurements.Models;

namespace Coordinates.Measurements.Elements
{
    public class Surface : BaseElement
    {
        public override int RequiredMeasurementCount { get; } = 5;

        public override bool CanCalculate()
        {
            return SelectedPositions.Count >= RequiredMeasurementCount;
        }

        public override ICalculationResult Calculate()
        {
            if (!CanCalculate())
                return new ErrorResult { Message = "Wybierz odpowiednią liczbę pomiarów." };

            try
            {
                double a = 0, b = 0, c = 0, d = 0, e = 0, f = 0, g = 0, h = 0, i = 0;
                double w = 0, wa1 = 0, wa2 = 0, wa3 = 0;
                double a0 = 0, a1 = 0, a2 = 0;
                double n = SelectedPositions.Count;

                foreach (var pos in SelectedPositions)
                {
                    a = a + pos.X;
                    b = b + pos.Y;
                    c = c + pos.Z;
                    d = d + (pos.X * pos.Y);
                    e = e + (pos.Y * pos.Z);
                    f = f + (pos.X * pos.Z);
                    g = g + (pos.X * pos.X);
                    h = h + (pos.Y * pos.Y);
                    i = i + (pos.Z * pos.Z);
                }

                switch (Plane)
                {
                    case PlaneEnum.XY:
                        w = n * g * h + 2 * a * b * d - b * b * g - a * a * h - n * d * d;

                        if (w.Equals(0.0))
                            return new ErrorResult { Message = "Wybrane pomiary są zbyt blisko siebie lub wykonane w linii prostej." };
                        
                        wa1 = c * g * h + b * d * f + a * d * e - b * e * g - a * f * h - c * d * d;
                        wa2 = n * f * h + a * b * e + b * c * d - b * b * f - a * c * h - n * e * d;
                        wa3 = n * e * g + a * c * d + a * b * f - b * c * g - a * a * e - n * d * f;
                        break;
                    case PlaneEnum.YZ:
                        w = n * h * i + 2 * b * c * e - c * c * h - b * b * i - n * e * e;

                        if (w.Equals(0.0))
                            return new ErrorResult { Message = "Wybrane pomiary są zbyt blisko siebie lub wykonane w linii prostej." };
                        
                        wa1 = a * h * i + c * d * e + b * e * f - c * f * h - b * d * i - a * e * e;
                        wa2 = n * d * i + b * c * f + a * c * e - c * c * d - a * b * i - n * e * f;
                        wa3 = n * f * h + a * b * e + b * c * d - a * c * h - b * b * f - n * e * d;
                        break;
                    case PlaneEnum.ZX:
                        w = n * g * i + 2 * a * c * f - a * a * i - c * c * g - n * f * f;

                        if (w.Equals(0.0))
                            return new ErrorResult { Message = "Wybrane pomiary są zbyt blisko siebie lub wykonane w linii prostej." };
                        
                        wa1 = b * g * i + a * e * f + c * d * f - a * d * i - c * e * g - b * f * f;
                        wa2 = n * e * g + a * c * d + a * b * f - a * a * e - b * c * g - n * d * f;
                        wa3 = n * d * i + b * c * f + a * c * e - a * b * i - c * c * d - n * e * f;
                        break;
                }

                a0 = wa1 / w;
                a1 = wa2 / w;
                a2 = wa3 / w;

                return new SurfaceResult
                {
                    A0 = a0,
                    A1 = a1,
                    A2 = a2
                };
            }
            catch (Exception ex)
            {
                return new ErrorResult
                {
                    Message = ex.Message
                };
            }
        }

        public override string ToString() => "Powierzchnia";
    }
}