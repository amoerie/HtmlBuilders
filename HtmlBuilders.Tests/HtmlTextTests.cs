using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xunit;

namespace HtmlBuilders.Tests {
  public class HtmlTextTests {
    public new class Equals : HtmlTextTests {
      [Fact]
      public void WhenTextsAreEqual_ShouldBeTrue() {
        new HtmlText("abc").Should().Be(new HtmlText("abc"));
      }

      [Fact]
      public void WhenTextsAreNotEqual_ShouldBeFalse() {
        new HtmlText("abc").Should().NotBe(new HtmlText("cba"));
      }
    }

    public class ToHtml : HtmlTextTests {
      [Fact]
      public void WhenTextIsAbc_ShouldAlwaysReturnAbc() {
        var htmlText = new HtmlText("abc");
        htmlText.ToHtml(TagRenderMode.EndTag).ToString().Should().Be("abc");
        htmlText.ToHtml(TagRenderMode.StartTag).ToString().Should().Be("abc");
        htmlText.ToHtml(TagRenderMode.SelfClosing).ToString().Should().Be("abc");
        htmlText.ToHtml(TagRenderMode.Normal).ToString().Should().Be("abc");
        htmlText.ToHtml().ToString().Should().Be("abc");
      }
    }

    public new class ToString : HtmlTextTests {
      [Fact]
      public void WhenTextIsAbc_ShouldBeAbc() {
        new HtmlText("abc").ToString().Should().Be("abc");
      }
    }
  }
}