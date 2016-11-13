using System;
using System.Globalization;
using Windows.UI.Xaml;
using Cimbalino.Toolkit.Converters;
using Coordinates.UI.ViewModels.MeasurementViewModels;

namespace Coordinates.UI.Helpers.Converters
{
    public class EqualityConverter : MultiValueConverterBase
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2 || values[0] == null || values[1] == null)
                return null;

            // dirty hack. multibinding from toolkit somehow caches the value if provided like: {Binding MeasurementMethodViewModel.MeasurementMethod.ActiveElement}
            var vm = values[1] as MeasurementMethodViewModel;

            if (vm == null)
                return null;

            var referenceEquality = values[0] == vm.MeasurementMethod.ActiveElement;

            return referenceEquality ? new Thickness(1) : new Thickness(0);
        }

        public override object[] ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = (string)value + " ";

            var values = stringValue.Split(' ');

            return new object[] { values[0], values[1] };
        }

    }
}
