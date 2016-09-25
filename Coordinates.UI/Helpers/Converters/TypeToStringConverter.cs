using System;
using System.Reflection;
using Windows.UI.Xaml.Data;

namespace Coordinates.UI.Helpers.Converters
{
    public class TypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var type = value as Type;

            if (type == null) return string.Empty;

            var name = type
                .GetProperty("Title", BindingFlags.Static | BindingFlags.Public)
                .GetValue(null, null);

            //.GetFields(BindingFlags.Public | BindingFlags.Static)
            //.Where(f => f.FieldType == typeof(string))
            //.ToDictionary(f => f.Name,
            //              f => (string)f.GetValue(null));

            return name ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
