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
        private readonly ObservableCollection<DiagnosticEvent> _connectionEvents;
        private ContentDialogResult _modalPick;

        public CodingPlaygroundViewModel(IConnectionService mockedConnectionService)
        {
            _mockedConnectionService = mockedConnectionService;

            _connectionEvents = new ObservableCollection<DiagnosticEvent>();

            _mockedConnectionService.DiagnosticEventsStream
                .Subscribe(message => _connectionEvents.Add(message));

            _mockedConnectionService.DiagnosticEventsStream
                .Where(message =>
                {
                    var test = (ConnectionState)message.Message;

                    return test.Equals(ConnectionState.Open);
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

                    dialog.Closed += (sender, args) =>
                    {
                        if (args.Result.Equals(ContentDialogResult.Secondary))
                            _mockedConnectionService.Close();

                        ModalPick = args.Result;
                    };

                    dialog.ShowAsync().GetResults();
                });

            _mockedConnectionService.Open();
        }

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
        }

        #region Navigation
        #endregion
    }
}
