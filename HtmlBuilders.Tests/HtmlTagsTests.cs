using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace HtmlBuilders.Tests;

public class HtmlTagsTests
{
    public static readonly TheoryData<string> Fields = typeof(HtmlTags)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Aggregate(
            new TheoryData<string>(),
            (data, info) =>
            {
                data.Add(info.Name);
                return data;
            }
        );

    public static readonly TheoryData<string> InputFields = typeof(HtmlTags.Input)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Aggregate(
            new TheoryData<string>(),
            (data, info) =>
            {
                data.Add(info.Name);
                return data;
            }
        );

    [Theory]
    [MemberData(nameof(Fields))]
    public void TagsShouldReturnTagWithCorrectName(string field)
    {
        // Arrange + Act
        var tag = typeof(HtmlTags).GetField(field)!.GetValue(null) as HtmlTag;

        // Assert
        Assert.NotNull(tag);
        Assert.Equal(field.ToLowerInvariant(), tag!.TagName);
    }

    [Theory]
    [MemberData(nameof(InputFields))]
    public void InputTagsShouldReturnTagWithCorrectNameAndType(string field)
    {
        // Arrange + Act
        var tag = typeof(HtmlTags.Input).GetField(field)!.GetValue(null) as HtmlTag;

        // Assert
        Assert.NotNull(tag);
        Assert.Equal("input", tag!.TagName);
        Assert.True(tag.HasAttribute("type"));
        if (field == nameof(HtmlTags.Input.DateTimeLocal))
        {
            // special case
            Assert.Equal("datetime-local", tag["type"]);
        }
        else
        {
            Assert.Equal(field.ToLowerInvariant(), tag["type"]);
        }
    }
}
