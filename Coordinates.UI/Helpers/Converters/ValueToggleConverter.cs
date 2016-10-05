using System;
using System.Globalization;
using Cimbalino.Toolkit.Converters;

namespace Coordinates.UI.Helpers.Converters
{
    public class ValueToggleConverter : MultiValueConverterBase
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 3 || !(values[0] is bool))
                return null;

            var positionsCount = values[1] as int?;
            var selectedPositionsCount = values[2] as int?;

            if (positionsCount == null || selectedPositionsCount == null)
                return null;

            return (bool)values[0] ? selectedPositionsCount : positionsCount;
        }

        public override object[] ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = (string)value + " ";

            var values = stringValue.Split(' ');

            return new object[] { values[0], values[1] };
        }
    }
}
