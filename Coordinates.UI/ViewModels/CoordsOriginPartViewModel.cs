using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Coordinates.UI.Models;
using Coordinates.UI.ViewModels.Interfaces;
using Template10.Mvvm;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Template10.Services.NavigationService;

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
                new MeasurementTypeModel() {MeasurementName = "Pomiar okrągłości" },
                new MeasurementTypeModel() {MeasurementName = "Pomiar płaskości" },
                new MeasurementTypeModel() {MeasurementName = "Dowolny pomiar" }
            };

            //SelectedMeasurementType = MeasurementTypes.FirstOrDefault();

        }

    }
}