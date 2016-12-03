using System;
using System.Collections.Generic;
using Coordinates.Measurements.Elements;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Helpers
{
    public static class ExtensionMethods
    {
        public static double Round(this double value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Returns -Key(axis):Value(Position on axis value)- according to provided plane
        /// </summary>
        public static KeyValuePair<string, double> GetBlockedAxisValue(this Position position, PlaneEnum? plane)
        {
            switch (plane)
            {
                case PlaneEnum.XY:
                    return new KeyValuePair<string, double>(nameof(position.Z), position.Z);
                case PlaneEnum.YZ:
                    return new KeyValuePair<string, double>(nameof(position.X), position.X);
                case PlaneEnum.ZX:
                    return new KeyValuePair<string, double>(nameof(position.Y), position.Y);
                case null:
                    return default(KeyValuePair<string, double>);
                default:
                    throw new ArgumentOutOfRangeException(nameof(plane), plane, null);
            }
        }
    }
}
