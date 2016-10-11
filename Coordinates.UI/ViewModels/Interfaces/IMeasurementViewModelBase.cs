using System.Windows.Input;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface IMeasurementViewModelBase
    {
        string Title { get; }
        ICommand GoBackCommand { get; }
        ICommand GoNextCommand { get; }
    }
}
