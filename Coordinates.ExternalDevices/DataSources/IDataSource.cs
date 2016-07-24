using System;

namespace Coordinates.ExternalDevices.DataSources
{
    public interface IDataSource<T> : IDisposable
    {
        IObservable<T> DataStream { get; set; }
    }
}