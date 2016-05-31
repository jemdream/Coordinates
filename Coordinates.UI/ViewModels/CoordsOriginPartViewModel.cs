using System.Collections.Generic;
using Coordinates.UI.ViewModels.Interfaces;
using Coordinates.UI.ViewModels.MeasurementViewModels;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class CoordsOriginPartViewModel : ViewModelBase, ICoordsOriginPartViewModel
    {
        public ICollection<IMeasurementTypeViewModel> MeasurementTypes { get; set; }
        public IMeasurementTypeViewModel SelectedMeasurementTypeViewModel { get; set; }

        public CoordsOriginPartViewModel()
        { 
            MeasurementTypes = new List<IMeasurementTypeViewModel>()
            {
                new FlatnessMeasurementViewModel(),
                new RoundnessMeasurementViewModel()
            };

            //SelectedMeasurementTypeViewModel = MeasurementTypes.FirstOrDefault();
        }
    }
}