using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EntryPoint.Types.KDTree {
  public class KDTree : IEnumerable<KDTreeNode> {
    private static int instancecounter = 0;
    private int ID;
    private KDTreeNode _node;
    private int _valueDimensions = -1;
    private KDTree[] _children = new KDTree[2];
    private Relativity _relativity;
    // private Func<KDTree,KDTree,bool> _branchingPredicate;
    private int _level;
    // Might be redundant
    private KDTree _parent = null;

    public KDTree(KDTreeNode node) {
      _node = node;
      _level = 0;
      ID = instancecounter++;
//      _branchingPredicate = predicate;
      _valueDimensions = node.Value.Dimensions;
      _relativity = Relativity.ROOT;
    }

    public KDTree(KDTreeNode node, KDTree parent) {
      _node = node;
      _parent = parent;
      _level = parent.Level + 1;
      ID = instancecounter++;
      _valueDimensions = node.Value.Dimensions;
      //      _branchingPredicate = predicate;
      _relativity = Relativity.ROOT;
    }

    public int Level {
      get { return _level; }
      protected set { _level = value; }
    }

    public KDTreeNode Node
    {
      get { return _node; }
      set { _node = value; }
    }

    public KDTree Parent {
      get { return _parent; }
      private set { _parent = value; }
    }

    public Relativity RelativeToParent {
      get { return _relativity; }
      set { _relativity = value; }
    }

//    public Func<KDTree, KDTree, bool> Predicate {
//      get { return _branchingPredicate; }
//      set { _branchingPredicate = value; }
//    } 

    public void AddChild(KDTree tree) {
      if (tree.Node.Value.Dimensions != _valueDimensions) {
        throw new ArgumentOutOfRangeException(nameof(tree.Node), $"Node's value has more dimensions then root tree node's value\nroot:{_valueDimensions}, got:{tree.Node.Value.Dimensions}");
      }

      tree.Parent = this;
      tree.Level = Level + 1;

      var dimensionToCheck = Level%Node.Value.Dimensions;

      if (tree.Node.Value.Data[dimensionToCheck] < Node.Value.Data[dimensionToCheck]) {
        if (_children[0] != null) {
          _children[0].AddChild(tree);
        }
        else {
          _children[0] = tree;
          tree.RelativeToParent = Relativity.LEFT;
        }
      } else {
      if (_children[1] != null) {
        _children[1].AddChild(tree);
      }
      else {
        _children[1] = tree;
        tree.RelativeToParent = Relativity.RIGHT;
      }
    }
        
      
    }

    public void AddChild(KDTreeNode node) {
      var tree = new KDTree(node, this);
      AddChild(tree);
    }

    public IEnumerator GetEnumerator() {
      throw new NotImplementedException();
    }

    IEnumerator<KDTreeNode> IEnumerable<KDTreeNode>.GetEnumerator() {
      throw new NotImplementedException();
    }

    public bool IsLeaf => _children[0] == null && _children[1] == null;

    public void AddString(ref string[,] list, int[] indeces) {
      if (IsLeaf) {
        list[Level,indeces[Level]] = $"{Path}" + Node.ToString();
        return;
      }
      list[Level,indeces[Level]] = $"{Path}" + Node.ToString();
      if (_children[0] != null) {
        _children[0].AddString(ref list, indeces);
        indeces[Level + 1]++;
      }
      if (_children[1] != null) {
        _children[1].AddString(ref list, indeces);
        indeces[Level + 1]++;
      }
    }

    private string Path {
      get {
        var s = "";
        var selected = this;
        while (selected.Parent != null) {
          s = selected.RelativeToParent.ToString() + s;
          selected = selected.Parent;
        }
        return s;
      }
    }

    public override string ToString() {
      var res = new string[16,136];
      for (int i = 0; i < res.GetLength(0); i++) {
        for (int j = 0; j < res.GetLength(1); j++) {
          res[i, j] = "";
        }
      }
      var sb = new StringBuilder();
      if (IsLeaf) {
        res[0,0] = Node.ToString();
        goto final;
      }

      AddString(ref res , new int[64]);

      int FullOffset = 13;

      final:
      for (int i = 0; i < res.GetLength(0); i++) {
        var gotMin = false;
        for (int j = 0; j < res.GetLength(1); j++) {
          if (!res[i,j].Equals("")) {
            sb.Append(res[i, j]);
            gotMin = true;
          }
        }
        if (gotMin) {
          sb.Append("\n");
        }
      }
      return sb.ToString();
    }

    public enum Relativity {
      ROOT = 0,
      LEFT = -1,
      RIGHT = 1
    }
  }
}