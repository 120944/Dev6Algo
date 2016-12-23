using System;

namespace EntryPoint.Type {
  public class Node<T> : Visitable, IDisposable {
    protected T _value;
    protected Node<T>[] _children;

    public Node(T value) {
      _value = value;
    }

    public T Value {
      get { return _value; }
      set { _value = value; }
    }

    public Node<T>[] Children {
      get { return _children; }
      protected set { _children = value; }
    }

    public void Dispose() {
      GC.SuppressFinalize(this);
    }
  }
}