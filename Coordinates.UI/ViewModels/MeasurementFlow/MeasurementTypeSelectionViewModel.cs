using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;
using Coordinates.Measurements;
using Coordinates.Measurements.Types;
using Coordinates.UI.Messages;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public interface IMeasurementTypeSelectionViewModel : IMeasurementViewModelBase
    {
        IEnumerable<IMeasurementMethod> AvailableMeasurementMethods { get; }
        IMeasurementMethod SelectedMeasurementMethod { get; set; }
    }

    public class MeasurementTypeSelectionViewModel : MeasurementViewModelBase, IMeasurementTypeSelectionViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IMeasurementManager _measurementManager;

        private DelegateCommand _goBackCommand;
        private DelegateCommand _goNextCommand;
        private IMeasurementMethod _selectedMeasurementMethod;

        public MeasurementTypeSelectionViewModel(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
        {
            _eventAggregator = eventAggregator;
            _measurementManager = measurementManager;

            AvailableMeasurementMethods = _measurementManager.AvailableMeasurementMethods;
        }

        public IEnumerable<IMeasurementMethod> AvailableMeasurementMethods { get; }

        public IMeasurementMethod SelectedMeasurementMethod
        {
            get { return _selectedMeasurementMethod; }
            set
            {
                Set(ref _selectedMeasurementMethod, value);
                _goNextCommand.RaiseCanExecuteChanged();
            }
        }

        public override string Title { get; } = "Tryb";
        public override ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new DelegateCommand(() =>
        {
        }, () => false));
        public override ICommand GoNextCommand => _goNextCommand ?? (_goNextCommand = new DelegateCommand(() =>
        {
            _measurementManager.SetupMeasurementMethod(SelectedMeasurementMethod);

            _eventAggregator
                .GetEvent<GoNextMeasurementMsg>()
                .Publish(GetType());

        }, () => SelectedMeasurementMethod != null));
        
        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            SelectedMeasurementMethod = _measurementManager.SelectedMeasurementMethod;

            return base.OnNavigatedToAsync(parameter, mode, state);
        }
    }
}
