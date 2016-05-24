using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reactive.Linq;
using Windows.UI.Xaml.Controls;
using Coordinates.Services.Connection;
using Coordinates.Services.Events.ConnectionEvents;
using Coordinates.UI.ViewModels.Interfaces;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class CodingPlaygroundViewModel : ViewModelBase, ICodingPlaygroundViewModel
    {
        private readonly IConnectionService _mockedConnectionService;
        private readonly ObservableCollection<ConnectionEvent<ConnectionState>> _connectionEvents;

        public CodingPlaygroundViewModel(IConnectionService mockedConnectionService)
        {
            _mockedConnectionService = mockedConnectionService;

            _connectionEvents = new ObservableCollection<ConnectionEvent<ConnectionState>>();
            
            _mockedConnectionService.ConnectionMessages
                .Subscribe(message => _connectionEvents.Add(message));
            
            _mockedConnectionService.ConnectionMessages
                .OfType<DisconnectedEvent>() // jak jest disconnected
                .Subscribe(message =>
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Disconnected Event",
                        Content = string.Format("[{0}]: {1}", message.TimeStamp, message.Message),
                        PrimaryButtonText = "OK",
                        SecondaryButtonText = "Reconnect"
                    };

                    ModalPick = dialog.ShowAsync().GetResults();

                    if (ModalPick.Equals(ContentDialogResult.Secondary))
                        _mockedConnectionService.Connect();
                });

            _mockedConnectionService.Connect();
        }

        public IEnumerable<ConnectionEvent<ConnectionState>> ConnectionEvents => _connectionEvents;

        public ContentDialogResult ModalPick { get; set; }

        // _ minusem jest podawanie nazwy metody (przez behaviour) zamiast bindowania
        public void EnterTextBox(object textBoxContent, EventArgs ev)
        {
            // ignoring everything, simple trigger
        }

        #region Navigation
        #endregion
    }
}
