using System;
using System.Collections.Generic;
using Coordinates.Services.Args;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface ICodingPlaygroundViewModel
    {
        string InitialModalPick { get; }
        void EnterTextBox(object textBoxContent, EventArgs ev);
        IEnumerable<ConnectionEvent> ConnectionEvents { get; }
    }
}