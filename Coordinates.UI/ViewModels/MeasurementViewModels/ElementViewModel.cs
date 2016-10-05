using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Helpers;
using Coordinates.Models.DTO;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels.MeasurementViewModels
{
    public interface IElementViewModel
    {
        int RequiredMeasurementCount { get; }

        bool CanCalculate();
        object Calculate();

        IElement Element { get; }

        ObservableList<Position> SelectedPositions { get; }
        ObservableCollection<Position> Positions { get; }
    }

    public class ElementViewModel : ViewModelBase, IElementViewModel
    {
        public ElementViewModel(IElement element)
        {
            Element = element;

            Positions = new ObservableCollection<Position>();

            Element.Positions.OnAdd
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(pos => Positions.Insert(0, pos));

            Element.Positions.OnRemove
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(pos => Positions.Remove(pos));

            Element.SelectedPositions.OnAdd
                .Subscribe(pos => RaisePropertyChanged(() => SelectedPositions));

            Element.SelectedPositions.OnRemove
                .Subscribe(pos => RaisePropertyChanged(() => SelectedPositions));
        }

        public IElement Element { get; }

        public int RequiredMeasurementCount => Element.RequiredMeasurementCount;
        public bool CanCalculate() => Element.CanCalculate();
        public object Calculate() => Element.Calculate();

        public ObservableList<Position> SelectedPositions => Element.SelectedPositions;
        public ObservableCollection<Position> Positions { get; }
    }
}
