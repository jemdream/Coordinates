using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Coordinates.ExternalDevices;

namespace Coordinates.UI.Helpers.Converters
{
    public class ConnectionStatusToForeground : IValueConverter
    {
        private readonly SolidColorBrush _positiveBrush = (SolidColorBrush)Application.Current.Resources["SystemControlForegroundBaseHighBrush"];
        private readonly SolidColorBrush _negativeBrush = new SolidColorBrush(Colors.Red);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is ConnectionStatus)) return _negativeBrush;

            var connectionStatus = (ConnectionStatus) value;
            
            switch (connectionStatus)
            {
                case ConnectionStatus.Open:
                case ConnectionStatus.Opening:
                    return _positiveBrush;
                default:
                    return _negativeBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
