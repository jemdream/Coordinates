using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Coordinates.UI.ViewModels.Interfaces;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class CodingPlaygroundViewModel : ViewModelBase, ICodingPlaygroundViewModel
    {
        private ContentDialogResult _initialModalPick;

        public string InitialModalPick => _initialModalPick.ToString();

        // _ minusem jest podawanie nazwy metody (przez behaviour) zamiast bindowania
        public void EnterTextBox(object textBoxContent, EventArgs ev)
        {
            
        }

        #region Navigation
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            // _ how to show modal:
            var dlg = new ContentDialog
            {
                Title = "Title",
                Content = "Content",
                PrimaryButtonText = "Apply",
                SecondaryButtonText = "Cancel"
            };

            // _ when you pick primary or secondary button, the value is stored below
            _initialModalPick = await dlg.ShowAsync();
            RaisePropertyChanged(() => InitialModalPick);

            #region Ignore
            //let message = result match(
            //    case Success < string > success: success.Result
            //    case Failure err: err.Message
            //    case *: "Unknown!"
            //);
            #endregion
        }
        #endregion
    }
}
