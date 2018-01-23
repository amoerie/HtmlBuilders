using FluentAssertions;
using Xunit;

namespace HtmlBuilders.Tests {
  public partial class HtmlTagExtensionsTests {
    public class Href : HtmlTagExtensionsTests {
      [Fact]
      public void AddingNewAttribute_ShouldHaveNewAttribute() {
        HtmlTag div = HtmlTags.Div.Href("test href");
        div.HasAttribute("href").Should().BeTrue();
        div["href"].Should().Be("test href");
      }

      [Fact]
      public void UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue() {
        HtmlTag div = HtmlTags.Div.Href("test href");
        div.Href("new href", false);
        div.HasAttribute("href").Should().BeTrue();
        div["href"].Should().Be("test href");
      }

      [Fact]
      public void UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue() {
        HtmlTag div = HtmlTags.Div.Href("test href").Href("new href");
        div.HasAttribute("href").Should().BeTrue();
        div["href"].Should().Be("new href");
      }
    }

    public class Id : HtmlTagExtensionsTests {
      [Fact]
      public void AddingNewAttribute_ShouldHaveNewAttribute() {
        HtmlTag div = HtmlTags.Div.Id("test id");
        div.HasAttribute("id").Should().BeTrue();
        div["id"].Should().Be("test id");
      }

      [Fact]
      public void UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue() {
        HtmlTag div = HtmlTags.Div.Id("test id");
        div.Id("new id", false);
        div.HasAttribute("id").Should().BeTrue();
        div["id"].Should().Be("test id");
      }

      [Fact]
      public void UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue() {
        HtmlTag div = HtmlTags.Div.Id("test id").Id("new id");
        div.HasAttribute("id").Should().BeTrue();
        div["id"].Should().Be("new id");
      }
    }

    public class Name : HtmlTagExtensionsTests {
      [Fact]
      public void AddingNewAttribute_ShouldHaveNewAttribute() {
        HtmlTag div = HtmlTags.Div.Name("test name");
        div.HasAttribute("name").Should().BeTrue();
        div["name"].Should().Be("test name");
      }

      [Fact]
      public void UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue() {
        HtmlTag div = HtmlTags.Div.Name("test name").Name("new name", false);
        div.HasAttribute("name").Should().BeTrue();
        div["name"].Should().Be("test name");
      }

      [Fact]
      public void UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue() {
        HtmlTag div = HtmlTags.Div.Name("test name").Name("new name");
        div.HasAttribute("name").Should().BeTrue();
        div["name"].Should().Be("new name");
      }
    }

    public class Type : HtmlTagExtensionsTests {
      [Fact]
      public void AddingNewAttribute_ShouldHaveNewAttribute() {
        HtmlTag div = HtmlTags.Div.Type("test type");
        div.HasAttribute("type").Should().BeTrue();
        div["type"].Should().Be("test type");
      }

      [Fact]
      public void UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue() {
        HtmlTag div = HtmlTags.Div.Type("test type").Type("new type", false);
        div.HasAttribute("type").Should().BeTrue();
        div["type"].Should().Be("test type");
      }

      [Fact]
      public void UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue() {
        HtmlTag div = HtmlTags.Div.Type("test type").Type("new type");
        div.HasAttribute("type").Should().BeTrue();
        div["type"].Should().Be("new type");
      }
    }

    public class Title : HtmlTagExtensionsTests {
      [Fact]
      public void AddingNewAttribute_ShouldHaveNewAttribute() {
        HtmlTag div = HtmlTags.Div.Title("test title");
        div.HasAttribute("title").Should().BeTrue();
        div["title"].Should().Be("test title");
      }

      [Fact]
      public void UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue() {
        HtmlTag div = HtmlTags.Div.Title("test title").Title("new title", false);
        div.HasAttribute("title").Should().BeTrue();
        div["title"].Should().Be("test title");
      }

      [Fact]
      public void UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue() {
        HtmlTag div = HtmlTags.Div.Title("test title").Title("new title");
        div.HasAttribute("title").Should().BeTrue();
        div["title"].Should().Be("new title");
      }
    }

    public class Value : HtmlTagExtensionsTests {
      [Fact]
      public void AddingNewAttribute_ShouldHaveNewAttribute() {
        HtmlTag div = HtmlTags.Div.Value("test value");
        div.HasAttribute("value").Should().BeTrue();
        div["value"].Should().Be("test value");
      }

      [Fact]
      public void UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue() {
        HtmlTag div = HtmlTags.Div.Value("test value").Value("new value", false);
        div.HasAttribute("value").Should().BeTrue();
        div["value"].Should().Be("test value");
      }

      [Fact]
      public void UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue() {
        HtmlTag div = HtmlTags.Div.Value("test value").Value("new value");
        div.HasAttribute("value").Should().BeTrue();
        div["value"].Should().Be("new value");
      }
    }
  }
}