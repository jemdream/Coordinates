using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Coordinates.Models.DTO;

namespace Coordinates.UI.Helpers.Converters
{
    public class PositionTypeToBackground : IValueConverter
    {
        private readonly SolidColorBrush _contact = new SolidColorBrush(Color.FromArgb(255, 30, 215, 96));//= new SolidColorBrush(Color.FromArgb(255, 127, 223, 73));//(SolidColorBrush)Application.Current.Resources["GaugeContactBarColorBrush"];
        private readonly SolidColorBrush _noContact = (SolidColorBrush)Application.Current.Resources["SystemControlBackgroundChromeMediumBrush"];

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var position = value as Position;

            if (position == null) return _noContact;

            return position.Contact ? _contact : _noContact;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}