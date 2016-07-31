using Coordinates.ExternalDevices.Connections;
using Coordinates.ExternalDevices.DataSources;

namespace Coordinates.ExternalDevices.Devices
{
    public interface IDeviceService<TData> : IDataSource<TData>, IConnectionService
    {
    }
}