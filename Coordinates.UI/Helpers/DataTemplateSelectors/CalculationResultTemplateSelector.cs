using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Coordinates.Measurements.Models;

namespace Coordinates.UI.Helpers.DataTemplateSelectors
{
    public class CalculationResultTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ErrorResultDataTemplate { get; set; }
        public DataTemplate HoleResultDataTemplate { get; set; }
        public DataTemplate SurfaceResultDataTemplate { get; set; }
        public DataTemplate RadianResultDataTemplate { get; set; }
        public DataTemplate TwoHolesResultDataTemplate { get; set; }
        public DataTemplate DefaultResultDataTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is HoleResult && HoleResultDataTemplate != null)
                return HoleResultDataTemplate;

            if (item is SurfaceResult && SurfaceResultDataTemplate != null)
                return SurfaceResultDataTemplate;

            if (item is ErrorResult && ErrorResultDataTemplate != null)
                return ErrorResultDataTemplate;

            if (item is TwoHolesResult && TwoHolesResultDataTemplate != null)
                return TwoHolesResultDataTemplate;

            if (item is SurfacePerpendicularityResult || item is SurfaceParalellismResult && RadianResultDataTemplate != null)
                return RadianResultDataTemplate;

            if (item is ICalculationResult && DefaultResultDataTemplate != null)
                return DefaultResultDataTemplate;

            return base.SelectTemplateCore(item, container);
        }
    }
}
