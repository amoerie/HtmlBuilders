using NUnit.Framework;

namespace HtmlBuilders.Tests {
  [TestFixture]
  public partial class TestsForExtensionsForHtmlTag {
    [Test]
    public void Height_AddingNewHeightToElementWithStyleAttribute_ShouldUpdateStyle() {
      HtmlTag div = HtmlTags.Div.Style("padding", "10px").Height("15px");
      Assert.That(div.HasAttribute("style"), Is.True);
      Assert.That(div.Styles.Count, Is.EqualTo(2));
      Assert.That(div.Styles.ContainsKey("height"), Is.True);
      Assert.That(div.Styles["padding"], Is.EqualTo("10px"));
      Assert.That(div.Styles.ContainsKey("height"), Is.True);
      Assert.That(div.Styles["height"], Is.EqualTo("15px"));
    }

    [Test]
    public void Height_AddingNewHeightToElementWithoutStyleAttribute_ShouldAddStyleAndHeight() {
      HtmlTag div = HtmlTags.Div.Height("10px");
      Assert.That(div.HasAttribute("style"), Is.True);
      Assert.That(div.Styles.Count, Is.EqualTo(1));
      Assert.That(div.Styles.ContainsKey("height"), Is.True);
      Assert.That(div.Styles["height"], Is.EqualTo("10px"));
    }

    [Test]
    public void Height_UpdatingHeightWithReplaceExistingFalse_ShouldNotUpdateHeight() {
      HtmlTag div = HtmlTags.Div.Height("10px").Height("25px", false);
      Assert.That(div.HasAttribute("style"), Is.True);
      Assert.That(div.Styles.Count, Is.EqualTo(1));
      Assert.That(div.Styles.ContainsKey("height"), Is.True);
      Assert.That(div.Styles["height"], Is.EqualTo("10px"));
    }

    [Test]
    public void Height_UpdatingHeightWithReplaceExistingTrue_ShouldUpdateHeight() {
      HtmlTag div = HtmlTags.Div.Height("10px").Height("25px");
      Assert.That(div.HasAttribute("style"), Is.True);
      Assert.That(div.Styles.Count, Is.EqualTo(1));
      Assert.That(div.Styles.ContainsKey("height"), Is.True);
      Assert.That(div.Styles["height"], Is.EqualTo("25px"));
    }

    [Test]
    public void Width_AddingNewWidthToElementWithStyleAttribute_ShouldUpdateStyle() {
      HtmlTag div = HtmlTags.Div.Style("padding", "10px").Width("15px");
      Assert.That(div.HasAttribute("style"), Is.True);
      Assert.That(div.Styles.Count, Is.EqualTo(2));
      Assert.That(div.Styles.ContainsKey("width"), Is.True);
      Assert.That(div.Styles["padding"], Is.EqualTo("10px"));
      Assert.That(div.Styles.ContainsKey("width"), Is.True);
      Assert.That(div.Styles["width"], Is.EqualTo("15px"));
    }

    [Test]
    public void Width_AddingNewWidthToElementWithoutStyleAttribute_ShouldAddStyleAndWidth() {
      HtmlTag div = HtmlTags.Div.Width("10px");
      Assert.That(div.HasAttribute("style"), Is.True);
      Assert.That(div.Styles.Count, Is.EqualTo(1));
      Assert.That(div.Styles.ContainsKey("width"), Is.True);
      Assert.That(div.Styles["width"], Is.EqualTo("10px"));
    }

    [Test]
    public void Width_UpdatingWidthWithReplaceExistingFalse_ShouldNotUpdateWidth() {
      HtmlTag div = HtmlTags.Div.Width("10px").Width("25px", false);
      Assert.That(div.HasAttribute("style"), Is.True);
      Assert.That(div.Styles.Count, Is.EqualTo(1));
      Assert.That(div.Styles.ContainsKey("width"), Is.True);
      Assert.That(div.Styles["width"], Is.EqualTo("10px"));
    }

    [Test]
    public void Width_UpdatingWidthWithReplaceExistingTrue_ShouldUpdateWidth() {
      HtmlTag div = HtmlTags.Div.Width("10px").Width("25px");
      Assert.That(div.HasAttribute("style"), Is.True);
      Assert.That(div.Styles.Count, Is.EqualTo(1));
      Assert.That(div.Styles.ContainsKey("width"), Is.True);
      Assert.That(div.Styles["width"], Is.EqualTo("25px"));
    }
  }
}