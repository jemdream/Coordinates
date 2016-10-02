using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using Coordinates.Measurements.Elements;

namespace Coordinates.Measurements.Types
{
    public interface IMeasurementMethod
    {
        IEnumerable<IElement> Elements { get; }
        IElement ActiveElement { get; }
        IElement ActivateNext();
        bool CanCalculate();
        object Calculate();
        CompositeDisposable Subscriptions { get; }
    }

    public abstract class BaseMeasurementMethod : IMeasurementMethod
    {
        protected LinkedList<IElement> BaseElements = new LinkedList<IElement>();
        public IEnumerable<IElement> Elements => BaseElements;
        public virtual IElement ActiveElement { get; private set; }

        public IElement ActivateNext()
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