using System;
using Microsoft.Xna.Framework;

namespace EntryPoint.Type {
  public static class Euclidian {
    public static readonly Func<Vector2, Func<Vector2, double>> Euclosure = t => v => Math.Pow(Math.Pow(t.X - v.X, 2.0) + Math.Pow(t.Y - v.Y, 2.0), 0.5);
  }
}