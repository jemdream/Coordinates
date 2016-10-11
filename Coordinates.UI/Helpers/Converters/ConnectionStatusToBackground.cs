using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Coordinates.ExternalDevices;

namespace Coordinates.UI.Helpers.Converters
{
    public class ConnectionStatusToBackground : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var test = (ConnectionStatus)value;
            
            switch (test)
            {
                case ConnectionStatus.Uninitialized:
                    break;
                case ConnectionStatus.Opening:
                    return new SolidColorBrush(Colors.Aquamarine);
                case ConnectionStatus.Open:
                    return new SolidColorBrush(Colors.DarkSeaGreen);
                case ConnectionStatus.Closing:
                    return new SolidColorBrush(Colors.LightPink);
                case ConnectionStatus.Closed:
                    return new SolidColorBrush(Colors.LightCoral);
                case ConnectionStatus.Broken:
                    return new SolidColorBrush(Colors.PaleVioletRed);
                default:
                    break;
            }

            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
