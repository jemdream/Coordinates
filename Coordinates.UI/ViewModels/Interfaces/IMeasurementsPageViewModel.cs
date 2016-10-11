using System.Collections.Generic;
using Prism.Events;

namespace Coordinates.UI.ViewModels.Interfaces
{
    public interface IMeasurementsPageViewModel
    {
        IEventAggregator EventAggregator { get; }
        List<IMeasurementViewModelBase> MeasurementFlowViewModels { get; }
        int SelectedTabIndex { get; set; }

        //MockDeviceService MockingDataService { get; set; } // TODO MOCK CONNECTION
    }
}