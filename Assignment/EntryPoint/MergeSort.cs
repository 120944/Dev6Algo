using System;
using System.Collections.Generic;
using System.Linq;
using EntryPoint.Type;
using Microsoft.Xna.Framework;

namespace EntryPoint {
  public class MergeSort<T> {
    // C# MergeSort implementation
    private static void Merge(ref IEnumerable<T> list, int left, int middle, int right, Func<T, double> selector) {
      T[] _list = list.ToArray();
      T[] _temp = new T[_list.Length];
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

    public static void Sort(ref IEnumerable<T> list, int left, int right, Func<T, double> selector) {
      if (left < right) {
        int middle = (left + right) / 2;
        Sort(ref list, left, middle, selector);
        Sort(ref list, middle + 1, right, selector);
        Merge(ref list, left, middle, right, selector);
      }
      else {
        return;
      }
    }

    public static List<T> SortReturn<U>(U list, int left, int right, Func<T, double> selector) {
      var result = (IEnumerable<T>) list;
      Sort(ref result, left, right, selector);
      return result.ToList();
    }
  }
}