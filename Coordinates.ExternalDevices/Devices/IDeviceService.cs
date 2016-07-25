using Coordinates.ExternalDevices.Connections;
using Coordinates.ExternalDevices.DataSources;

namespace Coordinates.ExternalDevices.Devices
{
    public interface IDeviceService<TData, TConnection> : IDataSource<TData>, IConnectionService<TConnection>
    {
    }
}