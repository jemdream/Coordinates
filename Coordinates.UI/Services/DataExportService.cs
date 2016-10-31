using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Coordinates.Measurements.Helpers.Serialization;
using Coordinates.Measurements.Types;
using Newtonsoft.Json;
using Template10.Utils;

namespace Coordinates.UI.Services
{
    public class TxtExportFormat : MeasurementExportFormat
    {
        public TxtExportFormat()
        {
            Extension = ".txt";
            Description = "Plik tekstowy (*.txt)";
        }

        public override string Serialize<T>(T toSerialize) => 
            (toSerialize as BaseMeasurementMethod)?.AsReadableString().ToString();
    }

    public class JsonExportFormat : MeasurementExportFormat
    {
        public JsonExportFormat()
        {
            Extension = ".json";
            Description = "Plik json (*.json)";
        }

        public override string Serialize<T>(T toSerialize)
            => JsonConvert.SerializeObject(toSerialize, Formatting.Indented);
    }

    public abstract class MeasurementExportFormat
    {
        public string Extension { get; protected set; }
        public string Description { get; protected set; }
        public abstract string Serialize<T>(T toSerialize);
    }

    public class MeasurementsExporter : IMeasurementsExporter
    {
        public IEnumerable<MeasurementExportFormat> Formats { get; }
            = new MeasurementExportFormat[] { new JsonExportFormat(), new TxtExportFormat() };
    }

    public interface IMeasurementsExporter
    {
        IEnumerable<MeasurementExportFormat> Formats { get; }
    }

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

        private static async Task InformSuccess(IStorageItem newFile)
        {
            var dialog = new ContentDialog
            {
                Title = "Eksport",
                Content = $"Dane zapisano jako {Environment.NewLine} {newFile.Path}",
                PrimaryButtonText = "Kopiuj ścieżkę",
                SecondaryButtonText = "OK"
            };

            if ((await dialog.ShowAsync()).Equals(ContentDialogResult.Primary))
            {
                var dP = new DataPackage();
                dP.SetText(newFile.Path);
                Clipboard.SetContent(dP);
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