using System;

namespace Coordinates.ExternalDevices
{
    [Flags]
    public enum ConnectionStatus
    {
        Uninitialized,
        Open,
        Opening,
        Closing,
        Closed,
        Broken
    }
}