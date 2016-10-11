using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Coordinates.UI.Helpers.Converters
{
    public class BooleanToSelectionMode : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var boolean = value as bool?;

            if (boolean == null) return null;

            if ((bool) boolean) return ListViewSelectionMode.Multiple;
            return ListViewSelectionMode.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}