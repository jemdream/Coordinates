using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Coordinates.UI.Components
{
    public sealed partial class PlaneChart : UserControl
    {
        #region Dependency Properties
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
        //private static void OnSecondaryAxisChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }
        #endregion

        public PlaneChart()
        {
            this.InitializeComponent();
        }
    }
}
