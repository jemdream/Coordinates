using System;

namespace Coordinates.DataSources.Events
{
    public class DiagnosticEvent
    {
        public DateTime TimeStamp { get; set; }
        public object Message { get; set; } // TODO change from object to new interface type 
    }
}
