using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Charts;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Coordinates.UI.Components
{
    public sealed partial class PlaneChart : UserControl
    {
        #region Dependency Properties
        public bool RenderCharts
        {
            get { return (bool)GetValue(RenderChartsProperty); }
            set { SetValue(RenderChartsProperty, value); }
        }

        public static readonly DependencyProperty RenderChartsProperty =
          DependencyProperty.Register("RenderCharts", typeof(bool), typeof(PlaneChart), new PropertyMetadata(null));


        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
          DependencyProperty.Register("Header", typeof(string), typeof(PlaneChart), new PropertyMetadata(null));

        public string PrimaryAxis
        {
            get { return (string)GetValue(PrimaryAxisProperty); }
            set { SetValue(PrimaryAxisProperty, value); }
        }

        public static readonly DependencyProperty PrimaryAxisProperty =
          DependencyProperty.Register("PrimaryAxis", typeof(string), typeof(PlaneChart), new PropertyMetadata(null));

        public string SecondaryAxis
        {
            get { return (string)GetValue(SecondaryAxisProperty); }
            set { SetValue(SecondaryAxisProperty, value); }
        }

        public static readonly DependencyProperty SecondaryAxisProperty =
          DependencyProperty.Register("SecondaryAxis", typeof(string), typeof(PlaneChart), new PropertyMetadata(null));
        #endregion

        private readonly Subject<Unit> _testSub = new Subject<Unit>();

        public PlaneChart()
        {
            this.InitializeComponent();

            _testSub
                .Throttle(TimeSpan.FromMilliseconds(200))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(_=> Height = ActualWidth);
        }

        private void Chart_OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var senderChart = sender as SfChart;
            if (senderChart == null) return;

            if (senderChart.IsEnabled) senderChart.ResumeSeriesNotification();
            else senderChart.SuspendSeriesNotification();
        }

        // Binding Height="{Binding Path=ActualWidth, ElementName=ColumnDefinition}" - is not being updated when size is changed; 
        // A Workaround would be Throttle watch on size changed:
        private void PlaneChart_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _testSub.OnNext(Unit.Default);
        }
    }
}