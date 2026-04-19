using Xunit;

namespace HtmlBuilders.Tests;

public partial class HtmlTagExtensionsTests
{
    public class Text : HtmlTagExtensionsTests
    {
        [Fact]
        public void WhenTagIsSimpleLabelShouldReturnLabelContents()
        {
            var tag = HtmlTags.Label.Append("This is the content");
            var text = tag.Text();
            Assert.Equal("This is the content", text);
        }

        [Fact]
        public void WhenTagIsEmptyShouldReturnEmptyString()
        {
            var tag = HtmlTags.Label;
            var text = tag.Text();
            Assert.NotNull(text);
            Assert.Empty(text ?? "");
        }

        [Fact]
        public void WhenTagContainsMultipleChildrenShouldReturnAllContentsOfChildren()
        {
            var tag = HtmlTag.Parse(
                "<div class='readonlygroup period'><label>Period</label><span>Friday 17 October 2014 - Thursday 30 October 2014</span></div>"
            );
            var text = tag.Text();
            Assert.Equal("PeriodFriday 17 October 2014 - Thursday 30 October 2014", text);
        }

        [Fact]
        public void WhenTagContainsMultipleEmptyChildrenShouldReturnEmptyString()
        {
            var tag = HtmlTags.Div.Append(HtmlTags.Label).Append(HtmlTags.Div);
            var text = tag.Text();
            Assert.NotNull(text);
            Assert.Empty(text ?? "");
        }

        [Fact]
        public void WhenTagIsNullShouldReturnEmptyString()
        {
            var tag = (HtmlTag?)null;
            var text = tag.Text();
            Assert.NotNull(text);
            Assert.Empty(text ?? "");
        }
    }
}
