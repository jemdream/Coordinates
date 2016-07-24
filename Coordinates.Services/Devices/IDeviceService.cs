using Coordinates.Services.Connections;
using Coordinates.Services.DataSources;

namespace Coordinates.Services.Devices
{
    public interface IDeviceService<TData, TConnection> : IDataSource<TData>, IConnectionService<TConnection>
    {
    }
}