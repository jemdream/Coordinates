namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface ISettingsPageViewModel
    {
        ISettingsPartViewModel SettingsPartViewModel { get; }
        IAboutPartViewModel AboutPartViewModel { get; }
    }
}