using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Coordinates.Measurements.Export;
using Coordinates.UI.Helpers;
using Template10.Utils;

namespace Coordinates.UI.Services
{
    public interface IDataExportService
    {
        Task SaveToFile<T>(T content);
    }

    public class DataExportService : IDataExportService
    {
        private readonly IMeasurementsExporter _measurementsExporter;

        public DataExportService(IMeasurementsExporter measurementsExporter)
        {
            _measurementsExporter = measurementsExporter;
        }

        /// <summary>
        /// Saves file after file picker with content.ToString(), prompts afterwards.
        /// </summary>
        /// <param name="content">Content to save. Uses .ToString() method.</param>
        public async Task SaveToFile<T>(T content)
        {
            // Prepare file picker
            var savePicker = new Windows.Storage.Pickers.FileSavePicker
            {
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary,
                SuggestedFileName = $"{DateTime.UtcNow.ToString("u")}"
            };

            _measurementsExporter.Formats
                .ForEach(ex => savePicker.FileTypeChoices.Add(ex.Description, new List<string> { ex.Extension }));

            var file = await savePicker.PickSaveFileAsync();
            if (file == null)
            {
                await InformFail();
                return;
            }

            // Get format
            var format = _measurementsExporter.Formats
                .FirstOrDefault(msf => msf.Extension.Equals(file.FileType));

            if (format == null)
            {
                await InformFail();
                return;
            }

            // Block Windows from accessing the file
            CachedFileManager.DeferUpdates(file);

            await FileIO.WriteTextAsync(file, format.Serialize(content));

            var status = await CachedFileManager.CompleteUpdatesAsync(file);

            // Check saving status
            if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                await InformSuccess(file);
            else
                await InformFail();
        }

        private static async Task InformSuccess(IStorageFile newFile)
        {
            var dialog = new ContentDialog
            {
                Title = "Eksport",
                Content = $"Dane zapisano jako {Environment.NewLine} {newFile.Path}",
                PrimaryButtonText = "Kopiuj ścieżkę i otwórz",
                SecondaryButtonText = "OK"
            };

            if ((await dialog.ShowAsync()).Equals(ContentDialogResult.Primary))
            {
                newFile.Path.CopyTextToClipboard();
                await Launcher.LaunchFileAsync(newFile);
            }
        }
        
        private static async Task InformFail()
        {
            var dialog = new ContentDialog
            {
                Title = "Eksport",
                Content = "Zapis do pliku nie powiódł się.",
                PrimaryButtonText = "OK"
            };

            await dialog.ShowAsync();
        }
    }
}