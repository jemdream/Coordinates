using System;

namespace Coordinates.DataSources
{
    public interface IDataSource<T> : IDisposable
    {
        IObservable<T> DataStream { get; set; }
    }
}