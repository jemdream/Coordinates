using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Coordinates.UI.Helpers.Converters
{
    public class EnumerableCountToValue : IValueConverter
    {
        public int Count { get; set; }
        public object Value { get; set; } = Visibility.Visible;
        public object DefaultValue { get; set; } = Visibility.Collapsed;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var ienum = value as IEnumerable<object>;
            if (ienum == null) return DefaultValue;

            var condition = ienum.Count() == Count;

            return condition ?
                Value :
                DefaultValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            string language)
        {
            throw new NotImplementedException();
        }
    }
}