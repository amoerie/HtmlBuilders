using FluentAssertions;
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
            input.HasAttribute("checked").Should().BeFalse();
        }

        [Fact]
        public void WhenTrueAttributeShouldBeAdded()
        {
            var input = new HtmlTag("input").Checked(true);
            input.HasAttribute("checked").Should().BeTrue();
            input["checked"].Should().Be("checked");
        }
    }

    public class Disabled : HtmlTagExtensionsTests
    {
        [Fact]
        public void WhenFalseAttributeShouldBeRemoved()
        {
            var input = new HtmlTag("input").Disabled(true).Disabled(false);
            input.HasAttribute("disabled").Should().BeFalse();
        }

        [Fact]
        public void WhenTrueAttributeShouldBeAdded()
        {
            var input = new HtmlTag("input").Disabled(true);
            input.HasAttribute("disabled").Should().BeTrue();
            input["disabled"].Should().Be("disabled");
        }
    }

    public class Readonly : HtmlTagExtensionsTests
    {
        [Fact]
        public void WhenFalseAttributeShouldBeRemoved()
        {
            var input = new HtmlTag("input").Readonly(true).Readonly(false);

            input.HasAttribute("readonly").Should().BeFalse();
        }

        [Fact]
        public void WhenTrueAttributeShouldBeAdded()
        {
            var input = new HtmlTag("input").Readonly(true);
            input.HasAttribute("readonly").Should().BeTrue();
            input["readonly"].Should().Be("readonly");
        }
    }

    public class Selected : HtmlTagExtensionsTests
    {
        [Fact]
        public void WhenFalseAttributeShouldBeRemoved()
        {
            var input = new HtmlTag("input").Selected(true).Selected(false);
            input.HasAttribute("selected").Should().BeFalse();
        }

        [Fact]
        public void WhenTrueAttributeShouldBeAdded()
        {
            var input = new HtmlTag("input").Selected(true);
            input.HasAttribute("selected").Should().BeTrue();
            input["selected"].Should().Be("selected");
        }
    }
}
