using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using Coordinates.Measurements;
using Coordinates.UI.Messages;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class CoordsMeasurementPartViewModel : MeasurementViewModelBase, ICoordsMeasurementPartViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMeasurementManager _measurementManager;
        private DelegateCommand _goBackCommand;
        private DelegateCommand _goNextCommand;

        public CoordsMeasurementPartViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
        {
            _eventAggregator = eventAggregator;
            _measurementManager = measurementManager;
        }

        private void InitializeNewMeasurement()
        {
            
        }

        public override ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new DelegateCommand(() =>
        {
            // TODO WARN that all data is wiped!
            _eventAggregator
                .GetEvent<GoBackMeasurementMsg>()
                .Publish(typeof(ICoordsMeasurementPartViewModel));

        }, () => true));

        public override ICommand GoNextCommand => _goNextCommand ?? (_goNextCommand = new DelegateCommand(() =>
        {
            _eventAggregator
                .GetEvent<GoNextMeasurementMsg>()
                .Publish(typeof(ICoordsMeasurementPartViewModel));

        }, () => false)); // TODO change false to condition checking

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            InitializeNewMeasurement();
            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}