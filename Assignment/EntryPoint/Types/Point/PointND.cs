using System;
using System.Text;

namespace EntryPoint.Types.Point {
  public class PointND {
    private int _dimensions;
    private double[] _data;

    public PointND(int dimensions) {
      _dimensions = dimensions;
      _data = new double[_dimensions];
    }

    public PointND(int dimensions, params double[] data) {
      _dimensions = dimensions;
      if (data.Length > dimensions) {
        throw new ArgumentOutOfRangeException(nameof(data),"data list had more items than the specified dimensions");
      }
      _data = data;
    }

    public bool SetMember(int index, double item) {
      try {
        _data[index] = item;
        return true;
      }
      catch (Exception) {
        return false;
      }
    }

    public double[] Data {
      get { return _data; }
      set { _data = value; }
    }

    public int Dimensions {
      get { return _dimensions; }
      set { _dimensions = value; }
    }

    public override string ToString() {
      var sb = new StringBuilder();
      var first = true;
      sb.Append("[");
      foreach (var d in _data) {
        if (!first) {
          sb.AppendFormat(",{0,5:##0.0}", d);
          continue;
        }
        sb.AppendFormat("{0,5:##0.0}",d);
        first = false;
      }
      sb.Append("]");
      return sb.ToString();
    }
  }
}