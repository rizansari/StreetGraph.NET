using System;
using System.Collections.Generic;

namespace StreetGraph.NET.ShortestPath
{
    internal class NodeComparer : IComparer<int>
    {
        private readonly IDictionary<int, int> _distance;

        public NodeComparer(IDictionary<int, int> distance)
        {
            _distance = distance;
        }

        public int Compare(int x, int y)
        {
            int xDistance = _distance.ContainsKey(x) ? _distance[x] : Int32.MaxValue;
            int yDistance = _distance.ContainsKey(y) ? _distance[y] : Int32.MaxValue;

            int comparer = xDistance.CompareTo(yDistance);

            if (comparer == 0)
            {
                return x.CompareTo(y);
            }

            return comparer;
        }
    }
}
