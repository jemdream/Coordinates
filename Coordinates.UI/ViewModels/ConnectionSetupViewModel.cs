using System;
using System.Windows.Input;
using Coordinates.DataSources;
using Coordinates.DataSources.Connections;
using Coordinates.Measurements;
using Coordinates.Models.DTO;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public interface IConnectionSetupViewModel
    {
        ICommand ConnectCommand { get; }
        ICommand DisconnectCommand { get; }
        ConnectionStatus ConnectionStatus { get; set; }
        Position Position { get; }
    }
    public class ConnectionSetupViewModel : ViewModelBase, IConnectionSetupViewModel
    {
        private readonly IConnectionService _connectionService;
        private ConnectionStatus _connectionStatus;
        private Position _position = new Position();

        private ICommand _connectCommand;
        private ICommand _disconnectCommand;

        public ConnectionSetupViewModel(IConnectionService connectionService, IMeasurementManager measurementManager)
        {
            _connectionService = connectionService;

            measurementManager.PositionSource
                .Subscribe(pos => { Position = pos; });

            _connectionService.DiagnosticEventsStream
                .Subscribe(dE => { ConnectionStatus = (ConnectionStatus)dE.Message; });

            ConnectCommand.Execute(null);
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

        public Position Position
        {
            get { return _position; }
            private set { Set(ref _position, value); }
        }
    }
}