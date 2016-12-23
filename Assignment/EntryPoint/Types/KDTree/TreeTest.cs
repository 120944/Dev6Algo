using System;
using EntryPoint.Types.Point;
using Microsoft.Xna.Framework;

namespace EntryPoint.Types.KDTree {
  public class TreeTest {
    public static void Main(string[] args) {
      var testTree = new KDTree(new KDTreeNode(5.0, 2.0));
      // l: left under root
      testTree.AddChild(new KDTreeNode(2.0, 1.0));
      // r: right under root
      testTree.AddChild(new KDTreeNode(10.0, 1.0));
      // ll: left, left under root
      testTree.AddChild(new KDTreeNode(2.0, 0.0));
      // lr: left, right under root
      testTree.AddChild(new KDTreeNode(1.0,3.0));
      // lll:
      testTree.AddChild(new KDTreeNode(1.5, 0.5));

      Console.WriteLine(testTree.ToString());
      Console.Read();
    } 
  }
}