using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Helpers;
using Coordinates.Measurements.Models;
using Coordinates.Models.DTO;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels.MeasurementViewModels
{
    public interface IElementViewModel
    {
        int RequiredMeasurementCount { get; }
        PlaneEnum? Plane { get; }
        bool CanCalculate();
        ICalculationResult Calculate { get; }
        IElement Element { get; }
        void RefreshUi();
        bool ViewHack { get; set; }
        
        ObservableList<Position> SelectedPositions { get; }
        ObservableCollection<Position> Positions { get; }
    }

    public class ElementViewModel : ViewModelBase, IElementViewModel
    {
        private bool _viewHack;

        public ElementViewModel(IElement element)
        {
            Element = element;

            Positions = new ObservableCollection<Position>();

            Element.Positions.OnAdd
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(pos =>
                {
                    Positions.Insert(0, pos);
                    RaisePropertyChanged(() => Calculate);
                });

            Element.Positions.OnRemove
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(pos =>
                {
                    Positions.Remove(pos);
                    RaisePropertyChanged(() => Calculate);
                });

            Element.SelectedPositions.OnAdd
                .Subscribe(pos => RefreshUi());

            Element.SelectedPositions.OnRemove
                .Subscribe(pos => RefreshUi());
        }

        public IElement Element { get; }

        public bool ViewHack { get { return _viewHack; } set { Set(ref _viewHack, value); } }
        public int RequiredMeasurementCount => Element.RequiredMeasurementCount;
        public PlaneEnum? Plane => Element.Plane;

        public bool CanCalculate() => Element.CanCalculate();
        public ICalculationResult Calculate => Element.Calculate();

        public void RefreshUi()
        {
            ViewHack = !ViewHack;
            RaisePropertyChanged(() => Plane);
            RaisePropertyChanged(() => Calculate);
            RaisePropertyChanged(() => SelectedPositions);
        }

        public ObservableList<Position> SelectedPositions => Element.SelectedPositions;
        public ObservableCollection<Position> Positions { get; }
    }
}
