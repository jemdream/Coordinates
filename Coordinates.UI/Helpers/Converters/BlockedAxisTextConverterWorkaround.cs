using System;
using System.Globalization;
using Cimbalino.Toolkit.Converters;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Helpers;
using Coordinates.UI.ViewModels.MeasurementViewModels;

namespace Coordinates.UI.Helpers.Converters
{
    public class BlockedAxisTextConverterWorkaround : MultiValueConverterBase
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return null;

            var vm = values[0] as IElementViewModel;
            var plane = values[1] as PlaneEnum?;

            if (vm == null || plane == null)
                return null;

            var position = vm.Element.InitialPosition;

            if (position == null)
                return null;

            var transformed = position.GetBlockedAxisValue(plane);

            return $"{transformed.Key} = {transformed.Value}";
        }

        public override object[] ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = (string)value + " ";

            var values = stringValue.Split(' ');

            return new object[] { values[0], values[1] };
        }
    }
}
