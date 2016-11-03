using Windows.UI.Xaml.Controls;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface IMainPageViewModel
    {
        DelegateCommand<Page> GoTo { get; }
    }
}
