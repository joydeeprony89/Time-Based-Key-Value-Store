using System;
using System.Collections.Generic;
using System.Linq;

namespace Time_Based_Key_Value_Store
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
    }
  }

  public class TimeMap
  {
    public Dictionary<string, Dictionary<int, string>> hash = null;
    public TimeMap()
    {
      hash = new Dictionary<string, Dictionary<int, string>>();
    }

    public void Set(string key, string value, int timestamp)
    {
      if (hash.ContainsKey(key))
      {
        var existing = hash[key];
        existing.Add(timestamp, value);
      }
      else
      {
        var newValue = new Dictionary<int, string>
        {
          { timestamp, value }
        };
        hash.Add(key, newValue);
      }
    }

    public string Get(string key, int timestamp)
    {
      string value = string.Empty;
      if (hash.ContainsKey(key))
      {
        var existing = hash[key];

        // right now below line returning TLE exception, we can do better by performing binary search on the dictioanry which holds the timestamp and respected values.
        var existingValue = existing.Where(item => item.Key <= timestamp).OrderByDescending(item => item.Key).FirstOrDefault();
        if (existingValue.Equals(default(KeyValuePair<int, string>))) return value;
        else value = existingValue.Value;
      }

      return value;
    }
  }
}
