using System;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml;
using Coordinates.UI.Services;
using Coordinates.UI.Services.SettingsServices;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public interface ISettingsPartViewModel
    {
        bool UseShellBackButton { get; }
        bool UseLightThemeButton { get; }
        bool UseFullScreenButton { get; }
        ICommand OpenLoggingFolder { get; }
    }

    public class SettingsPartViewModel : ViewModelBase, ISettingsPartViewModel
    {
        private readonly ISettingsService _settingsService;
        private readonly IFileLogger _fileLogger;
        private ICommand _openLoggingFolder;

        public SettingsPartViewModel(ISettingsService settingsService, IFileLogger fileLogger)
        {
            _settingsService = settingsService;
            _fileLogger = fileLogger;
        }

        public bool UseShellBackButton
        {
            get { return _settingsService.UseShellBackButton; }
            set { _settingsService.UseShellBackButton = value; RaisePropertyChanged(); }
        }

        public bool UseLightThemeButton
        {
            get { return _settingsService.AppTheme.Equals(ApplicationTheme.Light); }
            set { _settingsService.AppTheme = value ? ApplicationTheme.Light : ApplicationTheme.Dark; RaisePropertyChanged(); }
        }

        public bool UseFullScreenButton
        {
            get { return _settingsService.UseFullScreen; }
            set { _settingsService.UseFullScreen = value; RaisePropertyChanged(); }
        }

        public ICommand OpenLoggingFolder => _openLoggingFolder ?? (_openLoggingFolder = new AwaitableDelegateCommand(async x =>
        {
            await Launcher.LaunchFolderAsync(_fileLogger.LoggingFolder);
        }));
    }
}