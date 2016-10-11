using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Coordinates.Measurements.Helpers
{
    /// <summary>
    /// Wraps ObservableCollection with IObservable implementation for Add/Remove methods
    /// and NotifyCollectionChanged events.
    /// </summary>
    public class ObservableCollectionRx<T> : ObservableCollection<T>
    {
        private readonly ReplaySubject<T> _onAddSubject = new ReplaySubject<T>();
        private readonly ReplaySubject<T> _onRemoveSubject = new ReplaySubject<T>();
       
        public ObservableCollectionRx()
        {
            SelectedPositionsCollectionChanged =
                Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                    h => CollectionChanged += h,
                    h => CollectionChanged -= h)
                .Where(ev => ev.EventArgs.Action != NotifyCollectionChangedAction.Replace)
                .Select(ev => ev.EventArgs.NewItems.OfType<T>());
        }

        /// <summary>
        /// Exposes NewItems as IEnumerable of T, for all Actions (but not Replace)
        /// </summary>
        public IObservable<IEnumerable<T>> SelectedPositionsCollectionChanged { get; }
        public IObservable<T> OnAddObservable => _onAddSubject.AsObservable();
        public IObservable<T> OnRemoveObservable => _onRemoveSubject.AsObservable();

        // Hiding parent methods
        public new void Add(T obj)
        {
            _onAddSubject.OnNext(obj);
            base.Add(obj);
        }
        public new void Remove(T obj)
        {
            _onRemoveSubject.OnNext(obj);
            base.Remove(obj);
        }
    }
}
