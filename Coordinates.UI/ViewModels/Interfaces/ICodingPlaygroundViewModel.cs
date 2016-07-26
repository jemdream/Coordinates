using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Coordinates.ExternalDevices.Events.ConnectionEvents;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface ICodingPlaygroundViewModel
    {
        ContentDialogResult ModalPick { get; set; }
        void EnterTextBox(object textBoxContent, EventArgs ev);
        // TODO EXTERNALS
        IEnumerable<DiagnosticEvent> ConnectionEvents { get; }
    }
}