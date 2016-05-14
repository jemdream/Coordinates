using Template10.Mvvm;
using Coordinates.UI.ViewModels.Interfaces;

namespace Coordinates.UI.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase, ISettingsPageViewModel
    {
        public SettingsPageViewModel(ISettingsPartViewModel settingsPartViewModel, IAboutPartViewModel aboutPartViewModel)
        {
            SettingsPartViewModel = settingsPartViewModel;
            AboutPartViewModel = aboutPartViewModel;
        }

        public ISettingsPartViewModel SettingsPartViewModel { get; }
        public IAboutPartViewModel AboutPartViewModel { get; }
    }
}

