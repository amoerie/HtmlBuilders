using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlBuilders.Comparers
{
    internal static class DictionaryComparer
    {
        private static bool Equals(IEnumerable<KeyValuePair<string, string>> leftKeyValuePairs,
            IEnumerable<KeyValuePair<string, string>> rightKeyValuePairs,
            IEqualityComparer<string> equalityComparer,
            params string[] keysToExclude)
        {
            var leftDictionary = leftKeyValuePairs.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            var rightDictionary = rightKeyValuePairs.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            if (leftDictionary.Count != rightDictionary.Count) return false;

            keysToExclude = keysToExclude ?? new string[0];
            foreach (var keyValuePair in leftDictionary.Where(kvp => !keysToExclude.Contains(kvp.Key)))
            {
                string value;
                if (!rightDictionary.TryGetValue(keyValuePair.Key, out value)) return false;
                if (!equalityComparer.Equals(keyValuePair.Value, value)) return false;
            }
            return true;
        }


        internal static bool Equals(IEnumerable<KeyValuePair<string, string>> leftDictionary, IEnumerable<KeyValuePair<string, string>> rightDictionary, params string[] keysToExclude)
        {
            return Equals(leftDictionary, rightDictionary, EqualityComparer<string>.Default, keysToExclude);
        }
    }
}
