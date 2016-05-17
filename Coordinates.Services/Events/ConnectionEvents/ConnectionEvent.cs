using System;

namespace Coordinates.Services.Args
{
    public abstract class ConnectionEvent
    {
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
    }
}
