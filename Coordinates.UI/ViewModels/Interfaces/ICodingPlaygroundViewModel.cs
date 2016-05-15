using System;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface ICodingPlaygroundViewModel
    {
        string InitialModalPick { get; }
        void EnterTextBox(object textBoxContent, EventArgs ev);
    }
}