using System.Windows.Input;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface IMainPageViewModel
    {
        ICommand GotoDetailsPage { get; }
        ICommand GotoSettings { get; }
        ICommand GotoPrivacy { get; }
        ICommand GotoAbout { get; }
        string Value { get; set; }
    }
}
