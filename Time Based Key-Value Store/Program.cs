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
    string res = string.Empty;
    if (!_data.ContainsKey(key))
    {
      return res;
    }
    // As the list of values for any key would be in increasing order of time
    // Why ? because time always increase so when a set would get called for the same key, its ovious that new value for the same key timestamp would be bigger.
    // as the timestamps are sorted in ASC, we can perform binary search
    var list = _data[key];
    int l = 0;
    int r = list.Count - 1;
    while (l <= r)
    {
      int mid = l + (r - l) / 2;
      // As the question has asked if you find the exact timestamp return it else return the nearest timestamp value
      if (list[mid].timestamp <= timestamp)
      {
        // Thats why when a mid element timestamp is less than eq to target timestamp we can update our res
        // and res will get updated based on BS working
        res = list[mid].value;
        l = mid + 1;
      }
      else
      {
        // As here the mid position timestamp is greater than target so will not update our res.
        r = mid - 1;
      }
    }
    return res;
  }
}