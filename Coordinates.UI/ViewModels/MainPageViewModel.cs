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
        private DelegateCommand _gotoDetailsPage;
        private DelegateCommand _gotoSettingPage;
        private DelegateCommand _gotoPrivacy;
        private DelegateCommand _gotoAbout;

        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) // _ things for designer
            {
                Value = "Designtime value";
            }
        }

        public string Value { get { return _value; } set { Set(ref _value, value); } }


        public ICommand GotoDetailsPage =>
            _gotoDetailsPage ?? (_gotoDetailsPage = new DelegateCommand(() =>
                NavigationService.Navigate(typeof(Views.DetailPage), Value)));

        public ICommand GotoSettings =>
            _gotoSettingPage ?? (_gotoSettingPage = new DelegateCommand(() =>
                NavigationService.Navigate(typeof(Views.SettingsPage), 0)));

        public ICommand GotoPrivacy =>
            _gotoPrivacy ?? (_gotoPrivacy = new DelegateCommand(() =>
                NavigationService.Navigate(typeof(Views.SettingsPage), 1)));

        public ICommand GotoAbout =>
            _gotoAbout ?? (_gotoAbout = new DelegateCommand(() =>
                NavigationService.Navigate(typeof(Views.SettingsPage), 2)));

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
    }
}

