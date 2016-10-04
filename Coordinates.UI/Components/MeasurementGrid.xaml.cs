using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Coordinates.Measurements.Elements;
using Coordinates.Models.DTO;
using Coordinates.UI.ViewModels.MeasurementViewModels;
using Microsoft.Practices.ObjectBuilder2;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Coordinates.UI.Components
{
    public sealed partial class MeasurementGrid : UserControl
    {
        public MeasurementGrid()
        {
            InitializeComponent();
        }

        // TODO temporary solution - create "Attached Property" for control and place this functionality there
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

        // Refreshing selected elements
        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            var vm = (IElementViewModel)listView.DataContext;

            if (vm == null) return;

            var selectedPositionsVm = vm.SelectedPositions;

            // TODO or instead - tweak here
            // Get elements from UI, match with VM items, get the indexes, and select with SelectRange
            if (listView.Items.Any())
                listView.Items
                    .Select((n, i) => new { Value = n, Index = i })
                    .Where(vi => selectedPositionsVm.Contains(vi.Value))
                    .Select(vi => vi.Index)
                    .ForEach(index => listView.SelectRange(new ItemIndexRange(index, 1)));
        }
    }
}
