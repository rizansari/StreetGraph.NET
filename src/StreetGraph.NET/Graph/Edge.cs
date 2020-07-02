using StreetGraph.NET.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreetGraph.NET.Graph
{
    public class Edge<TNodeObject, TEdgeObject> where TNodeObject : INodeObject, new() where TEdgeObject : IEdgeObject, new()
    {
        public Edge(Node<TNodeObject, TEdgeObject> node, TEdgeObject @object)
        {
            Node = node;
            Object = @object;
        }

        public Node<TNodeObject, TEdgeObject> Node { get; }

        public TEdgeObject Object { get; }
    }
}
