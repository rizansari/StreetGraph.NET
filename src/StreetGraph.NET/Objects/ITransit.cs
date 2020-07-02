using System;
using System.Collections.Generic;
using System.Text;

namespace StreetGraph.NET.Objects
{
    public interface ITransit
    {
        public int Id { get; }
        public DateTime TransitTime { get; }
    }
}
