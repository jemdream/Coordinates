using System.Linq;
using Windows.UI.Xaml.Controls;
using Coordinates.UI.ViewModels.MeasurementFlow;

namespace Coordinates.UI.Views
{
    public sealed partial class MeasurementsPage : Page
    {
        public MeasurementsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Supporting ViewModelBase.OnNavigated for Pivot Items
        /// </summary>
        private void Pivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newViewModel = e.AddedItems.FirstOrDefault() as MeasurementViewModelBase;

            // Update commands
            BackButton.Command = newViewModel?.GoBackCommand;
            NextButton.Command = newViewModel?.GoNextCommand;
        }
    }
}