using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace EntryPoint.Type {
  public class GraphTest {
    public static readonly Func<Vector2, Func<GraphNode, double>> Euclosure =
      t => n => Math.Pow(Math.Pow(t.X - n.Value.X, 2.0) + Math.Pow(t.Y - n.Value.Y, 2.0), 0.5);

    public static void Main(string[] args) {
      var start = new Vector2(5, 5);
      var end = new Vector2(10,0);
      var graph = new GraphNode(start);

      Vector2[][] toadd = {
        new [] { new Vector2(5,4) }, // From point 5,5
        new [] { new Vector2(5,3), new Vector2(5,6) }, // From point 5,4
        new [] { new Vector2(6,3) }, // From point 5,3
        new [] { new Vector2(7,3) }, // From point 6,3
        new [] { new Vector2(7,2) }, // From point 7,3
        new [] { new Vector2(7,1) }, // From point 7,2
        new [] { new Vector2(8,1) }, // From point 7,1
        new [] { new Vector2(9,1) }, // From point 8,1
        new [] { new Vector2(9,2) }, // From point 9,1
        new [] { new Vector2(10,2) }, // From point 9,2
        new [] { new Vector2(10,1) }, // From point  10,2
      };

      var current = graph;

      foreach (var varr in toadd) {
        foreach (var vector2 in varr) {
          current.AddNeighbourNode(new GraphNode(vector2));
        }
        try {
          current = current.Connections[1].GetConnected(current);
        }
        catch (Exception) {
          current = current.Connections[0].GetConnected(current);
        }
        
      }
      current.AddNeighbourNode(new GraphNode(end));

      GraphNode.PrintConnectionsFrom(graph);
      Console.Read();
    } 
  }
}