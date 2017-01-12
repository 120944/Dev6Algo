using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using static EntryPoint.Type.Euclidian;

namespace EntryPoint.Type {
  public class GraphVertex {
    private Vector2 _value;
    private IList<GraphEdge> _edges;
    private List<GraphVertex> bestPath = new List<GraphVertex>();

    public GraphVertex(Vector2 value) {
      _value = value;
      _edges = new List<GraphEdge>();
    }

    public IEnumerable<GraphEdge> Edges => _edges;
    public Vector2 Value => _value;
    public double TotalWeight { get; set; }
    public List<GraphVertex> BestPath {
      get { return bestPath; }
      set { bestPath = value; }
    }
    public int VerticesAwayFromStart => BestPath.Count;

    public void AddEdge(GraphVertex target, double distance) {
      if (target == null) throw new ArgumentNullException("target");
      if (target == this) throw new ArgumentException("Current implementation neither expects nor allows Vertices to connect to themselves.");
      if (distance < 0) throw new ArgumentException("Distance must be positive.");
      _edges.Add(new GraphEdge(target, distance));
    }

    public override string ToString() {
      return $"{_value}";
    }
  }
}