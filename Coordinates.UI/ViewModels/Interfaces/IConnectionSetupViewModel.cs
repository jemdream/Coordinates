using System.Windows.Input;
using Coordinates.ExternalDevices;
using Coordinates.Models.DTO;

namespace Coordinates.UI.ViewModels
{
    public interface IConnectionSetupViewModel
    {
        ICommand ConnectCommand { get; }
        ICommand DisconnectCommand { get; }
        ConnectionStatus ConnectionStatus { get; set; }
        Position Position { get; }
    }
}