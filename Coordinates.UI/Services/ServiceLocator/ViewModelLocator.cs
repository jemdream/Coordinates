using System;
using Coordinates.UI.Helpers;
using Coordinates.UI.ViewModels.Interfaces;
using Microsoft.Practices.Unity;

namespace Coordinates.UI.Services.ServiceLocator
{
    public class ViewModelLocator
    {
        private IUnityContainer _myContainer;

        /// <summary>
        /// Setting up the container, source of services and viewmodels
        /// </summary>
        /// <param name="myContainer">Configured container</param>
        public void SetupContainer(IUnityContainer myContainer)
        {
            if (myContainer == null)
                throw new Exception(ExceptionMessages.SetupContainerException);

            _myContainer = myContainer;
        }

        //TODO 0 narson: consider moving the method below from App.xaml.cs (that is BootStrapper of our app) - which place is more appropriate
        // private static IUnityContainer SetupContainer()
        
        //TODO 1 narson: add missing viewmodels and services
        public IMainPageViewModel MainPageViewModel => _myContainer.Resolve<IMainPageViewModel>();
        public IDetailPageViewModel DetailPageViewModel => _myContainer.Resolve<IDetailPageViewModel>();
        public ISettingsPageViewModel SettingsPageViewModel => _myContainer.Resolve<ISettingsPageViewModel>();
        public ISettingsPartViewModel SettingsPartViewModel => _myContainer.Resolve<ISettingsPartViewModel>();
        public IAboutPartViewModel AboutPartViewModel => _myContainer.Resolve<IAboutPartViewModel>();
    }
}