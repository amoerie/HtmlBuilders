using Xunit;

namespace HtmlBuilders.Tests;

public class HtmlTextTests
{
    public new class Equals : HtmlTextTests
    {
        [Fact]
        public void WhenTextsAreEqualShouldBeTrue() =>
            Assert.Equal(new HtmlText("abc"), new HtmlText("abc"));

        [Fact]
        public void WhenTextsAreNotEqualShouldBeFalse() =>
            Assert.NotEqual(new HtmlText("cba"), new HtmlText("abc"));
    }

    public class ToHtml : HtmlTextTests
    {
        [Fact]
        public void WhenTextIsAbcShouldAlwaysReturnAbc()
        {
            var htmlText = new HtmlText("abc");
            Assert.Equal("abc", htmlText.ToHtml().ToHtmlString());
        }
    }

    public new class ToString : HtmlTextTests
    {
        [Fact]
        public void WhenTextIsAbcShouldBeAbc() =>
            Assert.Equal("abc", new HtmlText("abc").ToString());
    }
}
