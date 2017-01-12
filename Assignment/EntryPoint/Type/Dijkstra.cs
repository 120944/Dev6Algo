using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace EntryPoint.Type {
  public static class Dijkstra {
    public static List<Tuple<Vector2, Vector2>> GetPath(Graph graph, Vector2 origin, Vector2 end) {
      if (!graph.Vertices.ContainsKey(origin)) throw new ArgumentException("Starting node not in graph.");

      foreach (var vertex in graph.Vertices.Values) {
        vertex.TotalWeight = double.PositiveInfinity;
      }
      graph.Vertices[origin].TotalWeight = 0;

      var visited = new List<GraphVertex>();
      var queue = new LinkedList<GraphVertex>();
      queue.AddFirst(graph.Vertices[origin]);

      var currentNode = queue.First;
      currentNode.Value.BestPath = new List<GraphVertex> { currentNode.Value };

      while (true) {
        if (currentNode.Value.Value == end) {
          return ToTupleList(currentNode.Value.BestPath);
        }

        var currentVertex = currentNode.Value;

        if (visited.Contains(currentVertex)) {
          currentNode = currentNode.Next;
          queue.RemoveFirst();
          continue;
        }

        visited.Add(currentVertex);

        foreach (var edge in currentVertex.Edges) {
          var distance = currentVertex.TotalWeight + edge.Weight;
          if (distance < edge.Terminator.TotalWeight) {
            edge.Terminator.TotalWeight = distance;
            foreach (var vertx in currentVertex.BestPath)
              edge.Terminator.BestPath.Add(vertx);
            edge.Terminator.BestPath.Add(edge.Terminator);
          }
          queue.AddLast(edge.Terminator);
        }

        if (currentNode.Next == null) break;
        currentNode = currentNode.Next;
        queue.RemoveFirst();
      }
      return new List<Tuple<Vector2, Vector2>>();
    }

    private static List<Tuple<Vector2, Vector2>> ToTupleList(List<GraphVertex> path) {
      var result = new List<Tuple<Vector2, Vector2>>();

      for (int i = 0; i < path.Count; i++) {
        if (i+1 < path.Count) {
          result.Add(new Tuple<Vector2, Vector2>(path[i].Value, path[i + 1].Value));
        }
      }
      return result;
    }
  }
}