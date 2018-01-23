using System.Collections.Generic;
using System.Linq;

namespace HtmlBuilders {
  public static class AttributesComparer {
    public static bool Equals(IEnumerable<KeyValuePair<string, string>> leftAttributes,
      IEnumerable<KeyValuePair<string, string>> rightAttributes, params string[] keysToExclude) {
      return Equals(leftAttributes, rightAttributes, EqualityComparer<string>.Default, keysToExclude);
    }

    private static bool Equals(IEnumerable<KeyValuePair<string, string>> leftAttributes,
      IEnumerable<KeyValuePair<string, string>> rightAttributes,
      IEqualityComparer<string> equalityComparer,
      params string[] keysToExclude) {
      var leftDictionary = leftAttributes.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
      var rightDictionary = rightAttributes.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
      if (leftDictionary.Count != rightDictionary.Count) return false;

      keysToExclude = keysToExclude ?? new string[0];
      foreach (var keyValuePair in leftDictionary.Where(kvp => !keysToExclude.Contains(kvp.Key))) {
        if (!rightDictionary.TryGetValue(keyValuePair.Key, out var value)) return false;
        if (!equalityComparer.Equals(keyValuePair.Value, value)) return false;
      }

      return true;
    }
  }
}