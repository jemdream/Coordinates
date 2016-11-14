using System;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Template10.Common;
using Template10.Utils;
using Windows.UI.Xaml;
using Template10.Services.SettingsService;

namespace Coordinates.UI.Services.SettingsServices
{
    /// <summary>
    /// UI/UX Settings
    /// </summary>
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsHelper _helper;

        public SettingsService()
        {
            _helper = new SettingsHelper();
        }

        public bool UseShellBackButton
        {
            get { return _helper.Read<bool>(nameof(UseShellBackButton), true); }
            set
            {
                _helper.Write(nameof(UseShellBackButton), value);
                BootStrapper.Current.NavigationService.Dispatcher.Dispatch(() =>
                {
                    BootStrapper.Current.ShowShellBackButton = value;
                    BootStrapper.Current.UpdateShellBackButton();
                    BootStrapper.Current.NavigationService.Refresh();
                });
            }
        }

        public bool UseFullScreen
        {
            get { return _helper.Read<bool>(nameof(UseFullScreen), true); }
            set
            {
                _helper.Write(nameof(UseFullScreen), value);
                if (value) SetupFullScreen();
                else SetupWindow();
            }
        }

        public ApplicationTheme AppTheme
        {
            get
            {
                var theme = ApplicationTheme.Light;
                var value = _helper.Read<string>(nameof(AppTheme), theme.ToString());
                return Enum.TryParse<ApplicationTheme>(value, out theme) ? theme : ApplicationTheme.Dark;
            }
            set
            {
                _helper.Write(nameof(AppTheme), value.ToString());
                (Window.Current.Content as FrameworkElement).RequestedTheme = value.ToElementTheme();
                Views.Shell.HamburgerMenu.RefreshStyles(value);
            }
        }

        public TimeSpan CacheMaxDuration
        {
            get { return _helper.Read<TimeSpan>(nameof(CacheMaxDuration), TimeSpan.FromDays(2)); }
            set
            {
                _helper.Write(nameof(CacheMaxDuration), value);
                BootStrapper.Current.CacheMaxDuration = value;
            }
        }

        private static void SetupFullScreen()
        {
            // Setup type of window
            var view = ApplicationView.GetForCurrentView();
            if (!view.IsFullScreenMode) view.TryEnterFullScreenMode();

            var size = new Size(1366, 768);

            view.SetPreferredMinSize(size);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
        }

        private static void SetupWindow()
        {
            // Setup type of window
            var view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode) view.ExitFullScreenMode();
            
            var size = new Size(1366, 768);

            view.SetPreferredMinSize(size);
            ApplicationView.PreferredLaunchViewSize = size;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }
    }
}