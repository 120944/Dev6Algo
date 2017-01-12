using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using static EntryPoint.Type.Euclidian;

namespace EntryPoint.Type {
  public class GraphVertex {
    private Vector2 _value;
    private IList<GraphEdge> _connections;
    private bool visited = false;
    private GraphVertex _previous = null;

    public GraphVertex(Vector2 value) {
      _value = value;
      _connections = new List<GraphEdge>();
    }

    public IEnumerable<GraphEdge> Connections => _connections;
    public Vector2 Value => _value;

    public void AddEdge(GraphVertex target, double distance) {
      if (target == null) throw new ArgumentNullException("target");
      if (target == this) throw new ArgumentException("Current implementation neither expects nor allows Vertices to connect to themselves.");
      if (distance <= 0) throw new ArgumentException("Distance must be positive.");
      _connections.Add(new GraphEdge(target, distance));

    }

    public static void PrintAllNodesFrom(GraphVertex root) {
      Console.WriteLine($"Root Node: {root}\n");
      print_nodes_rec(root);
    }

    private static void print_nodes_rec(GraphVertex current) {
      current.visited = true;
      Console.WriteLine(current);
      foreach (var vertex in current._connections) {
        var node = vertex.GetConnected(current);
        if (!node.visited) {
          PrintAllNodesFrom(node);
        }
      }
    }

    public static void PrintConnectionsFrom(GraphVertex root) {
      Console.WriteLine($"Root Node: {root}\n");
      print_conn_rec(root, null);
    }

    private static void print_conn_rec(GraphVertex current, List<GraphEdge> seen) {
      current.visited = true;
      if (seen == null)
        seen = new List<GraphEdge>();
      foreach (var vertex in current._connections) {
        var node = vertex.GetConnected(current);
        if (!seen.Contains(vertex)) {
          Console.WriteLine(vertex);
          seen.Add(vertex);
        }
        if (!node.visited) {
          print_conn_rec(node, seen);
        }
      }
    }

    public override string ToString() {
      return $"{_value}";
    }
  }
}