using System;

namespace Coordinates.Services.Events.ConnectionEvents
{
    public class DiagnosticEvent
    {
        public DateTime TimeStamp { get; set; }
        public object Message { get; set; }
    }
}
