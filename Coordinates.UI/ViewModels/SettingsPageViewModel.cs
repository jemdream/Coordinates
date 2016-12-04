using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public interface ISettingsPageViewModel
    {
        ISettingsPartViewModel SettingsPartViewModel { get; }
        IAboutPartViewModel AboutPartViewModel { get; }
    }

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