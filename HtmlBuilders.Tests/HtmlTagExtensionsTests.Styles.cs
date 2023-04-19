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
}
