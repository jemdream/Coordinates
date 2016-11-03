using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Models;
using Coordinates.Models.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Coordinates.Measurements.Types
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class BaseMeasurementMethod : IMeasurementMethod
    {
        protected List<IElement> BaseElements = new List<IElement>();
        [JsonProperty]
        public IEnumerable<IElement> Elements => BaseElements;
        public virtual IElement ActiveElement { get; private set; }

        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public PlaneEnum Plane { get; set; }

        // TODO ugly temporary solution for max 2 member measurements
        public IElement ActivateNextElement()
        {
            if (ActiveElement == null)
                ActiveElement = BaseElements[0];
            else if (ActiveElement == BaseElements[0] && BaseElements.Count != 1)
                ActiveElement = BaseElements[1];
            else
                ActiveElement = null;

            return ActiveElement;
        }

        /// <summary>
        /// By default, sets same plane on each element.
        /// </summary>
        public virtual bool SetupPlane(PlaneEnum? plane)
        {
            BaseElements.ForEach(be => be.Plane = plane);
            return true;
        }

        /// <summary>
        /// By default, sets given position as InitialPosition on ActiveElement
        /// </summary>
        public virtual bool SetupInitialPosition(Position position)
        {
            if (ActiveElement == null) return false;
            ActiveElement.InitialPosition = position;
            return true;
        }

        // TODO ugly temporary solution for max 2 member measurements
        public bool IsNextElementAvailable
        {
            get
            {
                if (ActiveElement == null) return false;
                if (BaseElements.Count == 1 && ActiveElement == BaseElements[0]) return false;

                return ActiveElement != BaseElements[1];
            }
        }

        public virtual bool CanCalculate() => BaseElements.All(be => be.CanCalculate());
        [JsonProperty("Result")]
        public ICalculationResult ResultGetter => Calculate();
        public virtual ICalculationResult Calculate() => null;

        public virtual CompositeDisposable Subscriptions { get; } = new CompositeDisposable();
    }
}
