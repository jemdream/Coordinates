using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Coordinates.UI.Views
{
    public sealed partial class SettingsPage : Page
    {
        private readonly Template10.Services.SerializationService.ISerializationService _serializationService;

        public SettingsPage()
        {
            InitializeComponent();
            _serializationService = Template10.Services.SerializationService.SerializationService.Json;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var index = int.Parse(_serializationService.Deserialize(e.Parameter?.ToString()).ToString());
            MyPivot.SelectedIndex = index;
        }
    }
}
