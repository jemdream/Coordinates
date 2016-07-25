using Coordinates.UI.Messages;
using Coordinates.UI.Models;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class CoordsMeasurementPartViewModel : ViewModelBase, ICoordsMeasurementPartViewModel
    {
        private MeasurementSettingsModel _measurementSettings;

        public CoordsMeasurementPartViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator
                .GetEvent<NewMeasurementMessage>()
                .Subscribe(ChangeMeasurementType);
        }

        // TODO: zrobic taska na zamiane przekazywania instancji VM na enuma, 
        // TODO: a w View bedzie mapowanie po typie enuma i wyciagana nowa instancja z containera za kazdym razem jak odpalamy nowy measurement
        private void ChangeMeasurementType(MeasurementSettingsModel settings)
        {
            MeasurementSettings = settings;
        }

        public MeasurementSettingsModel MeasurementSettings
        {
            get { return _measurementSettings; }
            set { Set(ref _measurementSettings, value); }
        }
    }
}