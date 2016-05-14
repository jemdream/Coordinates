using Template10.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using Coordinates.UI.ViewModels.Interfaces;

namespace Coordinates.UI.ViewModels
{
    public class MainPageViewModel : ViewModelBase, IMainPageViewModel
    {
        private string _value = "Gas";

        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) // _ things for designer
            {
                Value = "Designtime value";
            }

            SetupCommands();
        }

        public string Value { get { return _value; } set { Set(ref _value, value); } }

        // TODO #commands
        public ICommand GotoDetailsPage { get; set; }
        public ICommand GotoSettings { get; set; }
        public ICommand GotoPrivacy { get; set; }
        public ICommand GotoAbout { get; set; }

        #region Navigation
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any()) Value = suspensionState[nameof(Value)]?.ToString();
            
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending) suspensionState[nameof(Value)] = Value;
            
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;

            await Task.CompletedTask;
        }
        #endregion

        private void SetupCommands()
        {
            GotoDetailsPage = new DelegateCommand(() => NavigationService.Navigate(typeof(Views.DetailPage), Value));
            GotoSettings = new DelegateCommand(() => NavigationService.Navigate(typeof(Views.SettingsPage), 0));
            GotoPrivacy = new DelegateCommand(() => NavigationService.Navigate(typeof(Views.SettingsPage), 1));
            GotoAbout = new DelegateCommand(() => NavigationService.Navigate(typeof(Views.SettingsPage), 2));
        }
    }
}

