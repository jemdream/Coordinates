using System.Windows.Input;

namespace Coordinates.UI.ViewModels
{
    public interface IConnectionSetupViewModel
    {
        ICommand ShuffleCommand { get; }
        string SomeTestBinding { get; set; }
    }
}