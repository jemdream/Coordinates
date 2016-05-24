using System;
using System.Collections.Generic;
using System.Data;
using Windows.UI.Xaml.Controls;
using Coordinates.Services.Events.ConnectionEvents;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface ICodingPlaygroundViewModel
    {
        ContentDialogResult ModalPick { get; }
        void EnterTextBox(object textBoxContent, EventArgs ev);
        IEnumerable<ConnectionEvent<ConnectionState>> ConnectionEvents { get; }
    }
}