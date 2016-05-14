using System.Windows.Input;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface IMainPageViewModel
    {
        // TODO #commands
        ICommand GotoSettings { get; set; }
        ICommand GotoPrivacy { get; set; }
        ICommand GotoAbout { get; set; }
        ICommand GotoDetailsPage { get; set; }
        string Value { get; set; }
    }
}
