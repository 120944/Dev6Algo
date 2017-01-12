using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace EntryPoint.Type {
  public class Graph {
    public readonly IDictionary<Vector2, GraphVertex> Vertices;

    public Graph() {
      Vertices = new Dictionary<Vector2, GraphVertex>();
    }

    public void AddVertex(Vector2 v) {
      var vertex = new GraphVertex(v);
      Vertices.Add(v, vertex);
    }

    public void AddEdge(Vector2 origin, Vector2 terminator, double distance) {
      if (!Vertices.ContainsKey(origin)) {
        Console.WriteLine("Graph does not contain this value: " + origin);
        return;
      }
      if (!Vertices.ContainsKey(terminator)) {
        Console.WriteLine("Graph does not contain this value: " + terminator);
        return;
      }

      Vertices[origin].AddEdge(Vertices[terminator], distance);
    }
  }
}