﻿using System.Collections.Generic;
using System.Reactive.Disposables;
using Coordinates.Measurements.Elements;

namespace Coordinates.Measurements.Types
{
    public interface IMeasurementMethod
    {
        IEnumerable<IElement> Elements { get; }
        IElement ActiveElement { get; }
        IElement ActivateNextElement();
        bool CanCalculate();
        object Calculate();
        CompositeDisposable Subscriptions { get; }
    }
}