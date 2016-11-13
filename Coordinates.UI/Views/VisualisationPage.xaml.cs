using System;
using System.Reactive.Linq;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Coordinates.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VisualisationPage : Page
    {
        public VisualisationPage()
        {
            this.InitializeComponent();
        }

        private void VisualisationPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            Busy.SetBusy(true, "Wczytywanie wykresów");

            Observable
                .Timer(TimeSpan.FromMilliseconds(100))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(_ =>
                {
                    FindName("ChartGrid");
                });
        }

        private void Chart_OnLoaded(object sender, RoutedEventArgs e)
        {
            Observable
                .Timer(TimeSpan.FromMilliseconds(500))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(__ => Busy.SetBusy(false));
        }
    }
}