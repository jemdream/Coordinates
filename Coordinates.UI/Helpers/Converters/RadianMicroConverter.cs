using System;
using Windows.UI.Xaml.Data;
using Coordinates.Measurements.Models;

namespace Coordinates.UI.Helpers.Converters
{
    /// <summary>
    /// If the value passed is below or equal the threshold, value is being converted to micro - multiplied by 1kk.
    /// UnderThresholdSuffix/OverThresholdSuffix added to converted value based on comparison result.
    /// </summary>
    public class ValueToMicroConverter : IValueConverter
    {
        public double Threshold { get; set; } = 0.01;
        public int DecimalPoints { get; set; } = 2;
        public string UnderThresholdSuffix { get; set; } = "[µrad]";
        public string OverThresholdSuffix { get; set; } = "[rad]";

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var sParalelismR = value as SurfaceParalellismResult;
            var sPerpendicR = value as SurfacePerpendicularityResult;
            double? valNullable;

            if (sParalelismR != null)
                valNullable = sParalelismR.Result;
            else if (sPerpendicR != null)
                valNullable = sPerpendicR.Result;
            else
                return null;
            
            var val = (double)valNullable;

            return val > Threshold ?
                PrepareToView(val, OverThresholdSuffix) :
                PrepareToView(val * 1000000.0, UnderThresholdSuffix);
        }

        private string PrepareToView(double val, string suffix)
        {
            var rounded = Math.Round(val, DecimalPoints, MidpointRounding.AwayFromZero);
            return $"{rounded} {suffix}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}