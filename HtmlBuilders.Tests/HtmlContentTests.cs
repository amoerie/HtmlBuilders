using System;
using Microsoft.AspNetCore.Html;
using Xunit;

namespace HtmlBuilders.Tests;

public class HtmlContentTests
{
    public class ToHtmlTag : HtmlContentTests
    {
        [Fact]
        public void ParsingFromHtmlContentShouldReturnCorrectTag()
        {
            // Arrange
            IHtmlContent content = new HtmlString("<label>Hello</label>");

            // Act
            var tag = content.ToHtmlTag();

            // Assert
            Assert.Equal(HtmlTags.Label.TagName, tag.TagName);
            Assert.Equal("Hello", tag.Text());
        }

        [Fact]
        public void ParsingEmptyShouldThrow()
        {
            // Arrange
            IHtmlContent content = new HtmlString("");

            // Act + Assert
            Assert.Throws<ArgumentException>(() => content.ToHtmlTag());
        }

        [Fact]
        public void ParsingMultipleShouldThrow()
        {
            // Arrange
            IHtmlContent content = new HtmlString("<label>Hello</label><label>Hello again</label>");

            // Act + Assert
            Assert.Throws<ArgumentException>(() => content.ToHtmlTag());
        }
    }
}
