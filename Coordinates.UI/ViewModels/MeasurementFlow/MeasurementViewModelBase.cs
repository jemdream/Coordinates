using System.Windows.Input;
using Coordinates.UI.ViewModels.Interfaces;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public abstract class MeasurementViewModelBase : ViewModelBase, IMeasurementViewModelBase
    {
        public abstract string Title { get; }
        public abstract ICommand GoBackCommand { get; }
        public abstract ICommand GoNextCommand { get; }
    }
}
