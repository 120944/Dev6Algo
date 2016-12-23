using System;
using System.Threading;
using EntryPoint.Type;
using Microsoft.Xna.Framework;

namespace EntryPoint.Type {
  public class TreeTest {
    public static void Run(string[] args) {
      var testTree = new Tree2D(new Vector2(10f, 10f));

      // left of root
      testTree.Insert(new Tree2D(new Vector2(5f, 15f)));

      // right of root
      testTree.Insert(new Tree2D(new Vector2(15f, 5f)));

      // left of left of root
      testTree.Insert(new Tree2D(new Vector2(5f, 10f)));

      Console.WriteLine(testTree.ToString());

      Console.WriteLine("Starting Search...");

      float searchX = 10f, searchY = 10f;
      using (var found = testTree.Search(v => v.X.Equals(searchX) && v.Y.Equals(searchY))) {
        var s = found != null ? "Found:\n" + found.GetString(false) : $"{{{searchX},{searchY}}} Not Found"; 
        Console.WriteLine(s);
      }
      
      Console.Read();
    } 
  }
}