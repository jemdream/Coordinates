using System.Collections.Generic;
using Coordinates.Measurements.Elements;
using Coordinates.Models.DTO;

namespace Coordinates.UI.ViewModels.MeasurementViewModels
{
    public interface IElementViewModel
    {
        int RequiredMeasurementCount { get; }

        bool CanCalculate();
        object Calculate();

        IList<Position> SelectedPositions { get; }
        IList<Position> Positions { get; }
    }

    public class ElementViewModel : IElementViewModel
    {
        private readonly IElement _element;

        public ElementViewModel(IElement element)
        {
            _element = element;
        }

        public int RequiredMeasurementCount { get; }
        public bool CanCalculate() => _element.CanCalculate();

        public object Calculate() => _element.Calculate();

        public IList<Position> SelectedPositions { get; }
        public IList<Position> Positions { get; }
    }
}
