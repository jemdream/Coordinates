using System;

namespace Coordinates.Services.Events.ConnectionEvents
{
    public abstract class ConnectionEvent<TMessageType>  
    {
        public DateTime TimeStamp { get; set; }
        public TMessageType Message { get; set; }
    }
}
