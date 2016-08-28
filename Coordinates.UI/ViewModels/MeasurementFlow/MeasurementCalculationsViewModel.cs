using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using Coordinates.Measurements;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementCalculationsViewModel : IMeasurementViewModelBase
    {
        
    }
    public class MeasurementCalculationsViewModel : ViewModelBase, IMeasurementCalculationsViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMeasurementManager _measurementManager;

        private DelegateCommand _goBackCommand;
        private DelegateCommand _goNextCommand;

        public MeasurementCalculationsViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
        {
            _eventAggregator = eventAggregator;
            _measurementManager = measurementManager;
        }

        public string Title { get; } = "Obliczenia";
        public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new DelegateCommand(() =>
        {
        }, () => true));
        public ICommand GoNextCommand => _goNextCommand ?? (_goNextCommand = new DelegateCommand(() =>
        {
        }, () => true));

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
