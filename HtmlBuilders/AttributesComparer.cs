using System;
using System.Collections.Generic;
using System.Linq;

namespace HtmlBuilders;

internal static class AttributesComparer
{
    public static bool Equals(
        IEnumerable<KeyValuePair<string, string?>> leftAttributes,
        IEnumerable<KeyValuePair<string, string?>> rightAttributes, params string[]? keysToExclude) =>
        Equals(leftAttributes, rightAttributes, EqualityComparer<string>.Default, keysToExclude);

    private static bool Equals(IEnumerable<KeyValuePair<string, string?>> leftAttributes,
        IEnumerable<KeyValuePair<string, string?>> rightAttributes,
        EqualityComparer<string> equalityComparer,
        params string[]? keysToExclude)
    {
        var leftDictionary = leftAttributes.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        var rightDictionary = rightAttributes.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        if (leftDictionary.Count != rightDictionary.Count)
        {
            return false;
        }

        keysToExclude ??= [];
        foreach (var keyValuePair in leftDictionary.Where(kvp => !keysToExclude.Contains(kvp.Key)))
        {
            if (!rightDictionary.TryGetValue(keyValuePair.Key, out var value))
            {
                return false;
            }

            if (!equalityComparer.Equals(keyValuePair.Value, value))
            {
                return false;
            }
        }

        return true;
    }
}
