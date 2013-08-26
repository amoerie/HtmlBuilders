using System.Collections.Generic;
using HtmlBuilders.Comparers;
using NUnit.Framework;

namespace HtmlBuilders.Tests.Comparers
{
    [TestFixture]
    public class TestsForDictionaryComparer
    {
        private IDictionary<string, string> _dictionary1;
        private IDictionary<string, string> _dictionary2;

        [SetUp]
        public void Setup()
        {
            _dictionary1 = new Dictionary<string, string>();
            _dictionary2 = new Dictionary<string, string>();
        }

        [Test]
        public void Equals_TwoEmptyDictionaries_ShouldBeEqual()
        {
            Assert.That(DictionaryComparer.Equals(_dictionary1, _dictionary2), Is.True);
        }

        [Test]
        public void Equals_TwoDictionariesWithEachOneEqualEntry_ShouldBeEqual()
        {
            _dictionary1["test"] = "value";
            _dictionary2["test"] = "value";
            Assert.That(DictionaryComparer.Equals(_dictionary1, _dictionary2), Is.True);
        }

        [Test]
        public void Equals_TwoDictionariesWithEachTwoEqualEntries_ShouldBeEqual()
        {
            _dictionary1["test1"] = "value1";
            _dictionary1["test2"] = "value2";
            _dictionary2["test1"] = "value1";
            _dictionary2["test2"] = "value2";
            Assert.That(DictionaryComparer.Equals(_dictionary1, _dictionary2), Is.True);
        }

        [Test]
        public void Equals_TwoDictionariesWhereOneHasOneEntryExtra_ShouldBeFalse()
        {
            _dictionary1["test1"] = "value1";
            _dictionary1["test2"] = "value2";
            _dictionary2["test1"] = "value1";
            Assert.That(DictionaryComparer.Equals(_dictionary1, _dictionary2), Is.False);
        }

        [Test]
        public void Equals_TwoDictionariesWhereMostEntriesAreEqualButTheUnequalEntriesAreExcludedByKey_ShouldBeTrue()
        {
            _dictionary1["test1"] = "value1";
            _dictionary1["test2"] = "value2";
            _dictionary2["test1"] = "w sfqffqf";
            _dictionary2["test2"] = "value2";
            Assert.That(DictionaryComparer.Equals(_dictionary1, _dictionary2, keysToExclude: new [] { "test1" }), Is.True);
        }
    }
}
