using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Coordinates.Services.Events.ConnectionEvents;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface ICodingPlaygroundViewModel
    {
        ContentDialogResult ModalPick { get; set; }
        void EnterTextBox(object textBoxContent, EventArgs ev);
        IEnumerable<DiagnosticEvent> ConnectionEvents { get; }
    }
}