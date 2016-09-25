using System;
using Coordinates.Measurements;

namespace Coordinates.UI.ViewModels.MeasurementViewModels
{
    public interface IMeasurementMethodViewModel
    {
        bool IncrementElement();
    }

    public class MeasurementMethodViewModel : IMeasurementMethodViewModel
    {
        private readonly IMeasurementManager _measurementManager;

        public MeasurementMethodViewModel(IMeasurementManager measurementManager)
        {
            _measurementManager = measurementManager;
            _measurementManager.MeasurementSource
                .Subscribe(_ => { });

            //measurementManager.RawContactPositions
            //    .OnAdd
            //    .ObserveOn(SynchronizationContext.Current)
            //    .Subscribe(position => ContactPositions.Insert(0, position));

            //measurementManager.RawContactPositions
            //    .OnRemove
            //    .ObserveOn(SynchronizationContext.Current)
            //    .Subscribe(position => ContactPositions.Remove(position));

            //SelectedPositions = measurementManager.SelectedPositions;

            // On add and on remove, update controls
            //SelectedPositions
            //    .OnAdd
            //    .Concat(SelectedPositions.OnRemove)
            //    .ObserveOn(SynchronizationContext.Current)
            //    .Subscribe(_ => ModelsUpdated());

            //ContactPositions = new ObservableCollection<Position>(MeasurementManager.RawContactPositions);
        }

        public bool IncrementElement()
        {
            /* TODO take actual measurement model and subscribe (???) */
            return true;
        }
    }
}
