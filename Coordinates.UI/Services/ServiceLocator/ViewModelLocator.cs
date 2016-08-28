using System;
using Coordinates.UI.Helpers;
using Coordinates.UI.ViewModels;
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
        
        public IMainPageViewModel MainPageViewModel => _myContainer.Resolve<IMainPageViewModel>();
        public IDetailPageViewModel DetailPageViewModel => _myContainer.Resolve<IDetailPageViewModel>();
        public ISettingsPageViewModel SettingsPageViewModel => _myContainer.Resolve<ISettingsPageViewModel>();
        public ISettingsPartViewModel SettingsPartViewModel => _myContainer.Resolve<ISettingsPartViewModel>();
        public IAboutPartViewModel AboutPartViewModel => _myContainer.Resolve<IAboutPartViewModel>();
        public ICodingPlaygroundViewModel CodingPlaygroundViewModel => _myContainer.Resolve<ICodingPlaygroundViewModel>();
        public IMeasurementsPageViewModel MeasurementsPageViewModel => _myContainer.Resolve<IMeasurementsPageViewModel>();
        public IVisualisationPageViewModel VisualisationPageViewModel => _myContainer.Resolve<IVisualisationPageViewModel>();
        public IConnectionSetupViewModel ConnectionSetupViewModel => _myContainer.Resolve<IConnectionSetupViewModel>();
    }
}