using System;
using Coordinates.Measurements;
using Coordinates.Measurements.Types;

namespace Coordinates.UI.ViewModels.MeasurementViewModels
{
    public interface IMeasurementMethodViewModel
    {
        bool IncrementElement();
    }

    public class MeasurementMethodViewModel : IMeasurementMethodViewModel
    {
        private readonly IMeasurementManager _measurementManager;

        private IMeasurementMethod _measurementMethod;

        public MeasurementMethodViewModel(IMeasurementManager measurementManager)
        {
            _measurementManager = measurementManager;

            _measurementManager.MeasurementSource
                .Subscribe(InitializeMeasurement);

            // todo this is from selectionmv \/
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
            
            // todo this is from manager \/
            // Storing contact points
            //compensatedPositions
            //    .Where(pos => pos.Contact)
            //    .Subscribe(pos => RawContactPositions.Add(pos));
            
            // Selected positions change
            //SelectedPositions.OnAdd
            //    .Where(_ => SelectedMeasurementMethod != null)
            //    .Subscribe(_ => { var test = SelectedMeasurementMethod.CanCalculate(); });
            //SelectedPositions.OnRemove
            //    .Where(_ => SelectedMeasurementMethod != null)
            //    .Subscribe(_ => { var test = SelectedMeasurementMethod.CanCalculate(); });

        }

        public bool IncrementElement()
        {
            /* TODO take actual measurement model and subscribe (???) */
            return true;
        }

        private void InitializeMeasurement(IMeasurementMethod measurementMethod)
        {
            _measurementMethod = measurementMethod;
        }
    }
}
