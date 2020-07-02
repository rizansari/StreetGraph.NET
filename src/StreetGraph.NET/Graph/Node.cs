using StreetGraph.NET.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreetGraph.NET.Graph
{
    public class Node<TNodeObject, TEdgeObject> where TNodeObject : INodeObject, new() where TEdgeObject : IEdgeObject, new()
    {
        private readonly HashSet<Node<TNodeObject, TEdgeObject>> _parents = new HashSet<Node<TNodeObject, TEdgeObject>>();
        private Edge<TNodeObject, TEdgeObject>[] _edges;

        internal Node(int key, TNodeObject item, Graph<TNodeObject, TEdgeObject> graph)
        {
            Key = key;
            Item = item;
            _edges = new Edge<TNodeObject, TEdgeObject>[5];
            Graph = graph;
        }

        internal void AddParent(Node<TNodeObject, TEdgeObject> parent)
        {
            _parents.Add(parent);
        }

        internal void AddEdge(in Edge<TNodeObject, TEdgeObject> edge)
        {
            if (_edges.Length == EdgesCount)
            {
                int newSize = _edges.Length;

                newSize *= 2;

                Array.Resize(ref _edges, newSize);
            }

            _edges[EdgesCount] = edge;
            EdgesCount++;
        }

        public IEnumerator<Edge<TNodeObject, TEdgeObject>> GetEnumerator()
        {
            for (int i = 0; i < EdgesCount; i++)
            {
                yield return _edges[i];
            }
        }

        public Edge<TNodeObject, TEdgeObject> GetEdge(int NodeId)
        {
            for (int i = 0; i < EdgesCount; i++)
            {
                if (_edges[i].Node.Key == NodeId)
                    return _edges[i];
            }

            return null;
        }

        public IEnumerable<Node<TNodeObject, TEdgeObject>> Parents => _parents;

        internal Graph<TNodeObject, TEdgeObject> Graph { get; }

        public int Key { get; }

        public TNodeObject Item { get; }

        public int EdgesCount { get; internal set; }

        public override string ToString()
        {
            return $"[{Key}({Item?.ToString()})]";
        }

        public override bool Equals(object obj)
        {
            var node = obj as Node<TNodeObject, TEdgeObject>;

            return node?.Key == Key;
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public TEdgeObject Connects(Node<TNodeObject, TEdgeObject> node)
        {
            foreach (var n in this)
            {
                if (n.Node == node)
                {
                    return n.Object;
                }
            }
            return default(TEdgeObject);
        }
    }
}
