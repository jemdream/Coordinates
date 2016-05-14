using System;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Coordinates.UI.Views
{
    public sealed partial class Splash : UserControl
    {
        public Splash(SplashScreen splashScreen)
        {
            InitializeComponent();

            Window.Current.SizeChanged += (s, e) => Resize(splashScreen);
            Resize(splashScreen);
        }

        /// <summary>
        /// Callback function handling resizing of the window.
        /// E.g. to resize controllers.
        /// </summary>
        /// <param name="splashScreen"></param>
        private void Resize(SplashScreen splashScreen)
        {
            if (Math.Abs(splashScreen.ImageLocation.Top) < 0.001)
            {
                SplashImage.Visibility = Visibility.Collapsed;
                return;
            }

            RootCanvas.Background = null;
            SplashImage.Visibility = Visibility.Visible;

            SplashImage.Height = splashScreen.ImageLocation.Height;
            SplashImage.Width = splashScreen.ImageLocation.Width;
            SplashImage.SetValue(Canvas.TopProperty, splashScreen.ImageLocation.Top);
            SplashImage.SetValue(Canvas.LeftProperty, splashScreen.ImageLocation.Left);
            ProgressTransform.TranslateY = SplashImage.Height / 2;
            // some mocked code
        }
    }
}