using StreetGraph.NET.Objects;
using StreetGraph.NET.ShortestPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreetGraph.NET.Graph
{
    public class Graph<TNodeObject, TEdgeObject> where TNodeObject : INodeObject, new() where TEdgeObject : IEdgeObject, new()
    {
        private readonly IDictionary<int, Node<TNodeObject, TEdgeObject>> _nodes = new Dictionary<int, Node<TNodeObject, TEdgeObject>>();

        public int AddNode(TNodeObject item)
        {
            int key = (int)item.Id;
            AddNode(key, item);
            return key;
        }

        public void AddNode(int key, TNodeObject @object)
        {
            if (_nodes.ContainsKey(key))
                throw new InvalidOperationException("Node have to be unique.", new Exception("The same key of node."));

            _nodes.Add(key, new Node<TNodeObject, TEdgeObject>(key, @object, this));
        }

        public bool Connect(int from, int to, TEdgeObject @object)
        {
            if (!_nodes.ContainsKey(from) || !_nodes.ContainsKey(to))
                return false;

            Node<TNodeObject, TEdgeObject> nodeFrom = _nodes[from];
            Node<TNodeObject, TEdgeObject> nodeTo = _nodes[to];

            nodeTo.AddParent(nodeFrom);
            nodeFrom.AddEdge(new Edge<TNodeObject, TEdgeObject>(nodeTo, @object));

            return true;
        }

        public TEdgeObject CalculateTransitTime(IRide ride)
        {
            var n1 = _nodes[ride.NodeStart];
            var n2 = _nodes[ride.NodeEnd];

            var edge = n1.Connects(n2);
            if (edge == null)
            {
                return default;
            }

            var result = new TEdgeObject();
            result.Distance = new Distance(edge.Distance.GetDistance(DistanceUnit.Meters), DistanceUnit.Meters);
            TimeSpan span = ride.TimeEnd - ride.TimeStart;
            result.Time = new Time(span.TotalSeconds, TimeUnit.Second);
            result.Speed = _calculateSpeed(result.Distance, result.Time);
            return result;
        }

        public TEdgeObject CalculateTransitTime(ITransit start, ITransit end)
        {
            var n1 = _nodes[start.Id];
            var n2 = _nodes[end.Id];

            var edge = n1.Connects(n2);
            if (edge == null)
            {
                return default;
            }

            var result = new TEdgeObject();
            result.Distance = new Distance(edge.Distance.GetDistance(DistanceUnit.Meters), DistanceUnit.Meters);
            TimeSpan span = end.TransitTime - start.TransitTime;
            result.Time = new Time(span.TotalSeconds, TimeUnit.Second);
            result.Speed = _calculateSpeed(result.Distance, result.Time);
            return result;
        }

        private Speed _calculateSpeed(Distance d, Time t)
        {
            return new Speed(d.GetDistance(DistanceUnit.Meters) / t.GetTime(TimeUnit.Second), SpeedUnit.MetersPerSecond);
        }

        private Distance _calculateDistance(Speed s, Time t)
        {
            return null;
        }

        private Time _calculateTime(Distance d, Speed s)
        {
            return null;
        }

        public ShortestPathResult CalculateShortestPath(int from, int to, ShortestPathCost cost, int depth = int.MaxValue)
        {
            var path = new Dictionary<int, int>();
            var distance = new Dictionary<int, int> { [from] = 0 };
            var d = new Dictionary<int, int> { [from] = 0 };
            var queue = new SortedSet<int>(new[] { from }, new NodeComparer(distance));
            var current = new HashSet<int>();

            int Distance(int key)
            {
                return distance.ContainsKey(key) ? distance[key] : Int32.MaxValue;
            }

            do
            {
                int u = queue.Deque();

                if (u == to)
                {
                    return new ShortestPathResult(from, to, distance[u], path);
                }

                current.Remove(u);

                if (depth == d[u])
                {
                    continue;
                }

                foreach (var edge in _nodes[u])
                {
                    var node = edge.Node.Key;
                    int costVal = Int32.MaxValue;
                    switch (cost)
                    {
                        case (ShortestPathCost.Distance):
                            costVal = Convert.ToInt32(edge.Object.Distance.GetDistance(DistanceUnit.Meters));
                            break;
                        case (ShortestPathCost.Time):
                            costVal = Convert.ToInt32(edge.Object.Time.GetTime(TimeUnit.Second));
                            break;
                    }


                    if (Distance(node) > Distance(u) + costVal)
                    {
                        if (current.Contains(node))
                        {
                            queue.Remove(node);
                        }

                        distance[node] = Distance(u) + costVal;
                        queue.Add(node);
                        current.Add(node);
                        path[node] = u;
                        d[node] = d[u] + 1;
                    }
                }

            } while (queue.Count > 0 && depth > 0);

            return new ShortestPathResult(from, to);
        }

        public override string ToString()
        {
            return $"Graph({_nodes.Count})";
        }

        public IEnumerator<Node<TNodeObject, TEdgeObject>> GetEnumerator() => _nodes.Select(x => x.Value).GetEnumerator();

        public IEnumerable<int> ParentsId(int NodeId) => _nodes[NodeId].Parents.Select(x => x.Key);

        public IEnumerable<Node<TNodeObject, TEdgeObject>> Parents(int node) => _nodes[node].Parents;
    }
}
