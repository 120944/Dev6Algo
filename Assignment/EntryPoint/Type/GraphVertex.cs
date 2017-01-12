using System;

namespace EntryPoint.Type {
  public class GraphVertex {
    private Tuple<GraphNode, GraphNode> _connections;
    private int _weight;

    public GraphVertex(int weight, GraphNode from, GraphNode to) {
      _weight = weight;
      _connections = new Tuple<GraphNode, GraphNode>(from, to);
      to.Connections.Add(this);
    }

    public int Weight {
      get { return _weight; }
      set { _weight = value; }
    }

    public Tuple<GraphNode, GraphNode> Connections {
      get { return _connections; }
      set { _connections = value; }
    }

//    public GraphNode Fst {
//      get { return _connections.Item1; }
//      set { _connections = new Tuple<GraphNode, GraphNode>(value, _connections.Item2); } 
//    }
//
//    public GraphNode Snd {
//      get { return _connections.Item2; }
//      set { _connections = new Tuple<GraphNode, GraphNode>(_connections.Item1, value); }
//    }

    public GraphNode GetConnected(GraphNode from) {
      if (_connections.Item1 == from) {
        return _connections.Item2;
      }
      if (_connections.Item2 == from) {
        return _connections.Item1;
      }
      return null;
    }

    public override string ToString() {
      return $"{_connections.Item1} --[{_weight}]-> {_connections.Item2}";
    }
  }
}