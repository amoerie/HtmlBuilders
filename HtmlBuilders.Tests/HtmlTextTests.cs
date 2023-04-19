using FluentAssertions;
using Xunit;

namespace HtmlBuilders.Tests;

public class HtmlTextTests
{
    public new class Equals : HtmlTextTests
    {
        [Fact]
        public void WhenTextsAreEqualShouldBeTrue() => new HtmlText("abc").Should().Be(new HtmlText("abc"));

        [Fact]
        public void WhenTextsAreNotEqualShouldBeFalse() => new HtmlText("abc").Should().NotBe(new HtmlText("cba"));
    }

    public class ToHtml : HtmlTextTests
    {
        [Fact]
        public void WhenTextIsAbcShouldAlwaysReturnAbc()
        {
            var htmlText = new HtmlText("abc");
            htmlText.ToHtml().ToHtmlString().Should().Be("abc");
        }
    }

    public new class ToString : HtmlTextTests
    {
        [Fact]
        public void WhenTextIsAbcShouldBeAbc() => new HtmlText("abc").ToString().Should().Be("abc");
    }
}
