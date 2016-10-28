using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Coordinates.UI.Components
{
    public sealed partial class PlaneChart : UserControl
    {
        #region Dependency Properties
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set
            {
                SetValue(HeaderProperty, value);
            }
        }

        public static readonly DependencyProperty HeaderProperty =
          DependencyProperty.Register("Header", typeof(string), typeof(PlaneChart), new PropertyMetadata(null, OnHeaderChanged));

        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

        public string PrimaryAxis
        {
            get { return (string)GetValue(PrimaryAxisProperty); }
            set
            {
                SetValue(PrimaryAxisProperty, value);
            }
        }

        public static readonly DependencyProperty PrimaryAxisProperty =
          DependencyProperty.Register("PrimaryAxis", typeof(string), typeof(PlaneChart), new PropertyMetadata(null, OnPrimaryAxisChanged));

        private static void OnPrimaryAxisChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }
        public string SecondaryAxis
        {
            get { return (string)GetValue(SecondaryAxisProperty); }
            set
            {
                SetValue(SecondaryAxisProperty, value);
            }
        }

        public static readonly DependencyProperty SecondaryAxisProperty =
          DependencyProperty.Register("SecondaryAxis", typeof(string), typeof(PlaneChart), new PropertyMetadata(null, OnSecondaryAxisChanged));

        private static void OnSecondaryAxisChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }
        #endregion

        public PlaneChart()
        {
            this.InitializeComponent();
        }
    }
}
