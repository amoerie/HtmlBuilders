using NUnit.Framework;

namespace HtmlBuilders.Tests {
  [TestFixture]
  public partial class TestsForExtensionsForHtmlTag {
    [Test]
    public void Checked_WhenFalse_AttributeShouldBeRemoved() {
      HtmlTag input = new HtmlTag("input").Checked(true).Checked(false);
      Assert.That(input.HasAttribute("checked"), Is.False);
    }

    [Test]
    public void Checked_WhenTrue_AttributeShouldBeAdded() {
      HtmlTag input = new HtmlTag("input").Checked(true);
      Assert.That(input.HasAttribute("checked"), Is.True);
      Assert.That(input["checked"], Is.EqualTo("checked"));
    }

    [Test]
    public void Disabled_WhenFalse_AttributeShouldBeRemoved() {
      HtmlTag input = new HtmlTag("input").Disabled(true).Disabled(false);
      Assert.That(input.HasAttribute("disabled"), Is.False);
    }

    [Test]
    public void Disabled_WhenTrue_AttributeShouldBeAdded() {
      HtmlTag input = new HtmlTag("input").Disabled(true);
      Assert.That(input.HasAttribute("disabled"), Is.True);
      Assert.That(input["disabled"], Is.EqualTo("disabled"));
    }

    [Test]
    public void Readonly_WhenFalse_AttributeShouldBeRemoved() {
      HtmlTag input = new HtmlTag("input").Readonly(true).Readonly(false);
      Assert.That(input.HasAttribute("readonly"), Is.False);
    }

    [Test]
    public void Readonly_WhenTrue_AttributeShouldBeAdded() {
      HtmlTag input = new HtmlTag("input").Readonly(true);
      Assert.That(input.HasAttribute("readonly"), Is.True);
      Assert.That(input["readonly"], Is.EqualTo("readonly"));
    }

    [Test]
    public void Selected_WhenFalse_AttributeShouldBeRemoved() {
      HtmlTag input = new HtmlTag("input").Selected(true).Selected(false);
      Assert.That(input.HasAttribute("selected"), Is.False);
    }

    [Test]
    public void Selected_WhenTrue_AttributeShouldBeAdded() {
      HtmlTag input = new HtmlTag("input").Selected(true);
      Assert.That(input.HasAttribute("selected"), Is.True);
      Assert.That(input["selected"], Is.EqualTo("selected"));
    }
  }
}