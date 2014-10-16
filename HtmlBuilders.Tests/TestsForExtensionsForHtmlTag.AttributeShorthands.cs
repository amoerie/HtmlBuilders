using NUnit.Framework;

namespace HtmlBuilders.Tests {
  [TestFixture]
  public partial class TestsForExtensionsForHtmlTag {
    [Test]
    public void Href_AddingNewAttribute_ShouldHaveNewAttribute() {
      HtmlTag div = HtmlTags.Div.Href("test href");
      Assert.That(div.HasAttribute("href"), Is.True);
      Assert.That(div["href"], Is.EqualTo("test href"));
    }

    [Test]
    public void Href_UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue() {
      HtmlTag div = HtmlTags.Div.Href("test href");
      div.Href("new href", false);
      Assert.That(div.HasAttribute("href"), Is.True);
      Assert.That(div["href"], Is.EqualTo("test href"));
    }

    [Test]
    public void Href_UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue() {
      HtmlTag div = HtmlTags.Div.Href("test href");
      div.Href("new href");
      Assert.That(div.HasAttribute("href"), Is.True);
      Assert.That(div["href"], Is.EqualTo("new href"));
    }

    [Test]
    public void Id_AddingNewAttribute_ShouldHaveNewAttribute() {
      HtmlTag div = HtmlTags.Div.Id("test id");
      Assert.That(div.HasAttribute("id"), Is.True);
      Assert.That(div["id"], Is.EqualTo("test id"));
    }

    [Test]
    public void Id_UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue() {
      HtmlTag div = HtmlTags.Div.Id("test id");
      div.Id("new id", false);
      Assert.That(div.HasAttribute("id"), Is.True);
      Assert.That(div["id"], Is.EqualTo("test id"));
    }

    [Test]
    public void Id_UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue() {
      HtmlTag div = HtmlTags.Div.Id("test id");
      div.Id("new id");
      Assert.That(div.HasAttribute("id"), Is.True);
      Assert.That(div["id"], Is.EqualTo("new id"));
    }

    [Test]
    public void Name_AddingNewAttribute_ShouldHaveNewAttribute() {
      HtmlTag div = HtmlTags.Div.Name("test name");
      Assert.That(div.HasAttribute("name"), Is.True);
      Assert.That(div["name"], Is.EqualTo("test name"));
    }

    [Test]
    public void Name_UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue() {
      HtmlTag div = HtmlTags.Div.Name("test name");
      div.Name("new name", false);
      Assert.That(div.HasAttribute("name"), Is.True);
      Assert.That(div["name"], Is.EqualTo("test name"));
    }

    [Test]
    public void Name_UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue() {
      HtmlTag div = HtmlTags.Div.Name("test name");
      div.Name("new name");
      Assert.That(div.HasAttribute("name"), Is.True);
      Assert.That(div["name"], Is.EqualTo("new name"));
    }

    [Test]
    public void Title_AddingNewAttribute_ShouldHaveNewAttribute() {
      HtmlTag div = HtmlTags.Div.Title("test title");
      Assert.That(div.HasAttribute("title"), Is.True);
      Assert.That(div["title"], Is.EqualTo("test title"));
    }

    [Test]
    public void Title_UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue() {
      HtmlTag div = HtmlTags.Div.Title("test title");
      div.Title("new title", false);
      Assert.That(div.HasAttribute("title"), Is.True);
      Assert.That(div["title"], Is.EqualTo("test title"));
    }

    [Test]
    public void Title_UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue() {
      HtmlTag div = HtmlTags.Div.Title("test title");
      div.Title("new title");
      Assert.That(div.HasAttribute("title"), Is.True);
      Assert.That(div["title"], Is.EqualTo("new title"));
    }

    [Test]
    public void Type_AddingNewAttribute_ShouldHaveNewAttribute() {
      HtmlTag div = HtmlTags.Div.Type("test type");
      Assert.That(div.HasAttribute("type"), Is.True);
      Assert.That(div["type"], Is.EqualTo("test type"));
    }

    [Test]
    public void Type_UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue() {
      HtmlTag div = HtmlTags.Div.Type("test type");
      div.Type("new type", false);
      Assert.That(div.HasAttribute("type"), Is.True);
      Assert.That(div["type"], Is.EqualTo("test type"));
    }

    [Test]
    public void Type_UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue() {
      HtmlTag div = HtmlTags.Div.Type("test type");
      div.Type("new type");
      Assert.That(div.HasAttribute("type"), Is.True);
      Assert.That(div["type"], Is.EqualTo("new type"));
    }

    [Test]
    public void Value_AddingNewAttribute_ShouldHaveNewAttribute() {
      HtmlTag div = HtmlTags.Div.Value("test value");
      Assert.That(div.HasAttribute("value"), Is.True);
      Assert.That(div["value"], Is.EqualTo("test value"));
    }

    [Test]
    public void Value_UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue() {
      HtmlTag div = HtmlTags.Div.Value("test value");
      div.Value("new value", false);
      Assert.That(div.HasAttribute("value"), Is.True);
      Assert.That(div["value"], Is.EqualTo("test value"));
    }

    [Test]
    public void Value_UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue() {
      HtmlTag div = HtmlTags.Div.Value("test value");
      div.Value("new value");
      Assert.That(div.HasAttribute("value"), Is.True);
      Assert.That(div["value"], Is.EqualTo("new value"));
    }
  }
}