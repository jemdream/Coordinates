using System;

namespace Coordinates.Services
{
    [Flags]
    public enum ConnectionState
    {
        Uninitialized,
        Open,
        Opening,
        Closing,
        Closed,
        Broken
    }
}