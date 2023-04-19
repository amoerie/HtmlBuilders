using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace HtmlBuilders.Tests;

public class HtmlTagsTests
{
    public static readonly IEnumerable<object[]> Fields = typeof(HtmlTags)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Select(f => new object[] { f });

    public static readonly IEnumerable<object[]> InputFields = typeof(HtmlTags.Input)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Select(f => new object[] { f });

    [Theory]
    [MemberData(nameof(Fields))]
    public void TagsShouldReturnTagWithCorrectName(FieldInfo field)
    {
        // Arrange + Act
        var tag = field.GetValue(null) as HtmlTag;

        // Assert
        tag.Should().NotBeNull();
        tag!.TagName.Should().Be(field.Name.ToLowerInvariant());
    }

    [Theory]
    [MemberData(nameof(InputFields))]
    public void InputTagsShouldReturnTagWithCorrectNameAndType(FieldInfo field)
    {
        // Arrange + Act
        var tag = field.GetValue(null) as HtmlTag;

        // Assert
        tag.Should().NotBeNull();
        tag!.TagName.Should().Be("input");
        tag.HasAttribute("type").Should().BeTrue();
        if (field.Name == nameof(HtmlTags.Input.DateTimeLocal))
        {
            // special case
            tag["type"].Should().Be("datetime-local");
        }
        else
        {
            tag["type"].Should().Be(field.Name.ToLowerInvariant());
        }
    }
}
