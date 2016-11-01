using Windows.ApplicationModel.DataTransfer;

namespace Coordinates.UI.Helpers
{
    public static class ExtensionMethods
    {
        public static void CopyTextToClipboard(this string content)
        {
            var dP = new DataPackage();
            dP.SetText(content);
            Clipboard.SetContent(dP);
        }
    }
}
