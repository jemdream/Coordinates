using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Windows.UI.Xaml.Controls;
using Coordinates.ExternalDevices;
using Coordinates.ExternalDevices.Connections;
using Coordinates.ExternalDevices.Events.ConnectionEvents;
using Coordinates.UI.ViewModels.Interfaces;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class CodingPlaygroundViewModel : ViewModelBase, ICodingPlaygroundViewModel
    {
        private readonly IConnectionService _connectionService;
        private readonly ObservableCollection<DiagnosticEvent> _connectionEvents;
        private ContentDialogResult _modalPick;

        public CodingPlaygroundViewModel(IConnectionService connectionService)
        {
            _connectionService = connectionService;

            _connectionEvents = new ObservableCollection<DiagnosticEvent>();

            _connectionService.DiagnosticEventsStream
                .Subscribe(message => _connectionEvents.Add(message));

            _connectionService.DiagnosticEventsStream
                .Where(message =>
                {
                    var test = (ConnectionStatus)message.Message;

                    return test.Equals(ConnectionStatus.Open);
                })
                .Subscribe(message =>
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Connection opened.",
                        Content = string.Format("[{0}]: {1}", message.TimeStamp, message.Message),
                        PrimaryButtonText = "OK",
                        SecondaryButtonText = "Close Connection"
                    };

                    dialog.Closed += OnDialogOnClosed;

                    dialog.ShowAsync().GetResults();
                });

            OpenConnection();
        }

        private void OpenConnection()
        {
            if (_connectionService.ConnectionStatus.Equals(ConnectionStatus.Open))
                _connectionService.Close();
            else
                _connectionService.Open();
        }

        private void OnDialogOnClosed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            if (args.Result.Equals(ContentDialogResult.Secondary))
                _connectionService.Close();

            ModalPick = args.Result;

            sender.Closed -= OnDialogOnClosed;
        }

        // TODO EXTERNALS
        public IEnumerable<DiagnosticEvent> ConnectionEvents => _connectionEvents;

        public ContentDialogResult ModalPick
        {
            get { return _modalPick; }
            set { Set(ref _modalPick, value); }
        }

        // _ minusem jest podawanie nazwy metody (przez behaviour) zamiast bindowania
        public void EnterTextBox(object textBoxContent, EventArgs ev)
        {
            // ignoring everything, simple trigger
            OpenConnection();
        }

        #region Navigation
        #endregion
    }
}
