using System;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface IAboutPartViewModel
    {
        Uri Logo { get; }
        string DisplayName { get; }
        string Publisher { get; }
        string Version { get; }
        Uri GitLink { get; }
    }
}