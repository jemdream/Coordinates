using System.Collections.Generic;
using System.Reactive.Disposables;
using Coordinates.Measurements.Elements;
using Coordinates.Measurements.Models;
using Coordinates.Models.DTO;

namespace Coordinates.Measurements.Types
{
    public interface IMeasurementMethod
    {
        IEnumerable<IElement> Elements { get; }
        IElement ActiveElement { get; }
        IElement ActivateNextElement();

        bool SetupPlane(PlaneEnum? plane);
        bool SetupInitialPosition(Position position);

        bool IsNextElementAvailable { get; }
        bool CanCalculate();
        ICalculationResult Calculate();
        CompositeDisposable Subscriptions { get; }
    }
}