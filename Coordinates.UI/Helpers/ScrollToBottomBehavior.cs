using System;
using System.Collections.Specialized;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;
using Syncfusion.UI.Xaml.Controls.Navigation;

namespace Coordinates.UI.Helpers
{
    public class ScrollToBottomBehavior : DependencyObject, IBehavior
    {
        public DependencyObject AssociatedObject { get; private set; }

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object),
            typeof(ScrollToBottomBehavior),
            new PropertyMetadata(null, ItemsSourcePropertyChanged));

        private static void ItemsSourcePropertyChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as ScrollToBottomBehavior;
            if (behavior.AssociatedObject == null || e.NewValue == null) return;

            var collection = behavior.ItemsSource as INotifyCollectionChanged;
            if (collection != null)
            {
                collection.CollectionChanged += (s, args) =>
                {
                    var scrollViewer = behavior.AssociatedObject.GetVisualDescendants().OfType<ScrollViewer>().FirstOrDefault();
                    scrollViewer.ChangeView(null, scrollViewer.ScrollableHeight, null);
                };
            }
        }

        public void Attach(DependencyObject associatedObject)
        {
            var control = associatedObject as ListView;
            if (control == null)
                throw new ArgumentException(
                    "ScrollToBottomBehavior can be attached only to ListView.");

            AssociatedObject = associatedObject;
        }

        public void Detach()
        {
            AssociatedObject = null;
        }
    }
}
