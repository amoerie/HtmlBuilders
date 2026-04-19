using System.Collections.Generic;
using Xunit;

namespace HtmlBuilders.Tests;

public class AttributesComparerTests
{
    private readonly IDictionary<string, string?> _dictionary1;
    private readonly IDictionary<string, string?> _dictionary2;

    protected AttributesComparerTests()
    {
        _dictionary1 = new Dictionary<string, string?>();
        _dictionary2 = new Dictionary<string, string?>();
    }

    public new class Equals : AttributesComparerTests
    {
        [Fact]
        public void TwoDictionariesWhereMostEntriesAreEqualButTheUnequalEntriesAreExcludedByKeyShouldBeTrue()
        {
            _dictionary1["test1"] = "value1";
            _dictionary1["test2"] = "value2";
            _dictionary2["test1"] = "w sfqffqf";
            _dictionary2["test2"] = "value2";
            Assert.True(AttributesComparer.Equals(_dictionary1, _dictionary2, "test1"));
        }

        [Fact]
        public void TwoDictionariesWhereOneHasOneEntryExtraShouldBeFalse()
        {
            _dictionary1["test1"] = "value1";
            _dictionary1["test2"] = "value2";
            _dictionary2["test1"] = "value1";
            Assert.False(AttributesComparer.Equals(_dictionary1, _dictionary2));
        }

        [Fact]
        public void TwoDictionariesWithEachOneEqualEntryShouldBeEqual()
        {
            _dictionary1["test"] = "value";
            _dictionary2["test"] = "value";
            Assert.True(AttributesComparer.Equals(_dictionary1, _dictionary2));
        }

        [Fact]
        public void TwoDictionariesWithEachTwoEqualEntriesShouldBeEqual()
        {
            _dictionary1["test1"] = "value1";
            _dictionary1["test2"] = "value2";
            _dictionary2["test1"] = "value1";
            _dictionary2["test2"] = "value2";
            Assert.True(AttributesComparer.Equals(_dictionary1, _dictionary2));
        }

        [Fact]
        public void TwoEmptyDictionariesShouldBeEqual() =>
            Assert.True(AttributesComparer.Equals(_dictionary1, _dictionary2));
    }
}
