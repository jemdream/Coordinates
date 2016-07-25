using Coordinates.UI.Models;
using Prism.Events;

namespace Coordinates.UI.Messages
{
    public class NewMeasurementMessage : PubSubEvent<MeasurementSettingsModel>
    {
    }
}