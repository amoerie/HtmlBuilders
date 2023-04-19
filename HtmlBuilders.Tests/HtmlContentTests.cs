using System;
using FluentAssertions;
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
            tag.TagName.Should().Be(HtmlTags.Label.TagName);
            tag.Text().Should().Be("Hello");
        }

        [Fact]
        public void ParsingEmptyShouldThrow()
        {
            // Arrange
            IHtmlContent content = new HtmlString("");

            // Act + Assert
            content.Invoking(c => c.ToHtmlTag())
                .Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ParsingMultipleShouldThrow()
        {
            // Arrange
            IHtmlContent content = new HtmlString(
                "<label>Hello</label><label>Hello again</label>"
            );

            // Act + Assert
            content.Invoking(c => c.ToHtmlTag())
                .Should().Throw<ArgumentException>();
        }
    }


}
