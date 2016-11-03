using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Coordinates.UI.Components
{
    public class AlternatingRowListView : ListView
    {
        public static readonly DependencyProperty OddRowBackgroundProperty =
            DependencyProperty.Register("OddRowBackground", typeof(Brush), typeof(AlternatingRowListView), null);
        public Brush OddRowBackground
        {
            get { return (Brush)GetValue(OddRowBackgroundProperty); }
            set { SetValue(OddRowBackgroundProperty, (Brush)value); }
        }

        public static readonly DependencyProperty EvenRowBackgroundProperty =
            DependencyProperty.Register("EvenRowBackground", typeof(Brush), typeof(AlternatingRowListView), null);
        public Brush EvenRowBackground
        {
            get { return (Brush)GetValue(EvenRowBackgroundProperty); }
            set { SetValue(EvenRowBackgroundProperty, (Brush)value); }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            var listViewItem = element as ListViewItem;

            if (listViewItem == null) return;

            if (Items == null) return;

            var test = IndexFromContainer(element);

            var index = SelectionMode != ListViewSelectionMode.None ? test : Items.Count + 1;

            if ((index + 1) % 2 == 1)
            {
                listViewItem.Background = OddRowBackground;
            }
            else
            {
                listViewItem.Background = EvenRowBackground;
            }
        }
    }
}