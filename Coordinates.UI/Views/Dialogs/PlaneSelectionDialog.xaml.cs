using Windows.UI.Xaml.Controls;

namespace Coordinates.UI.Views.Dialogs
{
    public sealed partial class PlaneSelectionDialog : ContentDialog
    {
        public PlaneSelectionDialog(object dataContext)
        {
            DataContext = dataContext;
            this.InitializeComponent();
        }

        public PlaneSelectionDialog()
        {
            this.InitializeComponent();
        }
    }
}
