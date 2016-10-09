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
        public PlaneEnum Plane { get; set; }

        public IElement ActivateNextElement()
        {
            // TODO ugly temporary solution for 2 member measurements
            if (ActiveElement == null)
            {
                ActiveElement = BaseElements[0];
            }
            else if (ActiveElement == BaseElements[0])
            {
                ActiveElement = BaseElements[1];
            }
            else
            {
                ActiveElement = null;
            }

            return ActiveElement;

            //ActiveElement = (ActiveElement == null) ? BaseElements.First() : BaseElements[1];
            //return ActiveElement;
        }

        /// <summary>
        /// By default, sets same plane on each element.
        /// </summary>
        public virtual bool SetupPlane(PlaneEnum? plane)
        {
            BaseElements.ForEach(be => be.Plane = plane);
            return true;
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
