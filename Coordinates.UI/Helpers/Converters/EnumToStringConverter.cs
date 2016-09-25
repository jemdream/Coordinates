using System;
using System.Reflection;
using Windows.UI.Xaml.Data;
using Coordinates.Models.Helpers;

namespace Coordinates.UI.Helpers.Converters
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var @enum = value as Enum;

            if (@enum == null) return null;
            
            var attrib = GetAttribute<DescriptionAttribute>(@enum);

            return attrib != null ? attrib.Description : @enum.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            string language)
        {
            throw new NotImplementedException();
        }

        private static T GetAttribute<T>(Enum enumValue) where T : Attribute
        {
            return enumValue.GetType().GetTypeInfo()
                .GetDeclaredField(enumValue.ToString())
                .GetCustomAttribute<T>();
        }

    }
}
