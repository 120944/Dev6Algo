using System;
using System.Collections.Generic;
using System.Linq;
using EntryPoint.Type;
using Microsoft.Xna.Framework;

namespace EntryPoint {
  public static class TreeTraverser {
    public static List<Vector2> GetResults(Tree2D tree, Predicate<Vector2> p) {
      var result = new List<Vector2>();
      fill_with_filtered(ref result, tree, p);
      return result;
    }

    private static void fill_with_filtered(ref List<Vector2> fillme, Tree2D tree, Predicate<Vector2> p) {
      if (p.Invoke(tree.Value)) fillme.Add(tree.Value);
      foreach (var child in tree.Children.Where(child => child != null)) {
        fill_with_filtered(ref fillme, (Tree2D) child, p);
      }
    }
  }
}