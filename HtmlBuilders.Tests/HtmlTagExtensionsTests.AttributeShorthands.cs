using Xunit;

namespace HtmlBuilders.Tests;

public partial class HtmlTagExtensionsTests
{
    public class Href : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddingNewAttributeShouldHaveNewAttribute()
        {
            var div = HtmlTags.Div.Href("test href");
            Assert.True(div.HasAttribute("href"));
            Assert.Equal("test href", div["href"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingFalseShouldStillHaveOldAttributeValue()
        {
            var div = HtmlTags.Div.Href("test href");
            div.Href("new href", false);
            Assert.True(div.HasAttribute("href"));
            Assert.Equal("test href", div["href"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingTrueShouldHaveUpdatedAttributeValue()
        {
            var div = HtmlTags.Div.Href("test href").Href("new href");
            Assert.True(div.HasAttribute("href"));
            Assert.Equal("new href", div["href"]);
        }
    }

    public class Id : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddingNewAttributeShouldHaveNewAttribute()
        {
            var div = HtmlTags.Div.Id("test id");
            Assert.True(div.HasAttribute("id"));
            Assert.Equal("test id", div["id"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingFalseShouldStillHaveOldAttributeValue()
        {
            var div = HtmlTags.Div.Id("test id");
            div.Id("new id", false);
            Assert.True(div.HasAttribute("id"));
            Assert.Equal("test id", div["id"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingTrueShouldHaveUpdatedAttributeValue()
        {
            var div = HtmlTags.Div.Id("test id").Id("new id");
            Assert.True(div.HasAttribute("id"));
            Assert.Equal("new id", div["id"]);
        }
    }

    public class Name : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddingNewAttributeShouldHaveNewAttribute()
        {
            var div = HtmlTags.Div.Name("test name");
            Assert.True(div.HasAttribute("name"));
            Assert.Equal("test name", div["name"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingFalseShouldStillHaveOldAttributeValue()
        {
            var div = HtmlTags.Div.Name("test name").Name("new name", false);
            Assert.True(div.HasAttribute("name"));
            Assert.Equal("test name", div["name"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingTrueShouldHaveUpdatedAttributeValue()
        {
            var div = HtmlTags.Div.Name("test name").Name("new name");
            Assert.True(div.HasAttribute("name"));
            Assert.Equal("new name", div["name"]);
        }
    }

    public class Type : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddingNewAttributeShouldHaveNewAttribute()
        {
            var div = HtmlTags.Div.Type("test type");
            Assert.True(div.HasAttribute("type"));
            Assert.Equal("test type", div["type"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingFalseShouldStillHaveOldAttributeValue()
        {
            var div = HtmlTags.Div.Type("test type").Type("new type", false);
            Assert.True(div.HasAttribute("type"));
            Assert.Equal("test type", div["type"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingTrueShouldHaveUpdatedAttributeValue()
        {
            var div = HtmlTags.Div.Type("test type").Type("new type");
            Assert.True(div.HasAttribute("type"));
            Assert.Equal("new type", div["type"]);
        }
    }

    public class Title : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddingNewAttributeShouldHaveNewAttribute()
        {
            var div = HtmlTags.Div.Title("test title");
            Assert.True(div.HasAttribute("title"));
            Assert.Equal("test title", div["title"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingFalseShouldStillHaveOldAttributeValue()
        {
            var div = HtmlTags.Div.Title("test title").Title("new title", false);
            Assert.True(div.HasAttribute("title"));
            Assert.Equal("test title", div["title"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingTrueShouldHaveUpdatedAttributeValue()
        {
            var div = HtmlTags.Div.Title("test title").Title("new title");
            Assert.True(div.HasAttribute("title"));
            Assert.Equal("new title", div["title"]);
        }
    }

    public class Value : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddingNewAttributeShouldHaveNewAttribute()
        {
            var div = HtmlTags.Div.Value("test value");
            Assert.True(div.HasAttribute("value"));
            Assert.Equal("test value", div["value"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingFalseShouldStillHaveOldAttributeValue()
        {
            var div = HtmlTags.Div.Value("test value").Value("new value", false);
            Assert.True(div.HasAttribute("value"));
            Assert.Equal("test value", div["value"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingTrueShouldHaveUpdatedAttributeValue()
        {
            var div = HtmlTags.Div.Value("test value").Value("new value");
            Assert.True(div.HasAttribute("value"));
            Assert.Equal("new value", div["value"]);
        }
    }

    public class Src : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddingNewAttributeShouldHaveNewAttribute()
        {
            var div = HtmlTags.Div.Src("test src");
            Assert.True(div.HasAttribute("src"));
            Assert.Equal("test src", div["src"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingFalseShouldStillHaveOldAttributeSrc()
        {
            var div = HtmlTags.Div.Src("test src").Src("new src", false);
            Assert.True(div.HasAttribute("src"));
            Assert.Equal("test src", div["src"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingTrueShouldHaveUpdatedAttributeSrc()
        {
            var div = HtmlTags.Div.Src("test src").Src("new src");
            Assert.True(div.HasAttribute("src"));
            Assert.Equal("new src", div["src"]);
        }
    }
}
