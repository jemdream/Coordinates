namespace Coordinates.ExternalDevices.Helpers
{
    /// <summary>
    /// Source of exception messages
    /// </summary>
    public class ExceptionMessages
    {
        //public const string SetupContainer = "SetupContainer should only be invoked once at application startup.";
        public const string Message = "";

        public const string CorruptData = "Data sent from device is corrupt. Please restart whole system.";
        public const string NoDeviceFound = "No device found. Make sure the device is connected and drivers are installed.";
        public const string MultipleDevicesFound = "Multiple suitable devices with name 'STMicroelectronics Virtual COM Port' found. Please make sure only one compatible device is connected.";
        public const string NoSpecificDeviceFound = "No device with name 'STMicroelectronics Virtual COM Port' found. Make sure the device is connected and drivers are installed.";
        public const string CouldNotConnect = "Could not connect to device.";
    }
}