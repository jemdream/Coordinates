﻿using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Coordinates.Measurements.Helpers
{
    public class ObservableList<T> : List<T>
    {
        private readonly ReplaySubject<T> _onAddSubject = new ReplaySubject<T>();
        private readonly ReplaySubject<T> _onRemoveSubject = new ReplaySubject<T>();

        public IObservable<T> OnAdd => _onAddSubject.AsObservable();
        public IObservable<T> OnRemove => _onRemoveSubject.AsObservable();

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

        public new void Clear()
        {
            foreach (var obj in this)
                _onRemoveSubject.OnNext(obj);
            base.Clear();
        }
    }
}
