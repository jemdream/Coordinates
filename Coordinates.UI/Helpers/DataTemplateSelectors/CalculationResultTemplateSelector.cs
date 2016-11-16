﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Coordinates.Measurements.Models;

namespace Coordinates.UI.Helpers.DataTemplateSelectors
{
    public class CalculationResultTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ErrorResultDataTemplate { get; set; }
        public DataTemplate HoleResultDataTemplate { get; set; }
        public DataTemplate SurfaceResultDataTemplate { get; set; }
        public DataTemplate DefaultResultDataTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is HoleResult)
                return HoleResultDataTemplate;

            if (item is SurfaceResult)
                return SurfaceResultDataTemplate;

            if (item is ErrorResult)
                return ErrorResultDataTemplate;

            if (item is ICalculationResult && DefaultResultDataTemplate != null)
                return DefaultResultDataTemplate;
            
            return base.SelectTemplateCore(item, container);
        }
    }
}
