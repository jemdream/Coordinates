using System;

namespace Coordinates.ExternalDevices.Events.ConnectionEvents
{
    public class DiagnosticEvent
    {
        public DateTime TimeStamp { get; set; }
        public object Message { get; set; } // TODO change from object to new interface type 
    }
}
