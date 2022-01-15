using System.Collections.Generic;

public class TimeMap
{
  private readonly IDictionary<string, IList<(int timestamp, string value)>> _data;

  public TimeMap()
  {
    _data = new Dictionary<string, IList<(int timestamp, string value)>>();
  }

  public void Set(string key, string value, int timestamp)
  {
    if (!_data.ContainsKey(key))
    {
      _data[key] = new List<(int timestamp, string value)>();
    }

    _data[key].Add((timestamp, value));
  }

  public string Get(string key, int timestamp)
  {
    if (!_data.ContainsKey(key))
    {
      return string.Empty;
    }

    var list = _data[key];

    int left = 0;
    int right = list.Count - 1;

    while (left < right)
    {
      if (right - left == 1)
      {
        break;
      }

      int mid = left + (right - left) / 2;

      var midItem = list[mid];
      if (midItem.timestamp == timestamp)
      {
        return midItem.value;
      }

      if (midItem.timestamp < timestamp)
      {
        left = mid;
        continue;
      }

      right = mid;
    }

    if (list[right].timestamp <= timestamp)
    {
      return list[right].value;
    }

    if (list[left].timestamp <= timestamp)
    {
      return list[left].value;
    }

    return string.Empty;
  }
}