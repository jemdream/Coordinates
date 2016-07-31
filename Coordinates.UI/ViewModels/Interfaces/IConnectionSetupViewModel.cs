using System.Windows.Input;
using Coordinates.ExternalDevices;

namespace Coordinates.UI.ViewModels
{
    public interface IConnectionSetupViewModel
    {
        ICommand ConnectCommand { get; }
        ICommand DisconnectCommand { get; }
        ConnectionStatus ConnectionStatus { get; set; }
    }
}