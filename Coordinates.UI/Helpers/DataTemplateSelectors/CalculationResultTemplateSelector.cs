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
        
        public DataTemplate OneHoleResultDataTemplate { get; set; }
        public DataTemplate TwoHolesResultDataTemplate { get; set; }

        public DataTemplate SurfaceParalellismResultDataTemplate { get; set; }
        public DataTemplate SurfacePerpendicularityResultDataTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (!(item is ICalculationResult))
                return base.SelectTemplateCore(item, container);

            if (item is HoleResult)
                return HoleResultDataTemplate;

            if (item is SurfaceResult)
                return SurfaceResultDataTemplate;

            if (item is ErrorResult)
                return ErrorResultDataTemplate;

            if (item is SurfaceParalellismResult)
                return SurfaceParalellismResultDataTemplate;

            if (item is SurfacePerpendicularityResult)
                return SurfacePerpendicularityResultDataTemplate;

            if (item is OneHoleResult)
                return OneHoleResultDataTemplate;

            if (item is TwoHolesResult)
                return TwoHolesResultDataTemplate;

            return base.SelectTemplateCore(item, container);
        }
    }
}
