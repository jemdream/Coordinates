using System;
using System.Windows.Input;
using Coordinates.ExternalDevices;
using Coordinates.ExternalDevices.Connections;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class ConnectionSetupViewModel : ViewModelBase, IConnectionSetupViewModel
    {
        private readonly IConnectionService _connectionService;

        private ICommand _connectCommand;
        private ICommand _disconnectCommand;
        private ConnectionStatus _connectionStatus;

        public ConnectionSetupViewModel(IConnectionService connectionService)
        {
            _connectionService = connectionService;

            _connectionService.DiagnosticEventsStream
                .Subscribe(dE => { ConnectionStatus = (ConnectionStatus)dE.Message; });
        }

        public ICommand ConnectCommand => _connectCommand ?? (_connectCommand = new DelegateCommand(async () =>
        {
            await _connectionService.Open();
        }));

        public ICommand DisconnectCommand => _disconnectCommand ?? (_disconnectCommand = new DelegateCommand(async () =>
        {
            await _connectionService.Close();
        }));

        public ConnectionStatus ConnectionStatus
        {
            get { return _connectionStatus; }
            set { Set(ref _connectionStatus, value); }
        }
    }
}