using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;
using Coordinates.ExternalDevices.Connections;
using Coordinates.ExternalDevices.Helpers;
using Coordinates.ExternalDevices.Models;

namespace Coordinates.ExternalDevices.Devices
{
    /// <summary>
    /// Implementation of STM32F4 VCP connection.
    /// TODO ProcessBuffer method could be extracted into another class - StmDataParser/Mapper
    /// </summary>
    public class SerialDeviceService : BaseConnectionService, IDeviceService<GaugePositionDTO>
    {
        private SerialDevice _device;
        private DataReader _dataReaderObject;

        private CancellationTokenSource _tokenSource;
        private CancellationToken _readCancellationToken;
        private Task _readTask;

        private const string StmFrameRegex = "^(X)(-?\\d+).*?(-?\\d+).*?(-?\\d+).*?(\\d+)$";
        private const uint ReadFrameLength = 38;
        private string _previousBufferString;

        private readonly Subject<GaugePositionDTO> _dataSourceSubject;

        private const string CorruptDataExceptionMessage = "Data sent from device is corrupt. Please restart whole system.";
        private const string SpecialFrameSign = "X";

        private void InitializeTokens()
        {
            if (_tokenSource != null && _tokenSource.IsCancellationRequested)
                _tokenSource.Dispose();

            _tokenSource = new CancellationTokenSource();
            _readCancellationToken = _tokenSource.Token;
            _previousBufferString = string.Empty;
        }

        public SerialDeviceService(Subject<GaugePositionDTO> dataSourceSubject)
        {
            InitializeTokens();
            _dataSourceSubject = dataSourceSubject;
            DataStream = _dataSourceSubject.AsObservable();
        }

        public IObservable<GaugePositionDTO> DataStream { get; set; }

        public override IConnection ConnectionConfiguration { get; }

        protected override async Task<bool> OnOpeningAsync()
        {
            try
            {
                InitializeTokens();

                var serialPortsSelector = SerialDevice.GetDeviceSelector();
                var devices = await DeviceInformation.FindAllAsync(serialPortsSelector);

                if (!devices.Any())
                    throw new NullReferenceException("No device found. Make sure the device is connected and drivers are installed.");

                var deviceIds = devices
                    .Where(device => device.Name.StartsWith(@"STMicroelectronics Virtual COM Port"))
                    .Select(device => device.Id)
                    .ToList();

                if (deviceIds.Count > 1)
                    throw new Exception("Multiple suitable devices with name 'STMicroelectronics Virtual COM Port' found. Please make sure only one compatible device is connected.");

                var firstDeviceId = deviceIds.FirstOrDefault();

                if (firstDeviceId == null)
                    throw new NullReferenceException("No device found. Make sure the device is connected and drivers are installed.");

                _device = await SerialDevice.FromIdAsync(firstDeviceId);

                if (_device == null)
                    throw new NullReferenceException("Could not connect to device.");

                _dataReaderObject = new DataReader(_device.InputStream);

                _readTask = Task.Run(ReadAsync, _readCancellationToken);

                return true;
            }
            catch (Exception e)
            {
                // TODO LOG 
                return false;
            }
        }

        protected override async Task<bool> OnClosingAsync()
        {
            try
            {
                _tokenSource.Cancel();

                if (_dataReaderObject != null)
                {
                    _dataReaderObject.DetachStream();
                    _dataReaderObject.Dispose();
                }

                // .NET is having an issue with Disposing Serial Device when the connection is cut (cannot even dispose streams) - deadlocks
                // but when synchronization with new ThreadPoolScheduler is requested, disposing is complete
                await Task.Run(() =>
                {
                    _device?.Dispose();
                }).TimeoutAfter(TimeSpan.FromSeconds(1), new ThreadPoolScheduler());

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                // TODO LOG 
                return await Task.FromResult(false);
            }
        }

        private static Match MatchStmFrame(string modifier)
        {
            return Regex.Match(modifier, StmFrameRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        private static bool ValidateFrame(Capture group)
        {
            return group != null && group.Length == 1 && group.Value.Equals("X");
        }

        private async Task ReadAsync()
        {
            try
            {
                while (true)
                {
                    if (_readCancellationToken.IsCancellationRequested) break;
                    // Set InputStreamOptions to complete the asynchronous read operation when one or more bytes is available
                    _dataReaderObject.InputStreamOptions = InputStreamOptions.None;

                    // Create a task object to wait for data on the serialPort.InputStream
                    var bufferString = await ReadBuffer(ReadFrameLength);
                    await ProcessBuffer(bufferString);
                }
            }
            catch (Exception ex)
            {
                if (_readCancellationToken.IsCancellationRequested) return;
                // TODO LOG 
                await Close();
            }
        }

        private async Task<string> ReadBuffer(uint readBufferLength)
        {
            var bytesRead = await _dataReaderObject.LoadAsync(readBufferLength)
                .AsTask(_readCancellationToken);

            var bufferArray = (bytesRead != 0) ? _dataReaderObject.ReadBuffer(bytesRead).ToArray() : new byte[0];

            return Encoding.ASCII.GetString(bufferArray);
        }

        private async Task ProcessBuffer(string bufferString)
        {
            // Validate if any change
            if (!string.IsNullOrEmpty(_previousBufferString) && bufferString.Equals(_previousBufferString))
                return;

            // Validate if matches
            var match = MatchStmFrame(bufferString);

            // If not matching, try to manipulate 'out of phase' frame 
            // (np. jak jest przesunięty X na 5 miejsce, to usun poczatek, dograj koniec i zachowaj normalny tryb dalej)
            if (!match.Success || !ValidateFrame(match.Groups[1]))
            {
                var xPosition = bufferString.IndexOf(SpecialFrameSign, StringComparison.CurrentCulture);

                if (xPosition < 0)
                    throw new Exception($"{CorruptDataExceptionMessage} Frame special sign 'X' is missing. [X index is {xPosition}].");

                // Delete incomplete data/letter before X
                var stringBuilder = new StringBuilder(bufferString);
                stringBuilder.Remove(0, xPosition);

                // Check how many letters to read from buffer
                var restLength = ReadFrameLength - stringBuilder.Length;
                if (restLength < 0)
                    throw new Exception($"{CorruptDataExceptionMessage} Frame has negative length: {restLength}.");

                // Read missing data and append
                var readFromBuffer = await ReadBuffer((uint)restLength);
                stringBuilder.Append(readFromBuffer);

                var newBuffer = stringBuilder.ToString();

                // Validate modified frame; if not matching, return;
                var secondMatch = MatchStmFrame(newBuffer);
                if (!secondMatch.Success || !ValidateFrame(secondMatch.Groups[1]))
                    throw new Exception($"{CorruptDataExceptionMessage} Second regex validation has failed. New buffer: {newBuffer}.");

                // Swap the matches and buffer
                match = secondMatch;
                bufferString = newBuffer;
            }

            double x, y, z;
            int gaugeInt;

            var xParse = double.TryParse(match.Groups[2].Value, out x);
            var yParse = double.TryParse(match.Groups[3].Value, out y);
            var zParse = double.TryParse(match.Groups[4].Value, out z);
            var gParse = int.TryParse(match.Groups[5].Value, out gaugeInt);

            if (!xParse || !yParse || !zParse || !gParse) // parsing the data has failed
                throw new Exception($"{CorruptDataExceptionMessage} Parsing Match.Groups[] has failed.");
            
            _previousBufferString = bufferString;
            _dataSourceSubject.OnNext(new GaugePositionDTO((x / 100).Round(), (y / 100).Round(), (z / 100).Round(), gaugeInt == 0));
        }
    }
}