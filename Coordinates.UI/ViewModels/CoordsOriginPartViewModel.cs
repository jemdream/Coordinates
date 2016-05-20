using System.Collections.Generic;
using Coordinates.UI.Models;
using Coordinates.UI.ViewModels.Interfaces;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class CoordsOriginPartViewModel : ViewModelBase, ICoordsOriginPartViewModel
    {
        public ICollection<MeasurementTypeModel> MeasurementTypes { get; set; }
        public MeasurementTypeModel SelectedMeasurementType { get; set; }

        public CoordsOriginPartViewModel()
        {
            MeasurementTypes = new List<MeasurementTypeModel>()
            {
                new MeasurementTypeModel() {MeasurementName = "OPTION 1" },
                new MeasurementTypeModel() {MeasurementName = "OPTION 2" },
                new MeasurementTypeModel() {MeasurementName = "OPTION 3" }
            };
        }

    }
}