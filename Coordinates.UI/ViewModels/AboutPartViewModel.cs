using System;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public interface IAboutPartViewModel
    {
        Uri Logo { get; }
        string DisplayName { get; }
        string Publisher { get; }
        string Version { get; }
        Uri GitLink { get; }
    }

    public class AboutPartViewModel : ViewModelBase, IAboutPartViewModel
    {
        public Uri Logo => Windows.ApplicationModel.Package.Current.Logo;

        public string DisplayName => Windows.ApplicationModel.Package.Current.DisplayName;

        public string Publisher => Windows.ApplicationModel.Package.Current.PublisherDisplayName;

        public string Version
        {
            get
            {
                var v = Windows.ApplicationModel.Package.Current.Id.Version;
                return $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
            }
        }

        public Uri GitLink => new Uri("https://github.com/jemdream");
    }
}
