using System;
using System.Collections.Generic;
using Coordinates.Measurements.Models;
using Coordinates.Models.DTO;

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
            if (!CanCalculate()) return null;

            // TODO validation example (checking if points are not too far from each other etc.) and return Error Result
            //if (!ValidateSelectedPositions())
            //{
            //    return new ErrorResult
            //    {
            //        Message = "Jakiś błąd na UI",
            //        FaultyPositions = new List<Position>() // pomiary, które mają złe wartości względem pozostałych zaznaczonych
            //    };
            //}

            try
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

                switch (Plane)
                {
                    case PlaneEnum.XY:
                        w = n * g * h + a * b * d + a * b * d - b * b * g - a * a * h - n * d * d;
                        wa1 = c * g * h + f * d * b + a * d * e - e * g * b - f * a * h - c * d * d;
                        wa2 = n * f * h + a * b * e + b * c * d - b * b * f - a * c * h - n * e * d;
                        wa3 = n * g * e + a * c * d + a * b * f - b * c * g - a * a * e - n * d * f;
                        break;
                    case PlaneEnum.YZ:
                        w = n * h * i + b * e * c + b * e * c - c * c * h - b * b * i - n * e * e;
                        wa1 = a * h * i + d * e * c + b * e * f - f * h * c - d * b * i - a * e * e;
                        wa2 = n * d * i + b * c * f + c * a * e - c * c * d - b * a * i - n * f * e;
                        wa3 = n * h * f + b * a * e + b * c * d - c * a * h - b * b * f - n * e * d;
                        break;
                    case PlaneEnum.ZX:
                        w = n * i * g + c * f * a + c * f * a - a * a * i - c * c * g - n * f * f;
                        wa1 = b * i * g + e * f * a + c * f * d - d * i * a - e * c * g - b * f * f;
                        wa2 = n * e * g + c * a * d + a * b * f - a * a * e - c * b * g - n * d * f;
                        wa3 = n * i * d + c * b * f + c * a * e - a * b * i - c * c * d - n * f * e;
                        break;
                }

                // wczesniej tez dodac walidacje jakas na inne parametry - uzyc 'ref'
                ValidateWs(ref w, ref wa1, ref wa2, ref wa3);

                a1 = wa1 / w;
                a2 = wa2 / w;
                a3 = wa3 / w;

                return new SurfaceResult
                {
                    A1 = a1,
                    A2 = a2,
                    A3 = a3
                };
            }
            catch (Exception ex)
            {
                return new ErrorResult
                {
                    Message = ex.Message,
                    FaultyPositions = new List<Position>() // pomiary, które mają złe wartości względem pozostałych zaznaczonych
                };
            }
        }

        private static void ValidateWs(ref double w, ref double wa1, ref double wa2, ref double wa3)
        {
            var faultyW = false;
            if (faultyW) throw new Exception("Coś się zepsuło i nie było mnie słychać. @faultyW");

            var faultyWA1 = false;
            if (faultyWA1) throw new Exception("Coś się zepsuło i nie było mnie słychać. @faultyWA1");

            var faultyWA2 = false;
            if (faultyWA2) throw new Exception("Coś się zepsuło i nie było mnie słychać. @faultyWA2");

            var faultyWA3 = false;
            if (faultyWA3) throw new Exception("Coś się zepsuło i nie było mnie słychać. @faultyWA3");
        }
    }
}