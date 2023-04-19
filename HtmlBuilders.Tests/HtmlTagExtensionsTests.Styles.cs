using System.Collections;
using FluentAssertions;
using Xunit;

namespace HtmlBuilders.Tests;

public partial class HtmlTagExtensionsTests
{
    public class Height : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddingNewHeightToElementWithStyleAttributeShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Style("padding", "10px").Height("15px");
            div.HasAttribute("style").Should().BeTrue();
            div.Styles.Count.Should().Be(2);
            div.Styles.ContainsKey("height").Should().BeTrue();
            div.Styles["padding"].Should().Be("10px");
            div.Styles.ContainsKey("height").Should().BeTrue();
            div.Styles["height"].Should().Be("15px");
        }

        [Fact]
        public void AddingNewHeightToElementWithoutStyleAttributeShouldAddStyleAndHeight()
        {
            var div = HtmlTags.Div.Height("10px");
            div.HasAttribute("style").Should().BeTrue();
            div.Styles.Count.Should().Be(1);
            div.Styles.ContainsKey("height").Should().BeTrue();
            div.Styles["height"].Should().Be("10px");
        }

        [Fact]
        public void UpdatingHeightWithReplaceExistingFalseShouldNotUpdateHeight()
        {
            var div = HtmlTags.Div.Height("10px").Height("25px", false);
            div.HasAttribute("style").Should().BeTrue();
            div.Styles.Count.Should().Be(1);
            div.Styles.ContainsKey("height").Should().BeTrue();
            div.Styles["height"].Should().Be("10px");
        }

        [Fact]
        public void UpdatingHeightWithReplaceExistingTrueShouldUpdateHeight()
        {
            var div = HtmlTags.Div.Height("10px").Height("25px");
            div.HasAttribute("style").Should().BeTrue();
            div.Styles.Count.Should().Be(1);
            div.Styles.ContainsKey("height").Should().BeTrue();
            div.Styles["height"].Should().Be("25px");
        }
    }

    public class Width : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddingNewWidthToElementWithStyleAttributeShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Style("padding", "10px").Width("15px");
            div.HasAttribute("style").Should().BeTrue();
            div.Styles.Count.Should().Be(2);
            div.Styles.ContainsKey("width").Should().BeTrue();
            div.Styles["padding"].Should().Be("10px");
            div.Styles.ContainsKey("width").Should().BeTrue();
            div.Styles["width"].Should().Be("15px");
        }

        [Fact]
        public void AddingNewWidthToElementWithoutStyleAttributeShouldAddStyleAndWidth()
        {
            var div = HtmlTags.Div.Width("10px");
            div.HasAttribute("style").Should().BeTrue();
            div.Styles.Count.Should().Be(1);
            div.Styles.ContainsKey("width").Should().BeTrue();
            div.Styles["width"].Should().Be("10px");
        }

        [Fact]
        public void UpdatingWidthWithReplaceExistingFalseShouldNotUpdateWidth()
        {
            var div = HtmlTags.Div.Width("10px").Width("25px", false);
            div.HasAttribute("style").Should().BeTrue();
            div.Styles.Count.Should().Be(1);
            div.Styles.ContainsKey("width").Should().BeTrue();
            div.Styles["width"].Should().Be("10px");
        }

        [Fact]
        public void UpdatingWidthWithReplaceExistingTrueShouldUpdateWidth()
        {
            var div = HtmlTags.Div.Width("10px").Width("25px");
            div.HasAttribute("style").Should().BeTrue();
            div.Styles.Count.Should().Be(1);
            div.Styles.ContainsKey("width").Should().BeTrue();
            div.Styles["width"].Should().Be("25px");
        }
    }

    public class Margin : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddMarginShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Margin("10px");
            div.Styles.Count.Should().Be(1);
            div.Styles.ContainsKey("margin").Should().BeTrue();
            div.Styles["margin"].Should().Be("10px");
        }
    }

    public class Padding : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddPaddingShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Padding("10px");
            div.Styles.Count.Should().Be(1);
            div.Styles.ContainsKey("padding").Should().BeTrue();
            div.Styles["padding"].Should().Be("10px");
        }
    }

    public class Color : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddColorShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Color("red");
            div.Styles.Count.Should().Be(1);
            div.Styles.ContainsKey("color").Should().BeTrue();
            div.Styles["color"].Should().Be("red");
        }
    }

    public class TextAlign : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddTextAlignShouldUpdateStyle()
        {
            var div = HtmlTags.Div.TextAlign("right");
            div.Styles.Count.Should().Be(1);
            div.Styles.ContainsKey("text-align").Should().BeTrue();
            div.Styles["text-align"].Should().Be("right");
        }
    }

    public class Border : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddBorderShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Border("1px solid red");
            div.Styles.Count.Should().Be(1);
            div.Styles.ContainsKey("border").Should().BeTrue();
            div.Styles["border"].Should().Be("1px solid red");
        }
    }
}
