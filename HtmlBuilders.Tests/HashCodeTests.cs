using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace HtmlBuilders.Tests;

public class HashCodeTests
{
    [Fact]
    public void ShouldWorkCorrectlyWithHashsets()
    {
        // Arrange
        var hashSet = new HashSet<IHtmlElement>();
        var tagElements = Enumerable.Range(0, 1000).Select(i => HtmlTags.Div.Append($"Div {i}")).ToArray();
        var textElements = Enumerable.Range(0, 1000).Select(i => new HtmlText($"Text {i}")).ToArray();

        // Act
        foreach (var label in tagElements)
        {
            hashSet.Add(label);
        }
        foreach (var textElement in textElements)
        {
            hashSet.Add(textElement);
        }

        // Assert
        hashSet.Contains(tagElements[0]).Should().BeTrue();
        hashSet.Contains(tagElements[^1]).Should().BeTrue();
        hashSet.Contains(textElements[0]).Should().BeTrue();
        hashSet.Contains(textElements[^1]).Should().BeTrue();
        hashSet.Contains(HtmlTags.Div).Should().BeFalse();
        hashSet.Contains(new HtmlText("Some other text")).Should().BeFalse();
    }

    [Fact]
    public void ShouldProduceSameHashCodesForSameTagNames() => HtmlTags.Div.GetHashCode()
        .Should().Be(HtmlTags.Div.GetHashCode());

    [Fact]
    public void ShouldProduceDifferentHashCodesForDifferentTagNames() => HtmlTags.Div.GetHashCode()
        .Should().NotBe(HtmlTags.Label.GetHashCode());

    [Fact]
    public void ShouldProduceSameHashCodesForSameAttributes() => HtmlTags.Div.Class("div-1").GetHashCode()
        .Should().Be(HtmlTags.Div.Class("div-1").GetHashCode());

    [Fact]
    public void ShouldProduceDifferentHashCodesForDifferentAttributes() => HtmlTags.Div.Class("div-1").GetHashCode()
        .Should().NotBe(HtmlTags.Div.Class("div-2").GetHashCode());

    [Fact]
    public void ShouldProduceSameHashCodesForSameChildren() => HtmlTags.Div.Append(HtmlTags.Span).GetHashCode()
        .Should().Be(HtmlTags.Div.Append(HtmlTags.Span).GetHashCode());

    [Fact]
    public void ShouldProduceDifferentHashCodesForDifferentChildren() => HtmlTags.Div.Append(HtmlTags.Span).GetHashCode()
        .Should().NotBe(HtmlTags.Div.Append(HtmlTags.Legend).GetHashCode());

    [Fact]
    public void ClassesInDifferentOrderShouldProduceSameHashCode() => HtmlTags.Div.Class("class1 class2").GetHashCode()
        .Should().Be(HtmlTags.Div.Class("class2 class1").GetHashCode());

    [Fact]
    public void StylesInDifferentOrderShouldProduceSameHashCode() => HtmlTags.Div.Attribute("style", "width:10px;height:20px;").GetHashCode()
        .Should().Be(HtmlTags.Div.Attribute("style", "height:20px;width:10px;").GetHashCode());
}
