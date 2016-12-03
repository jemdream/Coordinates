using System;
using Coordinates.Measurements.Models;

namespace Coordinates.Measurements.Elements
{
    public class Hole : BaseElement
    {
        public override int RequiredMeasurementCount { get; } = 5;
        public static double SensorRadius = 4.9985;

        public override bool CanCalculate()
        {
            return SelectedPositions.Count >= RequiredMeasurementCount;
        }
        
        public override ICalculationResult Calculate()
        {
            if (!CanCalculate()) return new ErrorResult { Message = "Wybierz odpowiednią liczbę pomiarów." };

            double a = 0, b = 0, c = 0, d = 0, e = 0, f = 0, g = 0, h = 0, i = 0, j = 0;
            double n = SelectedPositions.Count;
            double r;
            double aa, bb;
            double R = 0;
            double x0 = 0, y0 = 0, z0 = 0;
            double dx, dy, dz;

            switch (Plane)
            {
                case PlaneEnum.XY:
                    foreach (var pos in SelectedPositions)
                    {
                        a = a + pos.X;
                        b = b + pos.Y;
                    }
                    foreach (var pos in SelectedPositions)
                    {
                        dx = pos.X - (a / n);
                        dy = pos.Y - (b / n);
                        c = c + dx;
                        d = d + dy;
                        e = e + dx * dx;
                        f = f + dy * dy;
                        g = g + (dx * dy);
                        h = h + (dx * dx) + (dy * dy);
                        i = i + ((dx * dx) + (dy * dy)) * dx;
                        j = j + ((dx * dx) + (dy * dy)) * dy;
                        z0 = pos.Z;
                    }
                    r = 2 * ((e * f) - (g * g));
                    if (r.Equals(0.0))
                    {
                        return new ErrorResult {Message = "Wybrane pomiary są zbyt blisko siebie"};
                    }
                    aa = ((i * f) - (j * g)) / r;
                    bb = ((j * e) - (i * g)) / r;
                    x0 = (a / n) + aa;
                    y0 = (b / n) + bb;
                    R = Math.Sqrt((aa * aa) + (bb * bb) + ((e + f) / n)) + SensorRadius;
                    break;

                case PlaneEnum.YZ:
                    foreach (var pos in SelectedPositions)
                    {
                        a = a + pos.Y;
                        b = b + pos.Z;
                    }
                    foreach (var pos in SelectedPositions)
                    {
                        dy = pos.Y - (a / n);
                        dz = pos.Z - (b / n);
                        c = c + dy;
                        d = d + dz;
                        e = e + dy * dy;
                        f = f + dz * dz;
                        g = g + (dy * dz);
                        h = h + (dy * dy) + (dz * dz);
                        i = i + ((dy * dy) + (dz * dz)) * dy;
                        j = j + ((dy * dy) + (dz * dz)) * dz;
                        x0 = pos.X;
                    }
                    r = 2 * ((e * f) - (g * g));
                    if (r.Equals(0.0))
                    {
                        return new ErrorResult { Message = "Wybrane pomiary są zbyt blisko siebie" };
                    }
                    aa = ((i * f) - (j * g)) / r;
                    bb = ((j * e) - (i * g)) / r;
                    y0 = (a / n) + aa;
                    z0 = (b / n) + bb;
                    R = Math.Sqrt((aa * aa) + (bb * bb) + ((e + f) / n)) + SensorRadius;
                    break;

                case PlaneEnum.ZX:
                    foreach (var pos in SelectedPositions)
                    {
                        a = a + pos.Z;
                        b = b + pos.X;
                    }
                    foreach (var pos in SelectedPositions)
                    {
                        dz = pos.Z - (a / n);
                        dx = pos.X - (b / n);
                        c = c + dz;
                        d = d + dx;
                        e = e + dz * dz;
                        f = f + dx * dx;
                        g = g + (dz * dx);
                        h = h + (dz * dz) + (dx * dx);
                        i = i + ((dz * dz) + (dx * dx)) * dz;
                        j = j + ((dz * dz) + (dx * dx)) * dx;
                        y0 = pos.Y;
                    }
                    r = 2 * ((e * f) - (g * g));
                    if (r.Equals(0.0))
                    {
                        return new ErrorResult { Message = "Wybrane pomiary są zbyt blisko siebie" };
                    }
                    aa = ((i * f) - (j * g)) / r;
                    bb = ((j * e) - (i * g)) / r;
                    z0 = (a / n) + aa;
                    x0 = (b / n) + bb;
                    R = Math.Sqrt((aa * aa) + (bb * bb) + ((e + f) / n)) + SensorRadius;
                    break;
            }

            return new HoleResult
            {
                X0 = x0,
                Y0 = y0,
                Z0 = z0,
                R = R
            };
        }

        public override string ToString() => "Otwór";
    }
}
