using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.FSharp.Collections;

namespace EntryPoint
{
#if WINDOWS || LINUX
  public static class Program
  {

//    [STAThread]
//    static void Main()
//    {
//
//      var fullscreen = false;
//      read_input:
//      switch (Microsoft.VisualBasic.Interaction.InputBox("Which assignment shall run next? (1, 2, 3, 4, or q for quit)", "Choose assignment", VirtualCity.GetInitialValue()))
//      {
//        case "1":
//          using (var game = VirtualCity.RunAssignment1(SortSpecialBuildingsByDistance, fullscreen))
//            game.Run();
//          break;
//        case "2":
//          using (var game = VirtualCity.RunAssignment2(FindSpecialBuildingsWithinDistanceFromHouse, fullscreen))
//            game.Run();
//          break;
//        case "3":
//          using (var game = VirtualCity.RunAssignment3(FindRoute, fullscreen))
//            game.Run();
//          break;
//        case "4":
//          using (var game = VirtualCity.RunAssignment4(FindRoutesToAll, fullscreen))
//            game.Run();
//          break;
//        case "q":
//          return;
//      }
//      goto read_input;
//    }

    private static readonly Func<Vector2, Func<Vector2, double>> Euclidian = t => v => Math.Pow(Math.Pow(t.X - v.X, 2.0) + Math.Pow(t.Y - v.Y, 2.0), 0.5);

    private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house, IEnumerable<Vector2> specialBuildings) {
      var euclidianFromHouse = Euclidian.Invoke(house);
      //Done
      //F# - Implementation method found in Sorting.fs
      return Sorting.mergeSort(euclidianFromHouse, (FSharpList<Vector2>)specialBuildings);

      //C# - Implementation method below
      //      MergeSort(ref specialBuildings, 0, specialBuildings.Count(), euclidianFromHouse);
      //      return specialBuildings;
    }

    private static IEnumerable<IEnumerable<Vector2>> FindSpecialBuildingsWithinDistanceFromHouse(
      IEnumerable<Vector2> specialBuildings,
      IEnumerable<Tuple<Vector2, float>> housesAndDistances) {

      Func<Tuple<Vector2, float>, Predicate<Vector2>> predicate = t => v => Euclidian.Invoke(t.Item1).Invoke(v) <= t.Item2;
      return Sorting.FindWithinDistance((FSharpList<Vector2>)specialBuildings, (FSharpList<Tuple<Vector2, float>>)housesAndDistances, predicate);
    }

    private static IEnumerable<Tuple<Vector2, Vector2>> FindRoute(Vector2 startingBuilding,
      Vector2 destinationBuilding, IEnumerable<Tuple<Vector2, Vector2>> roads) {
      var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
      List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
      var prevRoad = startingRoad;
      for (int i = 0; i < 30; i++) {
        prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, destinationBuilding)).First());
        fakeBestPath.Add(prevRoad);
      }
      return fakeBestPath;
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

    // C# MergeSort implementation
    private static void Merge(ref IEnumerable<Vector2> list, int left, int middle, int right, Func<Vector2, double> selector) {
      Vector2[] _list = list.ToArray();
      Vector2[] _temp = new Vector2[_list.Length];
      int l1, l2, i;

      for (l1 = left, l2 = middle + 1, i = left; i <= middle && l2 <= right; i++) {
        if (selector.Invoke(_list[l1]) <= selector.Invoke(_list[l2])) {
          _temp[i] = _list[l1++];
        }
        else {
          _temp[i] = _list[l2++];
        }
      }

      while (l1 <= middle) {
        _temp[i++] = _list[l1++];
      }

      while (l2 <= right) {
        _temp[i++] = _list[l2++];
      }

      for (i = left; i <= right; i++) {
        _list[i] = _temp[i];
      }

      list = _list;
    }

    private static void MergeSort(ref IEnumerable<Vector2> list, int left, int right, Func<Vector2, double> selector) {
      if (left < right) {
        int middle = (left + right) / 2;
        MergeSort(ref list, left, middle, selector);
        MergeSort(ref list, middle + 1, right, selector);
        Merge(ref list, left, middle, right, selector);
      }
      else {
        return;
      }
    }
  }
#endif
}
