using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EntryPoint.Type;
using Microsoft.FSharp.Collections;
using static EntryPoint.Type.Euclidian;

namespace EntryPoint
{
#if WINDOWS || LINUX
  public static class Program
  {

    [STAThread]
    static void Main()
    {

      var fullscreen = false;
      read_input:
      switch (Microsoft.VisualBasic.Interaction.InputBox("Which assignment shall run next? (1, 2, 3, 4, or q for quit)", "Choose assignment", VirtualCity.GetInitialValue()))
      {
        case "1":
          using (var game = VirtualCity.RunAssignment1(SortSpecialBuildingsByDistance, fullscreen))
            game.Run();
          break;
        case "2":
          using (var game = VirtualCity.RunAssignment2(FindSpecialBuildingsWithinDistanceFromHouse, fullscreen))
            game.Run();
          break;
        case "3":
          using (var game = VirtualCity.RunAssignment3(FindRoute, fullscreen))
            game.Run();
          break;
        case "4":
          using (var game = VirtualCity.RunAssignment4(FindRoutesToAll, fullscreen))
            game.Run();
          break;
        case "q":
          return;
      }
      goto read_input;
    }

    

    private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings) {
      //Done

      //F# - Implementation method found in Sorting.fs
//      var euclidianFromHouseF = MathUtils.euclosure(house);
//      return Sorting.mergeSort(euclidianFromHouseF, (FSharpList<Vector2>)specialBuildings);

      //C# - Implementation method below
      var euclidianFromHouseC = Euclosure(house); 
      MergeSort<Vector2>.Sort(ref specialBuildings, 0, specialBuildings.Count() - 1, euclidianFromHouseC);
      return specialBuildings;
    }

    private static IEnumerable<IEnumerable<Vector2>> FindSpecialBuildingsWithinDistanceFromHouse(
      IEnumerable<Vector2> specialBuildings,
      IEnumerable<Tuple<Vector2, float>> housesAndDistances) {
      Func<Tuple<Vector2, float>, Predicate<Vector2>> predicate = t => v => Euclosure.Invoke(t.Item1).Invoke(v) < t.Item2;
      var specialBuildingsTree = Tree2D.FromEnumerable(specialBuildings);
      var newSpecialBuildings = new List<List<Vector2>>();
      foreach (var tuple in housesAndDistances) {
        var p = predicate.Invoke(tuple);
        newSpecialBuildings.Add(Tree2D.FilterTree(specialBuildingsTree, p));
      }
      return newSpecialBuildings;
    }


    private static IEnumerable<Tuple<Vector2, Vector2>> FindRoute(Vector2 startingBuilding,
      Vector2 destinationBuilding, IEnumerable<Tuple<Vector2, Vector2>> roads) {
      var graph = new Graph();
      var bestPath = new List<Tuple<Vector2,Vector2>>();
      

      // adding the startingbuilding, the destination and all roads to graph 
      graph.AddVertex(startingBuilding);
      graph.AddVertex(destinationBuilding);
      foreach (var tuple in roads) {
        var origin = tuple.Item1;
        var terminator = tuple.Item2;
        if (!graph.Vertices.ContainsKey(origin))
          graph.AddVertex(origin);

        if (!graph.Vertices.ContainsKey(terminator)) 
          graph.AddVertex(terminator);

        graph.AddEdge(origin, terminator, Euclosure.Invoke(terminator).Invoke(destinationBuilding));
      }

      var result = Dijkstra.GetPath(graph, startingBuilding, destinationBuilding);
      return result;
    }

    private static IEnumerable<IEnumerable<Tuple<Vector2, Vector2>>> FindRoutesToAll(Vector2 startingBuilding, 
      IEnumerable<Vector2> destinationBuildings, IEnumerable<Tuple<Vector2, Vector2>> roads)
    {
      //WIP
      List<List<Tuple<Vector2, Vector2>>> result = new List<List<Tuple<Vector2, Vector2>>>();
      foreach (var d in destinationBuildings)
      {
        var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
        List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
        var prevRoad = startingRoad;
        for (int i = 0; i < 30; i++)
        {
          prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, d)).First());
          fakeBestPath.Add(prevRoad);
        }
        result.Add(fakeBestPath);
      }
      return result;
    }
  }
#endif
}
