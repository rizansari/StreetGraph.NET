using StreetGraph.NET.Graph;
using StreetGraph.NET.Objects;
using System;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var g = new Graph<StreetLocation, StreetLink>();

            g.AddNode(new StreetLocation() { Id = 1, LocationName = "location 1" });
            g.AddNode(new StreetLocation() { Id = 2, LocationName = "location 2" });
            g.AddNode(new StreetLocation() { Id = 3, LocationName = "location 3" });
            g.AddNode(new StreetLocation() { Id = 4, LocationName = "location 4" });

            g.Connect(1, 2, new StreetLink() { Distance = new Distance(500, DistanceUnit.Meters), Speed = new Speed(80, SpeedUnit.KilometersPerHour) });
            g.Connect(1, 3, new StreetLink() { Distance = new Distance(600, DistanceUnit.Meters), Speed = new Speed(60, SpeedUnit.KilometersPerHour) });
            g.Connect(2, 3, new StreetLink() { Distance = new Distance(0.2, DistanceUnit.Kilometers), Speed = new Speed(60, SpeedUnit.KilometersPerHour) });
            g.Connect(2, 4, new StreetLink() { Distance = new Distance(450, DistanceUnit.Meters), Speed = new Speed(80, SpeedUnit.KilometersPerHour) });
            g.Connect(3, 4, new StreetLink() { Distance = new Distance(400, DistanceUnit.Meters), Speed = new Speed(80, SpeedUnit.KilometersPerHour) });

            // print graph locations and links
            foreach (var locations in g)
            {
                foreach (var link in locations)
                {
                    Console.WriteLine(locations.Key + "->" + link.Node.Key + " (" + link.Object.Speed + ", " + link.Object.Distance + ")");
                }
            }

            // calculate shortest distance between 2 locations
            var shortestPathResult = g.CalculateShortestPath(1, 4, ShortestPathCost.Distance);
            Console.WriteLine("shortest distance: {0}", shortestPathResult.Distance);
            Console.Write("path:");
            foreach (var r in shortestPathResult.GetPath())
            {
                Console.Write(r + "->");
            }
            Console.WriteLine();

            // calculate speed using IRide object
            var transitResult = g.CalculateTransitTime(new Ride() { NodeStart = 1, NodeEnd = 2, TimeStart = DateTime.Now, TimeEnd = DateTime.Now.AddSeconds(22.5) });
            Console.WriteLine("transit speed: {0:0} Km/hr", transitResult.Speed.GetSpeed(SpeedUnit.KilometersPerHour));

            // calculate speed using ITransit object
            transitResult = g.CalculateTransitTime(new Trans() { Id = 1, TransitTime = DateTime.Now }, new Trans() { Id = 2, TransitTime = DateTime.Now.AddSeconds(22.5) });
            Console.WriteLine("transit speed: {0:0} Km/hr", transitResult.Speed.GetSpeed(SpeedUnit.KilometersPerHour));
        }
    }

    public class StreetLocation : INodeObject
    {
        public int Id { get; set; }
        public string LocationName { get; set; }

        public override string ToString()
        {
            return $"StreetLocation({Id}, {LocationName})";
        }
    }

    public class StreetLink : IEdgeObject
    {
        public Distance Distance { get; set; }
        public Speed Speed { get; set; }
        public Time Time { get; set; }

        public override string ToString()
        {
            return $"StreetLink({Distance}, {Speed})";
        }
    }

    public class Ride : IRide
    {
        public int NodeStart { get; set; }
        public int NodeEnd { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
    }

    public class Trans : ITransit
    {
        public int Id { get; set; }

        public DateTime TransitTime { get; set; }
    }
}
