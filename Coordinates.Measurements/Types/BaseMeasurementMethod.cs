using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using Coordinates.Measurements.Elements;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Types
{
    public abstract class BaseMeasurementMethod : IMeasurementMethod
    {
        protected List<IElement> BaseElements = new List<IElement>();
        public IEnumerable<IElement> Elements => BaseElements;
        public virtual IElement ActiveElement { get; private set; }

        public IElement ActivateNextElement()
        {
            if (ActiveElement == null)
                ActiveElement = Elements.First();
            //else Elements.
            return ActiveElement;
        }
        
        public virtual bool CanCalculate() => true;
        public virtual object Calculate() => null;

        public virtual CompositeDisposable Subscriptions { get; } = new CompositeDisposable();
    }
}
