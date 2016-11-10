using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Diagnostics;
using Windows.Storage;
using Windows.UI.Xaml;
using Template10.Common;

namespace Coordinates.UI.Services
{
    public interface IFileLogger
    {
        /// <summary>
        /// Logging Session to store channels in.
        /// </summary>
        LoggingSession LoggingSession { get; }
        
        /// <summary>
        /// Folder where logs are stored in.
        /// </summary>
        StorageFolder LoggingFolder { get; }

        /// <summary>
        /// Delete logs of 30 days and older.
        /// </summary>
        Task DeleteOutdatedLogs();
        
        /// <summary>
        /// Creates LoggingFolder and registers handlers for unhandled exceptions.
        /// </summary>
        Task InitiateLogger();
    }

    public class FileLogger : IFileLogger
    {
        private const int DaysToDelete = 30;
        private const string BaseName = "Coordinates.Log";
        private const string ChannelName = "UnhandledChannel";
        private readonly LoggingChannel _loggingChannel = new LoggingChannel(ChannelName, new LoggingChannelOptions(Guid.NewGuid()));

        public LoggingSession LoggingSession { get; } = new LoggingSession(BaseName);
        public StorageFolder LoggingFolder { get; private set; }

        public async Task InitiateLogger()
        {
            LoggingSession.AddLoggingChannel(_loggingChannel);
            LoggingFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("LogFiles", CreationCollisionOption.OpenIfExists);

            CoreApplication.UnhandledErrorDetected += CoreApplication_UnhandledErrorDetected;
            BootStrapper.Current.UnhandledException += CurrentOnUnhandledException;
            BootStrapper.Current.Suspending += CurrentOnSuspending;
        }

        private void CurrentOnSuspending(object sender, SuspendingEventArgs suspendingEventArgs)
        {
            SaveFile();
        }

        public async Task DeleteOutdatedLogs()
        {
            try
            {
                var logFiles = await LoggingFolder.GetFilesAsync();

                var obsoleteFiles = logFiles
                    .Where(logFile => (DateTime.Now - logFile.DateCreated).Days > DaysToDelete);

                foreach (var logFile in obsoleteFiles)
                    await logFile.DeleteAsync();
            }
            catch (Exception ex)
            {
                _loggingChannel.LogMessage(ex.Message);
            }
        }
        
        private void CurrentOnUnhandledException(object sender, UnhandledExceptionEventArgs ex)
        {
            _loggingChannel.LogMessage(string.Format($"Unhandled error HResult: {ex.Exception.HResult}"), LoggingLevel.Critical);
            _loggingChannel.LogMessage(string.Format($"Unhandled error message: {ex.Exception.Message}"), LoggingLevel.Critical);

            SaveFile();
        }

        private void CoreApplication_UnhandledErrorDetected(object sender, UnhandledErrorDetectedEventArgs e)
        {
            try
            {
                e.UnhandledError.Propagate();
            }
            catch (Exception ex)
            {
                _loggingChannel.LogMessage(string.Format($"Unhandled error HResult: {ex.HResult}"), LoggingLevel.Critical);
                _loggingChannel.LogMessage(string.Format($"Unhandled error message: {ex.Message}"), LoggingLevel.Critical);

                SaveFile();
                throw;
            }
        }

        private StorageFile SaveFile()
        {
            if (LoggingSession == null) return null;

            var task = LoggingSession
                    .SaveToFileAsync(LoggingFolder, $"{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-tt")}.{BaseName}.etl")
                    .AsTask();

            task.Wait();

            return task.Result;
        }
    }
}