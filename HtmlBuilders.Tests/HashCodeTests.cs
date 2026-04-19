using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HtmlBuilders.Tests;

public class HashCodeTests
{
    [Fact]
    public void ShouldWorkCorrectlyWithHashsets()
    {
        // Arrange
        var hashSet = new HashSet<IHtmlElement>();
        var tagElements = Enumerable
            .Range(0, 1000)
            .Select(i => HtmlTags.Div.Append($"Div {i}"))
            .ToArray();
        var textElements = Enumerable
            .Range(0, 1000)
            .Select(i => new HtmlText($"Text {i}"))
            .ToArray();

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
        Assert.Contains(tagElements[0], hashSet);
        Assert.Contains(tagElements[^1], hashSet);
        Assert.Contains(textElements[0], hashSet);
        Assert.Contains(textElements[^1], hashSet);
        Assert.DoesNotContain(HtmlTags.Div, hashSet);
        Assert.DoesNotContain(new HtmlText("Some other text"), hashSet);
    }

    [Fact]
    public void ShouldProduceSameHashCodesForSameTagNames() =>
        Assert.Equal(HtmlTags.Div.GetHashCode(), HtmlTags.Div.GetHashCode());

    [Fact]
    public void ShouldProduceDifferentHashCodesForDifferentTagNames() =>
        Assert.NotEqual(HtmlTags.Label.GetHashCode(), HtmlTags.Div.GetHashCode());

    [Fact]
    public void ShouldProduceSameHashCodesForSameAttributes() =>
        Assert.Equal(
            HtmlTags.Div.Class("div-1").GetHashCode(),
            HtmlTags.Div.Class("div-1").GetHashCode()
        );

    [Fact]
    public void ShouldProduceDifferentHashCodesForDifferentAttributes() =>
        Assert.NotEqual(
            HtmlTags.Div.Class("div-2").GetHashCode(),
            HtmlTags.Div.Class("div-1").GetHashCode()
        );

    [Fact]
    public void ShouldProduceSameHashCodesForSameChildren() =>
        Assert.Equal(
            HtmlTags.Div.Append(HtmlTags.Span).GetHashCode(),
            HtmlTags.Div.Append(HtmlTags.Span).GetHashCode()
        );

    [Fact]
    public void ShouldProduceDifferentHashCodesForDifferentChildren() =>
        Assert.NotEqual(
            HtmlTags.Div.Append(HtmlTags.Legend).GetHashCode(),
            HtmlTags.Div.Append(HtmlTags.Span).GetHashCode()
        );

    [Fact]
    public void ClassesInDifferentOrderShouldProduceSameHashCode() =>
        Assert.Equal(
            HtmlTags.Div.Class("class2 class1").GetHashCode(),
            HtmlTags.Div.Class("class1 class2").GetHashCode()
        );

    [Fact]
    public void StylesInDifferentOrderShouldProduceSameHashCode() =>
        Assert.Equal(
            HtmlTags.Div.Attribute("style", "height:20px;width:10px;").GetHashCode(),
            HtmlTags.Div.Attribute("style", "width:10px;height:20px;").GetHashCode()
        );
}
