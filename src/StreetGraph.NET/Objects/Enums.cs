using System;
using System.Collections.Generic;
using System.Text;

namespace StreetGraph.NET.Objects
{
    public enum DistanceUnit
    {
        Kilometers,
        Meters,
        Miles
    }

    public enum SpeedUnit
    {
        KilometersPerHour,
        MilesPerHour,
        MetersPerSecond
    }

    public enum TimeUnit
    {
        Hour,
        Minute,
        Second,
        Millisecond
    }

    public enum ShortestPathCost
    {
        Distance,
        Time
    }
}
