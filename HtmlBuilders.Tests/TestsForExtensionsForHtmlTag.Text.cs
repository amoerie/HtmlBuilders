using NUnit.Framework;

namespace HtmlBuilders.Tests {
  [TestFixture]
  public partial class TestsForExtensionsForHtmlTag {

    [Test]
    public void Text_WhenTagIsSimpleLabel_ShouldReturnLabelContents() {
      var tag = HtmlTags.Label.Append("This is the content");
      var text = tag.Text();
      Assert.That(text, Is.EqualTo("This is the content"));
    }

    [Test]
    public void Text_WhenTagIsEmpty_ShouldReturnEmptyString() {
      var tag = HtmlTags.Label;
      var text = tag.Text();
      Assert.That(text, Is.Not.Null.And.Empty);
    }

    [Test]
    public void Text_WhenTagContainsMultipleChildren_ShouldReturnAllContentsOfChildren() {
      var tag = HtmlTag.Parse("<div class='readonlygroup period'><label>Period</label><span>Friday 17 October 2014 - Thursday 30 October 2014</span></div>");
      var text = tag.Text();
      Assert.That(text, Is.EqualTo("PeriodFriday 17 October 2014 - Thursday 30 October 2014"));
    }

    [Test]
    public void Text_WhenTagContainsMultipleEmptyChildren_ShouldReturnEmptyString() {
      var tag = HtmlTags.Div.Append(HtmlTags.Label).Append(HtmlTags.Div);
      var text = tag.Text();
      Assert.That(text, Is.Not.Null.And.Empty);
    }
  }
}