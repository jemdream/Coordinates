using System;

namespace Coordinates.Services.Args
{
    public abstract class ConnectionArgs
    {
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
    }
}
