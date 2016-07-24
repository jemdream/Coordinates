using System;

namespace Coordinates.Services.DataSources
{
    public interface IDataSource<T> : IDisposable
    {
        IObservable<T> DataStream { get; set; }
    }
}