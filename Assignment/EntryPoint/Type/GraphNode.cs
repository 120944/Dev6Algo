using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using static EntryPoint.Type.Euclidian;

namespace EntryPoint.Type {
  public class GraphNode {
    private Vector2 _value;
    private List<GraphVertex> _connections = new List<GraphVertex>();
    private bool visited = false;

    public GraphNode(Vector2 value) {
      Value = value;
    }

    public Vector2 Value {
      get { return _value; }
      private set { _value = value; }
    }

    public List<GraphVertex> Connections {
      get { return _connections; }
      private set { _connections = value; }
    }

    public void AddNeighbourNode(GraphNode neighbourNode, int weight = 0) {
      _connections.Add(new GraphVertex(weight, this, neighbourNode));
    }

    public static void PrintAllNodesFrom(GraphNode root) {
      Console.WriteLine($"Root Node: {root}\n");
      print_nodes_rec(root);
    }

    private static void print_nodes_rec(GraphNode current) {
      current.visited = true;
      Console.WriteLine(current);
      foreach (var vertex in current._connections) {
        var node = vertex.GetConnected(current);
        if (!node.visited) {
          PrintAllNodesFrom(node);
        }
      }
    }

    public static void PrintConnectionsFrom(GraphNode root) {
      Console.WriteLine($"Root Node: {root}\n");
      print_conn_rec(root, null);
    }

    private static void print_conn_rec(GraphNode current, List<GraphVertex> seen) {
      current.visited = true;
      if (seen == null)
        seen = new List<GraphVertex>();
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
      return $"{_value}"; //, neighbours: {_connections.Count()}";
    }
  }
}