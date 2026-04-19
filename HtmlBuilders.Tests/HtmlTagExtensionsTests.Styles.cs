using System.Collections;
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
            Assert.True(div.HasAttribute("style"));
            Assert.Equal(2, div.Styles.Count);
            Assert.True(div.Styles.ContainsKey("height"));
            Assert.Equal("10px", div.Styles["padding"]);
            Assert.True(div.Styles.ContainsKey("height"));
            Assert.Equal("15px", div.Styles["height"]);
        }

        [Fact]
        public void AddingNewHeightToElementWithoutStyleAttributeShouldAddStyleAndHeight()
        {
            var div = HtmlTags.Div.Height("10px");
            Assert.True(div.HasAttribute("style"));
            Assert.Single(div.Styles);
            Assert.True(div.Styles.ContainsKey("height"));
            Assert.Equal("10px", div.Styles["height"]);
        }

        [Fact]
        public void UpdatingHeightWithReplaceExistingFalseShouldNotUpdateHeight()
        {
            var div = HtmlTags.Div.Height("10px").Height("25px", false);
            Assert.True(div.HasAttribute("style"));
            Assert.Single(div.Styles);
            Assert.True(div.Styles.ContainsKey("height"));
            Assert.Equal("10px", div.Styles["height"]);
        }

        [Fact]
        public void UpdatingHeightWithReplaceExistingTrueShouldUpdateHeight()
        {
            var div = HtmlTags.Div.Height("10px").Height("25px");
            Assert.True(div.HasAttribute("style"));
            Assert.Single(div.Styles);
            Assert.True(div.Styles.ContainsKey("height"));
            Assert.Equal("25px", div.Styles["height"]);
        }
    }

    public class Width : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddingNewWidthToElementWithStyleAttributeShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Style("padding", "10px").Width("15px");
            Assert.True(div.HasAttribute("style"));
            Assert.Equal(2, div.Styles.Count);
            Assert.True(div.Styles.ContainsKey("width"));
            Assert.Equal("10px", div.Styles["padding"]);
            Assert.True(div.Styles.ContainsKey("width"));
            Assert.Equal("15px", div.Styles["width"]);
        }

        [Fact]
        public void AddingNewWidthToElementWithoutStyleAttributeShouldAddStyleAndWidth()
        {
            var div = HtmlTags.Div.Width("10px");
            Assert.True(div.HasAttribute("style"));
            Assert.Single(div.Styles);
            Assert.True(div.Styles.ContainsKey("width"));
            Assert.Equal("10px", div.Styles["width"]);
        }

        [Fact]
        public void UpdatingWidthWithReplaceExistingFalseShouldNotUpdateWidth()
        {
            var div = HtmlTags.Div.Width("10px").Width("25px", false);
            Assert.True(div.HasAttribute("style"));
            Assert.Single(div.Styles);
            Assert.True(div.Styles.ContainsKey("width"));
            Assert.Equal("10px", div.Styles["width"]);
        }

        [Fact]
        public void UpdatingWidthWithReplaceExistingTrueShouldUpdateWidth()
        {
            var div = HtmlTags.Div.Width("10px").Width("25px");
            Assert.True(div.HasAttribute("style"));
            Assert.Single(div.Styles);
            Assert.True(div.Styles.ContainsKey("width"));
            Assert.Equal("25px", div.Styles["width"]);
        }
    }

    public class Margin : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddMarginShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Margin("10px");
            Assert.Single(div.Styles);
            Assert.True(div.Styles.ContainsKey("margin"));
            Assert.Equal("10px", div.Styles["margin"]);
        }
    }

    public class Padding : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddPaddingShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Padding("10px");
            Assert.Single(div.Styles);
            Assert.True(div.Styles.ContainsKey("padding"));
            Assert.Equal("10px", div.Styles["padding"]);
        }
    }

    public class Color : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddColorShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Color("red");
            Assert.Single(div.Styles);
            Assert.True(div.Styles.ContainsKey("color"));
            Assert.Equal("red", div.Styles["color"]);
        }
    }

    public class TextAlign : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddTextAlignShouldUpdateStyle()
        {
            var div = HtmlTags.Div.TextAlign("right");
            Assert.Single(div.Styles);
            Assert.True(div.Styles.ContainsKey("text-align"));
            Assert.Equal("right", div.Styles["text-align"]);
        }
    }

    public class Border : HtmlTagExtensionsTests
    {
        [Fact]
        public void AddBorderShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Border("1px solid red");
            Assert.Single(div.Styles);
            Assert.True(div.Styles.ContainsKey("border"));
            Assert.Equal("1px solid red", div.Styles["border"]);
        }
    }
}
