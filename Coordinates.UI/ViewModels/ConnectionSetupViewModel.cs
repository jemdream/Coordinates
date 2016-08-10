using System;
using System.Windows.Input;
using Coordinates.ExternalDevices;
using Coordinates.ExternalDevices.Connections;
using Coordinates.Measurements;
using Coordinates.Models.DTO;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class ConnectionSetupViewModel : ViewModelBase, IConnectionSetupViewModel
    {
        private readonly IConnectionService _connectionService;
        private readonly IMeasurementManager _measurementManager;

        private ICommand _connectCommand;
        private ICommand _disconnectCommand;
        private ConnectionStatus _connectionStatus;
        private Position _position = new GaugePosition();
        private object _bColor;

        public ConnectionSetupViewModel(IConnectionService connectionService, IMeasurementManager measurementManager)
        {
            _connectionService = connectionService;
            _measurementManager = measurementManager;

            _measurementManager.PositionSource
                .Subscribe(pos => { Position = pos; });

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

        public Position Position
        {
            get { return _position; }
            private set { Set(ref _position, value); }
        }
    }
}