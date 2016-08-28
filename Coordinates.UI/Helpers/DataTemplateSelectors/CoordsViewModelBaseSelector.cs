using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Coordinates.UI.ViewModels.MeasurementFlow;

namespace Coordinates.UI.Helpers.DataTemplateSelectors
{
    public class CoordsViewModelBaseSelector : DataTemplateSelector
    {
        public DataTemplate TypeSelectionDataTemplate { get; set; }
        public DataTemplate CalibrationDataTemplate { get; set; }
        public DataTemplate ProcessDataTemplate { get; set; }
        public DataTemplate SelectionDataTemplate { get; set; }
        public DataTemplate CalculationsDataTemplate { get; set; }
        
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is MeasurementTypeSelectionViewModel)
                return TypeSelectionDataTemplate;
            if (item is MeasurementCalibrationViewModel)
                return CalibrationDataTemplate;
            if (item is MeasurementProcessViewModel)
                return ProcessDataTemplate;
            if (item is MeasurementSelectionViewModel)
                return SelectionDataTemplate;
            if (item is MeasurementCalculationsViewModel)
                return CalculationsDataTemplate;

            return base.SelectTemplateCore(item, container);
        }
    }
}
