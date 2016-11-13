using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Template10.Mvvm;
using Windows.UI.Xaml.Controls;
using Coordinates.UI.ViewModels.Interfaces;
using Template10.Services.NavigationService;

namespace Coordinates.UI.ViewModels
{
    public class MainPageViewModel : ViewModelBase, IMainPageViewModel
    {
        private DelegateCommand<Page> _goTo;
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public DelegateCommand<Page> GoTo => _goTo ??
            (_goTo = new DelegateCommand<Page>(async page =>
            {
                await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => _navigationService.Navigate(page.GetType()));
            }));
    }
}