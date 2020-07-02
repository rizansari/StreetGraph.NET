using System;
using System.Collections.Generic;
using System.Text;

namespace StreetGraph.NET.Objects
{
    public interface IEdgeObject
    {
        public Distance Distance { get; set; }
        public Speed Speed { get; set; }
        public Time Time { get; set; }
    }
}
