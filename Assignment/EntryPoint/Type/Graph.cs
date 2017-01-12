using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace EntryPoint.Type {
  public class Graph {
    private readonly IDictionary<Vector2, GraphVertex> graph;

//    public void Dijkstra(Vector2 start) {
//      if (!graph.ContainsKey(start)) {
//        Console.WriteLine($"Graph contains no node of: {start}");
//        return;
//      }
//
//      GraphVertex source = graph[start];
//
//
//      foreach (var v in graph.Values) {
//        v.Previous = v == source ? source : null;
//      }
//    }

    public Graph() {
      graph = new Dictionary<Vector2, GraphVertex>();
    }

    public void AddVertex(Vector2 v) {
      var vertex = new GraphVertex(v);
      graph.Add(v, vertex);
    }

    public void AddEdge(Vector2 origin, Vector2 terminator, double distance) {
      if (!graph.ContainsKey(origin)) 
        Console.WriteLine("Graph does not contain this value: " + origin);
      if (!graph.ContainsKey(terminator)) 
        Console.WriteLine("Graph does not contain this value: " + terminator);

      graph[origin].AddEdge(graph[terminator], distance);
    }
  }
}