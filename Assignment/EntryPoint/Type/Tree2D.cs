using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EntryPoint.Type {
  public class Tree2D : Node<Vector2> {
    private static int Instancecounter = 0;
    private int _level;
    private Tree2D _parent;
    private int _id;
    private Relativity _relativity;

    public Tree2D(Vector2 value) : base(value) {
      _children = new Node<Vector2>[2];
      _level = 0;
      _id = Instancecounter++;
    }

    public Tree2D(Tree2D parent, Vector2 value) : base(value) {
      _parent = parent;
      _children = new Node<Vector2>[2];
      _level = _parent._level + 1;
      _id = Instancecounter++;
    }

    public int Level {
      get { return _level; }
      set { _level = value; }
    }

    public Tree2D Parent {
      get { return _parent; }
      set { _parent = value; }
    }

    public int Id {
      get { return _id; }
      set { _id = value; }
    }

    public Relativity RelativeToParent {
      get { return _relativity; }
      set { _relativity = value; }
    }

    public bool Insert(Tree2D tree) {
      return Insert(tree, true);
    }

    private bool Insert(Tree2D tree, bool horizontal) {
      tree._parent = this;
      tree._level = _level + 1;
      if (horizontal) {
        if (tree.Value.X < Value.X) {
          if (_children[0] == null) {
            tree.RelativeToParent = Relativity.LEFT;
            _children[0] = tree;
            return true;
          } ((Tree2D)_children[0]).Insert(tree, !horizontal);
        }
        else {
          if (_children[1] == null) {
            tree.RelativeToParent = Relativity.RIGHT;
            _children[1] = tree;
            return true;
          } ((Tree2D)_children[1]).Insert(tree, !horizontal);
        }
      } else {
        if (tree.Value.Y < Value.Y) {
          if (_children[0] == null) {
            tree.RelativeToParent = Relativity.LEFT;
            _children[0] = tree;
            return true;
          } ((Tree2D)_children[0]).Insert(tree, !horizontal);
        }
        else {
          if (_children[1] == null) {
            tree.RelativeToParent = Relativity.RIGHT;
            _children[1] = tree;
            return true;
          } ((Tree2D)_children[1]).Insert(tree, !horizontal);
        }
      }
      return false;
    }

    public Tree2D Search(Predicate<Vector2> p) {
      Tree2D found;

      Visited = true;
      if (p.Invoke(Value)) {
        return this;
      }
      if (_children[0] != null && !_children[0].Visited) {
        found = ((Tree2D)_children[0]).Search(p);
        if (found != null) {
          return found;
        }
      }
      else if (_children[1] != null && !_children[1].Visited){
        found = ((Tree2D)_children[1]).Search(p);
        if (found != null) {
          return found;
        }
      }
      return null;
    }

    // For 'casting' the IEnumerable in ex2
    public static Tree2D FromEnumerable(IEnumerable<Vector2> enumerable) {
      var tree = new Tree2D(Vector2.Zero);
      for (int i = 0; i < enumerable.Count(); i++) {
        var vector = enumerable.ElementAt(i);
        if (i > 0) {
          tree.Insert(new Tree2D(vector));
        } else {
          tree = new Tree2D(vector);
        }
      }
      return tree;
    }

    // Used for visualising
    public enum Relativity {
      ROOT,
      LEFT,
      RIGHT
    }

    // Visualising code
    public string GetString(bool getChildren) {
      var sb = new StringBuilder();
      sb.Append(Parent != null
        ? $"ID: {Id} | Level: {_level} | Value: {Value} | {RelativeToParent} branch of {Parent.Id}\n"
        : $"ID: {Id} | Level: {_level} | Value: {Value} | {RelativeToParent}\n");
      if (getChildren) {
        foreach (var child in _children.Where(child => child != null)) {
          sb.Append(child);
        }
      }
      return sb.ToString();
    }

    public override string ToString() {
      return GetString(true);
    }

    // Filtering on predicate
    public static List<Vector2> FilterTree(Tree2D tree, Predicate<Vector2> p) {
      var result = new List<Vector2>();
      fill_with_filtered(ref result, tree, p);
      return result;
    }

    private static void fill_with_filtered(ref List<Vector2> fillme, Tree2D tree, Predicate<Vector2> p) {
      if (p.Invoke(tree.Value)) fillme.Add(tree.Value);
      foreach (var child in tree.Children.Where(child => child != null)) {
        fill_with_filtered(ref fillme, (Tree2D)child, p);
      }
    }
  }
}