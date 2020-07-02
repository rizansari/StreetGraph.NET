using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreetGraph.NET.ShortestPath
{
    public class ShortestPathResult
    {
        private readonly IDictionary<int, int> _path;

        public double Distance { get; set; }
        public int FromNode { get; }
        public int ToNode { get; }
        public bool IsFounded => _path != null;

        internal ShortestPathResult(int @from, int to, int distance = Int32.MaxValue, IDictionary<int, int> path = null)
        {
            FromNode = @from;
            ToNode = to;
            Distance = distance;
            _path = path;
        }

        public IEnumerable<int> GetReversePath()
        {
            if (_path == null)
            {
                yield break;
            }

            int result = ToNode;

            while (true)
            {
                yield return result;

                if (result == FromNode)
                    break;

                result = _path[result];
            }
        }

        public IEnumerable<int> GetPath() => GetReversePath().Reverse();
    }
}
