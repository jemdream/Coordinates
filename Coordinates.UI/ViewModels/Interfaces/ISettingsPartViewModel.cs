using System.Windows.Input;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface ISettingsPartViewModel
    {
        bool UseShellBackButton { get; }
        bool UseLightThemeButton { get; }
        string BusyText { get; }
        ICommand ShowBusyCommand { get; }
    }
}
