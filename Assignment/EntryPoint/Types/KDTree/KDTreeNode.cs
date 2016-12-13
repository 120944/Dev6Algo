using EntryPoint.Types.Point;

namespace EntryPoint.Types.KDTree {
  public class KDTreeNode {
    private PointND _data;

    public KDTreeNode(PointND data) {
      _data = data;
    }

    public KDTreeNode(params double[] data) {
      _data = new PointND(data.Length, data);
    }

    public PointND Value
    {
      get { return _data; }
      set { _data = value; }
    }

    public override string ToString() {
      return Value.ToString();
    }
  }
}