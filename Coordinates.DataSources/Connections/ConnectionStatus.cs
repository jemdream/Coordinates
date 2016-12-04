using System;
using Coordinates.Models.Helpers;

namespace Coordinates.DataSources.Connections
{
    [Flags]
    public enum ConnectionStatus
    {
        [Description("Nienawiązane")]
        Uninitialized,

        [Description("Połączony")]
        Open,

        [Description("Łączenie")]
        Opening,

        [Description("Rozłączanie")]
        Closing,

        [Description("Rozłączony")]
        Closed,

        [Description("Błąd")]
        Broken
    }
}