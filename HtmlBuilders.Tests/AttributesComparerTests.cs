using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace HtmlBuilders.Tests {
  public class AttributesComparerTests {
    protected AttributesComparerTests() {
      _dictionary1 = new Dictionary<string, string>();
      _dictionary2 = new Dictionary<string, string>();
    }

    private readonly IDictionary<string, string> _dictionary1;
    private readonly IDictionary<string, string> _dictionary2;

    public new class Equals : AttributesComparerTests {

      [Fact]
      public void TwoDictionariesWhereMostEntriesAreEqualButTheUnequalEntriesAreExcludedByKey_ShouldBeTrue() {
        _dictionary1["test1"] = "value1";
        _dictionary1["test2"] = "value2";
        _dictionary2["test1"] = "w sfqffqf";
        _dictionary2["test2"] = "value2";
        AttributesComparer.Equals(_dictionary1, _dictionary2, new[] { "test1" }).Should().BeTrue();
      }

      [Fact]
      public void TwoDictionariesWhereOneHasOneEntryExtra_ShouldBeFalse() {
        _dictionary1["test1"] = "value1";
        _dictionary1["test2"] = "value2";
        _dictionary2["test1"] = "value1";
        AttributesComparer.Equals(_dictionary1, _dictionary2).Should().BeFalse();
      }

      [Fact]
      public void TwoDictionariesWithEachOneEqualEntry_ShouldBeEqual() {
        _dictionary1["test"] = "value";
        _dictionary2["test"] = "value";
        AttributesComparer.Equals(_dictionary1, _dictionary2).Should().BeTrue();
      }

      [Fact]
      public void TwoDictionariesWithEachTwoEqualEntries_ShouldBeEqual() {
        _dictionary1["test1"] = "value1";
        _dictionary1["test2"] = "value2";
        _dictionary2["test1"] = "value1";
        _dictionary2["test2"] = "value2";
        AttributesComparer.Equals(_dictionary1, _dictionary2).Should().BeTrue();
      }

      [Fact]
      public void TwoEmptyDictionaries_ShouldBeEqual() {
        AttributesComparer.Equals(_dictionary1, _dictionary2).Should().BeTrue();
      }
    }

  }
}