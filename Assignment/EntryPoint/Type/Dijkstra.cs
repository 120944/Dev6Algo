using System.Collections.Generic;

namespace EntryPoint.Type {
  public class Dijkstra {
    private List<GraphEdge> shortestPath = new List<GraphEdge>();
    private double dist;

    public void ApplyWeights(ref GraphVertex vertex, GraphVertex source) {
      dist = 0

      foreach (var graphVertex in vertex.Connections) {
        graphVertex.Weight = double.PositiveInfinity;

      }

    }
  }
}