using Template10.Mvvm;
using Windows.UI.Xaml.Controls;
using Coordinates.UI.ViewModels.Interfaces;

namespace Coordinates.UI.ViewModels
{
    public class MainPageViewModel : ViewModelBase, IMainPageViewModel
    {
        private DelegateCommand<Page> _goTo;
        public DelegateCommand<Page> GoTo => _goTo ?? 
            (_goTo = new DelegateCommand<Page>(page => NavigationService.Navigate(page.GetType())));
    }
}