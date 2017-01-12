using System;

namespace EntryPoint.Type {
  public class GraphEdge {
    private GraphVertex _terminator;
    private double _weight;

    public GraphEdge(GraphVertex terminator, double weight) {
      _weight = weight;
      _terminator = terminator;
    }

    public double Weight {
      get { return _weight; }
      set { _weight = value; }
    }

    public GraphVertex Terminator {
      get { return _terminator; }
      set { _terminator = value; }
    }

    public override string ToString() {
      return $"--[{_weight}]-> {_terminator}";
    }
  }
}