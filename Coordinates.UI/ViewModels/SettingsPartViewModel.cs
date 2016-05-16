using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Coordinates.UI.Services.SettingsServices;
using Coordinates.UI.ViewModels.Interfaces;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class SettingsPartViewModel : ViewModelBase, ISettingsPartViewModel
    {
        private readonly ISettingsService _settingsService;
        private DelegateCommand _showBusyCommand;
        private string _busyText = "Please wait...";

        public SettingsPartViewModel(ISettingsService settingsService)
        {
            // if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            _settingsService = settingsService;
        }

        #region _ Some example bindings and visual settings
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

        public string BusyText
        {
            get { return _busyText; }
            set
            {
                Set(ref _busyText, value);
                _showBusyCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        // TODO #commands
        // _ if _showBusyCommand is null, then create new delegatecommand, assign to _showBusyCommand and return it
        public ICommand ShowBusyCommand
            => _showBusyCommand ?? (_showBusyCommand = new DelegateCommand(async () =>
            {
                Views.Busy.SetBusy(true, _busyText);
                await Task.Delay(5000);
                Views.Busy.SetBusy(false);
            }, () => !string.IsNullOrEmpty(BusyText)));
    }
}
