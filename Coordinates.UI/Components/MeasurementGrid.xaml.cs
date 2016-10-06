using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Coordinates.Models.DTO;
using Coordinates.UI.ViewModels.MeasurementViewModels;
using Microsoft.Practices.ObjectBuilder2;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Coordinates.UI.Components
{
    public sealed partial class MeasurementGrid : UserControl
    {
        #region Dependency Properties
        public bool IsSelectable
        {
            get { return (bool)GetValue(IsSelectableProperty); }
            set
            {
                SetValue(IsSelectableProperty, value);
            }
        }

        public static readonly DependencyProperty IsSelectableProperty =
          DependencyProperty.Register("IsSelectable", typeof(bool), typeof(MeasurementGrid), new PropertyMetadata(null, OnIsSelectableChanged));

        private static void OnIsSelectableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }
        #endregion

        public MeasurementGrid()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var added = e.AddedItems
                .Select(x => x as Position)
                .ToList();

            var removed = e.RemovedItems
                .Select(x => x as Position)
                .ToList();

            var listView = (ListView)sender;

            var selectedPositionsVm = ((IElementViewModel)listView.DataContext).SelectedPositions;

            added
                .Where(position => !selectedPositionsVm.Contains(position))
                .ForEach(position => selectedPositionsVm.Add(position));

            removed
                .Where(position => selectedPositionsVm.Contains(position))
                .ForEach(position => selectedPositionsVm.Remove(position));
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            var vm = (IElementViewModel)listView.DataContext;

            if (vm == null) return;

            var selectedPositionsVm = vm.SelectedPositions;
            
            if (!IsSelectable || !listView.Items.Any()) return;

            // workaround for not updating bindings before this callback is invoked
            listView.SelectionMode = ListViewSelectionMode.Multiple;

            // Get elements from UI, match with VM items, get the indexes, and select with SelectRange
            listView.Items
                .Select((n, i) => new {Value = n, Index = i})
                .Where(vi => selectedPositionsVm.Contains(vi.Value))
                .Select(vi => vi.Index)
                .ForEach(index => listView.SelectRange(new ItemIndexRange(index, 1)));
        }
    }
}
