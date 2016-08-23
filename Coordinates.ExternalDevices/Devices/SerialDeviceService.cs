using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;
using Coordinates.ExternalDevices.Connections;
using Coordinates.ExternalDevices.Models;

namespace Coordinates.ExternalDevices.Devices
{
    public class Stm32F4Connection : IConnection
    {

    }

    public class SerialDeviceService : BaseConnectionService, IDeviceService<GaugePositionDTO> // IDataSource<GaugePositionDTO>
    {
        private SerialDevice _device;

        private readonly IConnection _connectionConfiguration; // TODO Change IConnection to implementation
        private readonly Subject<GaugePositionDTO> _dataSourceSubject;

        private DataReader _dataReaderObject;
        private DataWriter _dataWriteObject;

        private CancellationToken _sometoken = new CancellationToken();

        private readonly CancellationToken _readCancellationToken = new CancellationToken();
        private readonly CancellationToken _writeCancellationToken = new CancellationToken();

        public SerialDeviceService(Subject<GaugePositionDTO> dataSourceSubject)
        {
            _dataSourceSubject = dataSourceSubject;
            DataStream = _dataSourceSubject.AsObservable();
        }

        public override IConnection ConnectionConfiguration => _connectionConfiguration;

        protected override async Task<bool> OnOpeningAsync()
        {
            try
            {
                var aqsFilter = SerialDevice.GetDeviceSelector("COM3");
                var devices = await DeviceInformation.FindAllAsync(aqsFilter);
                if (devices.Any())
                {
                    var deviceId = devices.First().Id;
                    _device = await SerialDevice.FromIdAsync(deviceId);

                    if (_device != null)
                    {
                        //_device.BaudRate = (uint)57600;
                        //_device.StopBits = SerialStopBitCount.One;
                        //_device.DataBits = 8;
                        //_device.Parity = SerialParity.None;
                        //_device.Handshake = SerialHandshake.None;

                        _dataReaderObject = new DataReader(_device.InputStream);
                        _dataWriteObject = new DataWriter(_device.OutputStream);

                        var readTask = Task.Run(ReadAsync, _readCancellationToken);
                        // var writeTask = Task.Run(WriteAsync, _writeCancellationToken);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            if (_device == null)
                return false; // TODO validate 

            // implement connecting to SerialDevice
            //throw new NotImplementedException();

            return await Task.FromResult(true);
        }

        /* Write method template
        /// <summary>
        /// Writes data into serial port object
        /// </summary>
        private async Task WriteAsync()
        {
            while (true)
            {
                if (_sometoken.IsCancellationRequested) break;

                _dataWriteObject.WriteString("...");
                // Launch an async task to complete the write operation
                await _dataWriteObject.StoreAsync();
            }
        }
        */

        private const string StmFrameRegex = "(X)(\\d+).*?(\\d+).*?(\\d+).*?(\\d+)";

        private static Match MatchStmFrame(string modifier)
        {
            return Regex.Match(modifier, StmFrameRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        private static bool ValidateFirstSign(Capture group)
        {
            return (group != null && group.Length == 1 && group.Value.Equals("X"));
        }

        private async Task ReadAsync()
        {
            while (true)
            {
                if (_sometoken.IsCancellationRequested) break;

                const uint readBufferLength = 26;

                // Set InputStreamOptions to complete the asynchronous read operation when one or more bytes is available
                _dataReaderObject.InputStreamOptions = InputStreamOptions.None;

                // Create a task object to wait for data on the serialPort.InputStream
                var bytesRead = await _dataReaderObject.LoadAsync(readBufferLength);

                var bufferArray = _dataReaderObject.ReadBuffer(bytesRead)
                    .ToArray();

                var bufferString = System.Text.Encoding.ASCII.GetString(bufferArray);

                // TODO if not starting with X (no match) get position of X and skip the frame with next while loop
                var match = MatchStmFrame(bufferString);

                if (match.Success && ValidateFirstSign(match.Groups[1]))
                {
                    double x;
                    var xParse = double.TryParse(match.Groups[2].Value, out x);

                    double y;
                    var yParse = double.TryParse(match.Groups[3].Value, out y);

                    double z;
                    var zParse = double.TryParse(match.Groups[4].Value, out z);

                    int gaugeInt;
                    var gParse = int.TryParse(match.Groups[5].Value, out gaugeInt);

                    var gauge = gaugeInt != 0;

                    _dataSourceSubject.OnNext(new GaugePositionDTO(x, y, z, gauge));
                }
            }
        }

        protected override async Task<bool> OnClosingAsync()
        {
            if (_device == null)
                return false; // TODO validate 

            // implement disconnecting to SerialDevice

            return await Task.FromResult(true);
        }

        public IObservable<GaugePositionDTO> DataStream { get; set; }
    }
}