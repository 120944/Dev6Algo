using System;

namespace EntryPoint.Type {
  public class GraphEdge {
    private GraphVertex _v1;
    private GraphVertex _v2;
    private double _weight;

    public GraphEdge(GraphVertex to, double weight) {
      _weight = weight;
      _v2 = to;
    }

    public double Weight {
      get { return _weight; }
      set { _weight = value; }
    }

    public GraphVertex V1 {
      get { return _v1; }
      set { _v1 = value; }
    }

    public GraphVertex V2 {
      get { return _v2; }
      set { _v2 = value; }
    }

    public GraphVertex GetConnected(GraphVertex from) {
      if (_v1 == from) {
        return _v2;
      }
      if (_v2 == from) {
        return _v1;
      }
      return null;
    }

    public override string ToString() {
      return $"{_v1} --[{_weight}]-> {_v2}";
    }
  }
}