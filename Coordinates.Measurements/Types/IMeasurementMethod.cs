﻿namespace Coordinates.Measurements.Types
{
    public interface IMeasurementMethod
    {
        bool CanExecute();
        object Execute(); // todo params?
    }
}
