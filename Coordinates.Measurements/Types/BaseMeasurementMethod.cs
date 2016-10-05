using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using Coordinates.Measurements.Elements;

namespace Coordinates.Measurements.Types
{
    public abstract class BaseMeasurementMethod : IMeasurementMethod
    {
        protected List<IElement> BaseElements = new List<IElement>();
        public IEnumerable<IElement> Elements => BaseElements;
        public virtual IElement ActiveElement { get; private set; }

        public IElement ActivateNextElement()
        {
            // TODO temporary solution for 2 member measurements
            return ActiveElement = (ActiveElement == null) ?
                BaseElements.First() :
                BaseElements[1];
        }

        public bool IsNextElementAvailable
        {
            get
            {
                if (ActiveElement == null) return false;

                return ActiveElement != BaseElements[1];
            }
        }

        public virtual bool CanCalculate() => true;
        public virtual object Calculate() => null;

        public virtual CompositeDisposable Subscriptions { get; } = new CompositeDisposable();
    }
}
