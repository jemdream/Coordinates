using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Coordinates.UI.Views.Dialogs
{
    public sealed partial class AxisMovementDialog : ContentDialog
    {
        public AxisMovementDialog(object dataContext)
        {
            DataContext = dataContext;
            this.InitializeComponent();
        }

        public AxisMovementDialog()
        {
            this.InitializeComponent();
        }
    }
}
