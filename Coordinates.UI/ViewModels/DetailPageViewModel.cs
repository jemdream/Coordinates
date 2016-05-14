using System.Collections.Generic;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using Coordinates.UI.ViewModels.Interfaces;

namespace Coordinates.UI.ViewModels
{
    public class DetailPageViewModel : ViewModelBase, IDetailPageViewModel
    {
        private string _value = "Default";

        public DetailPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        public string Value { get { return _value; } set { Set(ref _value, value); } }

        #region Navigation
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            string test;

            const string t = nameof(Value);

            if (suspensionState.ContainsKey(t))
            {
                test = suspensionState[t]?.ToString();
            }
            else
            {
                test = parameter?.ToString();
            }

            Value = test;

            await Task.CompletedTask;
        }
        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
            }
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

