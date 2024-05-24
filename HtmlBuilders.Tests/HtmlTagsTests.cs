using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace HtmlBuilders.Tests;

public class HtmlTagsTests
{
    public static readonly TheoryData<string> Fields = typeof(HtmlTags)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Aggregate(new TheoryData<string>(), (data, info) =>
        {
            data.Add(info.Name);
            return data;
        });

    public static readonly TheoryData<string> InputFields = typeof(HtmlTags.Input)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Aggregate(new TheoryData<string>(), (data, info) =>
        {
            data.Add(info.Name);
            return data;
        });

    [Theory]
    [MemberData(nameof(Fields))]
    public void TagsShouldReturnTagWithCorrectName(string field)
    {
        // Arrange + Act
        var tag = typeof(HtmlTags).GetField(field)!.GetValue(null) as HtmlTag;

        // Assert
        tag.Should().NotBeNull();
        tag!.TagName.Should().Be(field.ToLowerInvariant());
    }

    [Theory]
    [MemberData(nameof(InputFields))]
    public void InputTagsShouldReturnTagWithCorrectNameAndType(string field)
    {
        // Arrange + Act
        var tag = typeof(HtmlTags.Input).GetField(field)!.GetValue(null) as HtmlTag;

        // Assert
        tag.Should().NotBeNull();
        tag!.TagName.Should().Be("input");
        tag.HasAttribute("type").Should().BeTrue();
        if (field == nameof(HtmlTags.Input.DateTimeLocal))
        {
            // special case
            tag["type"].Should().Be("datetime-local");
        }
        else
        {
            tag["type"].Should().Be(field.ToLowerInvariant());
        }
    }
}
