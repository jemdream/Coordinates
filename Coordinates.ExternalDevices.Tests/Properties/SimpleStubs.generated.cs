using System;
using System.Runtime.CompilerServices;
using Etg.SimpleStubs;
using System.Threading.Tasks;
using Coordinates.ExternalDevices.Events.ConnectionEvents;

namespace Coordinates.ExternalDevices.Connections
{
    [CompilerGenerated]
    public class StubIConnectionService : IConnectionService
    {
        private readonly StubContainer<StubIConnectionService> _stubs = new StubContainer<StubIConnectionService>();

        global::Coordinates.ExternalDevices.IConnection global::Coordinates.ExternalDevices.Connections.IConnectionService.ConnectionConfiguration
        {
            get
            {
                return _stubs.GetMethodStub<ConnectionConfiguration_Get_Delegate>("get_ConnectionConfiguration").Invoke();
            }
        }

        global::Coordinates.ExternalDevices.ConnectionStatus global::Coordinates.ExternalDevices.Connections.IConnectionService.ConnectionStatus
        {
            get
            {
                return _stubs.GetMethodStub<ConnectionStatus_Get_Delegate>("get_ConnectionStatus").Invoke();
            }
        }

        global::System.IObservable<global::Coordinates.ExternalDevices.Events.ConnectionEvents.DiagnosticEvent> global::Coordinates.ExternalDevices.Connections.IConnectionService.DiagnosticEventsStream
        {
            get
            {
                return _stubs.GetMethodStub<DiagnosticEventsStream_Get_Delegate>("get_DiagnosticEventsStream").Invoke();
            }
        }

        public delegate global::Coordinates.ExternalDevices.IConnection ConnectionConfiguration_Get_Delegate();

        public StubIConnectionService ConnectionConfiguration_Get(ConnectionConfiguration_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::Coordinates.ExternalDevices.ConnectionStatus ConnectionStatus_Get_Delegate();

        public StubIConnectionService ConnectionStatus_Get(ConnectionStatus_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate global::System.IObservable<global::Coordinates.ExternalDevices.Events.ConnectionEvents.DiagnosticEvent> DiagnosticEventsStream_Get_Delegate();

        public StubIConnectionService DiagnosticEventsStream_Get(DiagnosticEventsStream_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task<global::Coordinates.ExternalDevices.ConnectionStatus> global::Coordinates.ExternalDevices.Connections.IConnectionService.Open()
        {
            return _stubs.GetMethodStub<Open_Delegate>("Open").Invoke();
        }

        public delegate global::System.Threading.Tasks.Task<global::Coordinates.ExternalDevices.ConnectionStatus> Open_Delegate();

        public StubIConnectionService Open(Open_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        global::System.Threading.Tasks.Task<global::Coordinates.ExternalDevices.ConnectionStatus> global::Coordinates.ExternalDevices.Connections.IConnectionService.Close()
        {
            return _stubs.GetMethodStub<Close_Delegate>("Close").Invoke();
        }

        public delegate global::System.Threading.Tasks.Task<global::Coordinates.ExternalDevices.ConnectionStatus> Close_Delegate();

        public StubIConnectionService Close(Close_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        void global::System.IDisposable.Dispose()
        {
            _stubs.GetMethodStub<IDisposable_Dispose_Delegate>("Dispose").Invoke();
        }

        public delegate void IDisposable_Dispose_Delegate();

        public StubIConnectionService Dispose(IDisposable_Dispose_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace Coordinates.ExternalDevices.DataSources
{
    [CompilerGenerated]
    public class StubIDataSource<T> : IDataSource<T>
    {
        private readonly StubContainer<StubIDataSource<T>> _stubs = new StubContainer<StubIDataSource<T>>();

        global::System.IObservable<T> global::Coordinates.ExternalDevices.DataSources.IDataSource<T>.DataStream
        {
            get
            {
                return _stubs.GetMethodStub<DataStream_Get_Delegate>("get_DataStream").Invoke();
            }

            set
            {
                _stubs.GetMethodStub<DataStream_Set_Delegate>("set_DataStream").Invoke(value);
            }
        }

        public delegate global::System.IObservable<T> DataStream_Get_Delegate();

        public StubIDataSource<T> DataStream_Get(DataStream_Get_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        public delegate void DataStream_Set_Delegate(global::System.IObservable<T> value);

        public StubIDataSource<T> DataStream_Set(DataStream_Set_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }

        void global::System.IDisposable.Dispose()
        {
            _stubs.GetMethodStub<IDisposable_Dispose_Delegate>("Dispose").Invoke();
        }

        public delegate void IDisposable_Dispose_Delegate();

        public StubIDataSource<T> Dispose(IDisposable_Dispose_Delegate del, int count = Times.Forever, bool overwrite = false)
        {
            _stubs.SetMethodStub(del, count, overwrite);
            return this;
        }
    }
}

namespace Coordinates.ExternalDevices
{
    [CompilerGenerated]
    public class StubIConnection : IConnection
    {
        private readonly StubContainer<StubIConnection> _stubs = new StubContainer<StubIConnection>();
    }
}