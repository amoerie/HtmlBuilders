using Xunit;

namespace HtmlBuilders.Tests;

public partial class HtmlTagExtensionsTests
{
    public class Checked : HtmlTagExtensionsTests
    {
        [Fact]
        public void WhenFalseAttributeShouldBeRemoved()
        {
            var input = new HtmlTag("input").Checked(true).Checked(false);
            Assert.False(input.HasAttribute("checked"));
        }

        [Fact]
        public void WhenTrueAttributeShouldBeAdded()
        {
            var input = new HtmlTag("input").Checked(true);
            Assert.True(input.HasAttribute("checked"));
            Assert.Equal("checked", input["checked"]);
        }
    }

    public class Disabled : HtmlTagExtensionsTests
    {
        [Fact]
        public void WhenFalseAttributeShouldBeRemoved()
        {
            var input = new HtmlTag("input").Disabled(true).Disabled(false);
            Assert.False(input.HasAttribute("disabled"));
        }

        [Fact]
        public void WhenTrueAttributeShouldBeAdded()
        {
            var input = new HtmlTag("input").Disabled(true);
            Assert.True(input.HasAttribute("disabled"));
            Assert.Equal("disabled", input["disabled"]);
        }
    }

    public class Readonly : HtmlTagExtensionsTests
    {
        [Fact]
        public void WhenFalseAttributeShouldBeRemoved()
        {
            var input = new HtmlTag("input").Readonly(true).Readonly(false);

            Assert.False(input.HasAttribute("readonly"));
        }

        [Fact]
        public void WhenTrueAttributeShouldBeAdded()
        {
            var input = new HtmlTag("input").Readonly(true);
            Assert.True(input.HasAttribute("readonly"));
            Assert.Equal("readonly", input["readonly"]);
        }
    }

    public class Selected : HtmlTagExtensionsTests
    {
        [Fact]
        public void WhenFalseAttributeShouldBeRemoved()
        {
            var input = new HtmlTag("input").Selected(true).Selected(false);
            Assert.False(input.HasAttribute("selected"));
        }

        [Fact]
        public void WhenTrueAttributeShouldBeAdded()
        {
            var input = new HtmlTag("input").Selected(true);
            Assert.True(input.HasAttribute("selected"));
            Assert.Equal("selected", input["selected"]);
        }
    }
}
