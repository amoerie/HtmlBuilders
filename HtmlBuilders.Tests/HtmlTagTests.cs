using System;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Xunit;

namespace HtmlBuilders.Tests {
  public class HtmlTagTests {
    public class Append : HtmlTagTests {
      [Fact]
      public void HtmlElementOnHtmlTagWith1Child_ShouldAppendHtmlElement() {
        var div = HtmlTag.Parse("<div><div id='child1'></div></div>");
        div = div.Append(HtmlTags.Div.Id("child2"));
        div.Children.Count().Should().Be(2);
        div.Children.Last()["id"].Should().Be("child2");
      }

      [Fact]
      public void HtmlElementOnHtmlTagWith1TextChild_ShouldAppendHtmlElement() {
        var div = HtmlTag.Parse("<label>This is a label</label>");
        div = div.Append(HtmlTags.I.Class("icon icon-label"));
        div.Children.Count().Should().Be(1); // text nodes don't count as a child, so the count should be 1
        div.Contents.Count().Should().Be(2);
        div.Children.Last()["class"].Should().Be("icon icon-label");
        div.Contents.First().ToHtmlString().Should().Be("This is a label");
      }

      [Fact]
      public void HtmlElementOnHtmlTagWithNoChildren_ShouldAddHtmlElement() {
        var div = HtmlTags.Div;
        var child = HtmlTags.Div;
        div = div.Append(child);
        div.Children.Count().Should().Be(1);
        div.Children.Single().As<object>().Should().Be(child);
      }

      [Fact]
      public void HtmlTextOnHtmlTagWith1ElementChild_ShouldAppendHtmlText() {
        var ul = HtmlTag.Parse("<ul><li>This is the first item</li></ul>");
        ul = ul.Append("These are the items");
        ul.Children.Count().Should().Be(1);
        ul.Contents.Count().Should().Be(2);
        ul.Contents.Last().ToHtmlString().Should().Be("These are the items");
        ul.Contents.First().ToHtmlString().Should().Be("<li>This is the first item</li>");
      }

      [Fact]
      public void WhenAppendingMultipleElements_ShouldRetainOrderOfAddedElements() {
        var div = HtmlTag.Parse("<ul><li>This is the first item</li><li>This is the second item</li></ul>");
        div = div.Append(HtmlTag.Parse("<li>This is the third item</li>"), HtmlTag.Parse("<li>This is the fourth item</li>"));
        var children = div.Children.ToArray();
        children.Length.Should().Be(4);
        children[0].As<object>().Should().Be(HtmlTag.Parse("<li>This is the first item</li>"));
        children[1].As<object>().Should().Be(HtmlTag.Parse("<li>This is the second item</li>"));
        children[2].As<object>().Should().Be(HtmlTag.Parse("<li>This is the third item</li>"));
        children[3].As<object>().Should().Be(HtmlTag.Parse("<li>This is the fourth item</li>"));
      }

      [Fact]
      public void AppendingHtmlContent_ShouldAppendCorrectly() {
        IHtmlContent hiDoctorNick = new HtmlString("<div>Hi doctor Nick!</div>");
        HtmlTag hiEverybody = HtmlTag.Parse("<div>Hi everybody!</div>");

        var result = hiEverybody.Append(hiDoctorNick);

        result.ToHtmlString().Should().Be("<div>Hi everybody!<div>Hi doctor Nick!</div></div>");
      }
    }

    public class Attribute : HtmlTagTests {
      [Fact]
      public void AddingNewAttribute_ShouldHaveNewAttribute() {
        var div = HtmlTags.Div.Attribute("id", "testid");
        div.HasAttribute("id").Should().BeTrue();
        div["id"].Should().Be("testid");
      }

      [Fact]
      public void UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue() {
        var div = HtmlTags.Div.Attribute("id", "testid");
        div.Attribute("id", "newid", false);
        div.HasAttribute("id").Should().BeTrue();
        div["id"].Should().Be("testid");
      }

      [Fact]
      public void UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue() {
        var div = HtmlTags.Div.Attribute("id", "testid").Attribute("id", "newid");
        div.HasAttribute("id").Should().BeTrue();
        div["id"].Should().Be("newid");
      }
    }

    public class Class : HtmlTagTests {
      [Fact]
      public void SettingClassToElementWithClassAttribute_ShouldUpdateClassAttributeWithNewValue() {
        var div = HtmlTags.Div.Class("existing").Class("new");
        div.HasAttribute("class").Should().BeTrue();
        div.HasClass("existing").Should().BeTrue();
        div.HasClass("new").Should().BeTrue();
      }

      [Fact]
      public void SettingClassToElementWithoutClassAttribute_ShouldAddClassAttributeAndValue() {
        var div = HtmlTags.Div.Class("new");
        div.HasAttribute("class").Should().BeTrue();
        div.HasClass("new").Should().BeTrue();
      }
    }

    public class Contents : HtmlTagTests {
      [Fact]
      public void WhenGetting_ShouldReturnCorrectHtmlTag() {
        var tag = HtmlTag.Parse("<div><span>This is the span</span></div>");
        tag.Contents.Count().Should().Be(1);
        tag.Contents.First().ToString().Should().Be(HtmlTags.Span.Append("This is the span").ToString());
      }

      [Fact]
      public void WhenGettingAndContentsAreEmpty_ShouldReturnEmpty() {
        var tag = HtmlTag.Parse("<div></div>");
        tag.Contents.Count().Should().Be(0);
      }
    }

    public class Data : HtmlTagTests {
      [Fact]
      public void WhenAddingAnonymousObjectAndReplaceExistingIsFalse_OnlyNewAttributesAreAdded() {
        var div =
          HtmlTags.Div.Data("data-test", "data test")
            .Data(new {data_test = "new data test", data_test2 = "data test 2", data_test3 = "data test 3"}, false);
        div.HasAttribute("data-test").Should().BeTrue();
        div["data-test"].Should().Be("data test");
        div.HasAttribute("data-test2").Should().BeTrue();
        div["data-test2"].Should().Be("data test 2");
        div.HasAttribute("data-test3").Should().BeTrue();
        div["data-test3"].Should().Be("data test 3");
      }

      [Fact]
      public void WhenAddingAnonymousObjectAndReplaceExistingIsTrue_AllAttributesOfObjectShouldBeAdded() {
        var div =
          HtmlTags.Div.Data(new {data_test = "data test", data_test2 = "data test 2", data_test3 = "data test 3"});
        div.HasAttribute("data-test").Should().BeTrue();
        div["data-test"].Should().Be("data test");
        div.HasAttribute("data-test2").Should().BeTrue();
        div["data-test2"].Should().Be("data test 2");
        div.HasAttribute("data-test3").Should().BeTrue();
        div["data-test3"].Should().Be("data test 3");
      }

      [Fact]
      public void WhenAttributeIsAlreadyPrefixedWithData_AttributeShouldNotBePrefixedAgain() {
        var div = HtmlTags.Div.Data("data-test", "datatest");
        div.HasAttribute("data-test").Should().BeTrue();
        div["data-test"].Should().Be("datatest");
      }

      [Fact]
      public void WhenExistingAttributeAndReplaceExistingIsFalse_OldAttributeValueIsStillPresent() {
        var div = HtmlTags.Div.Data("test", "datatest").Data("test", "new datatest", false);
        div.HasAttribute("data-test").Should().BeTrue();
        div["data-test"].Should().Be("datatest");
      }

      [Fact]
      public void WhenExistingAttributeAndReplaceExistingIsTrue_ExistingAttributeValueShouldBeUpdated() {
        var div = HtmlTags.Div.Data("test", "datatest").Data("test", "new datatest");
        div.HasAttribute("data-test").Should().BeTrue();
        div["data-test"].Should().Be("new datatest");
      }

      [Fact]
      public void WhenNewAttribute_NewDataAttributeShouldBeAdded() {
        var div = HtmlTags.Div.Data("test", "datatest");
        div.HasAttribute("data-test").Should().BeTrue();
        div["data-test"].Should().Be("datatest");
      }
    }

    public new class Equals : HtmlTagTests {
      [Fact]
      public void TwoEmptyHtmlTagsWithDifferentTagNames_ShouldNotBeEqual() {
        var tag1 = HtmlTags.Span;
        var tag2 = HtmlTags.Div;
        tag1.Equals(tag2).Should().BeFalse();
      }

      [Fact]
      public void TwoEmptyHtmlTagsWithSameTagName_ShouldBeEqual() {
        var tag1 = HtmlTags.Div;
        var tag2 = HtmlTags.Div;
        tag1.Equals(tag2).Should().BeTrue();
      }

      [Fact]
      public void TwoHtmlTagsWithDifferentAmountClasses_ShouldNotBeEqual() {
        var tag1 = new HtmlTag("span").Class("class1 class2");
        var tag2 = new HtmlTag("span").Class("class1 class2 class3");
        tag1.Equals(tag2).Should().BeFalse();
      }

      [Fact]
      public void TwoHtmlTagsWithDifferentAmountOfStyles_ShouldNotBeEqual() {
        var tag1 = new HtmlTag("span").Width("10px").Height("15px");
        var tag2 = new HtmlTag("span").Height("15px").Width("10px").Style("padding", "5px");
        tag1.Equals(tag2).Should().BeFalse();
      }

      [Fact]
      public void TwoHtmlTagsWithDifferentChildren_ShouldNotBeEqual() {
        var tag1 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
        var tag2 = HtmlTag.Parse("<div><ul><li>This is some other text<li></ul></div>");
        tag1.Equals(tag2).Should().BeFalse();
      }

      [Fact]
      public void TwoHtmlTagsWithDifferentClasses_ShouldNotBeEqual() {
        var tag1 = new HtmlTag("span").Class("class1 class2");
        var tag2 = new HtmlTag("span").Class("class1 class3");
        tag1.Equals(tag2).Should().BeFalse();
      }

      [Fact]
      public void TwoHtmlTagsWithDifferentStyles_ShouldNotBeEqual() {
        var tag1 = new HtmlTag("span").Width("9px").Height("15px");
        var tag2 = new HtmlTag("span").Height("15px").Width("10px");
        tag1.Equals(tag2).Should().BeFalse();
      }

      [Fact]
      public void TwoHtmlTagsWithSameAttributes_ShouldBeEqual() {
        var tag1 = HtmlTags.Span.Name("test");
        var tag2 = HtmlTags.Span.Name("test");
        tag1.Equals(tag2).Should().BeTrue();
      }

      [Fact]
      public void TwoHtmlTagsWithSameChildrenButWithDifferentAttributes_ShouldNotBeEqual() {
        var tag1 = HtmlTag.Parse("<div><ul><li class='active'>This is some text<li></ul></div>");
        var tag2 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
        tag1.Equals(tag2).Should().BeFalse();
      }

      [Fact]
      public void TwoHtmlTagsWithSameChildren_ShouldBeEqual() {
        var tag1 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
        var tag2 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
        tag1.Equals(tag2).Should().BeTrue();
      }

      [Fact]
      public void TwoHtmlTagsWithSameClasses_ShouldBeEqual() {
        var tag1 = new HtmlTag("span").Class("class1 class2");
        var tag2 = new HtmlTag("span").Class("class1 class2");
        tag1.Equals(tag2).Should().BeTrue();
      }

      [Fact]
      public void TwoHtmlTagsWithSameStyles_ShouldBeEqual() {
        var tag1 = new HtmlTag("span").Width("10px").Height("15px");
        var tag2 = new HtmlTag("span").Height("15px").Width("10px");
        tag1.Equals(tag2).Should().BeTrue();
      }
    }

    public class Find : HtmlTagTests {
      [Fact]
      public void WhenOneGrandchildMatches_ShouldReturnGrandChild() {
        var ul =
          HtmlTag.Parse(
            "<ul><li class='active'><span id='first'>This is the first</span></li><li><label>This is the second</label></li></ul>");
        var first = ul.Find(tag => tag.HasAttribute("id") && tag["id"] == "first").Single();
        first.As<object>().Should().Be(new HtmlTag("span").Id("first").Append("This is the first"));
      }

      [Fact]
      public void WhenOnlyOneChildMatches_ShouldReturnThatChild() {
        var ul = HtmlTag.Parse("<ul><li class='active'>This is the first</li><li>This is the second</li></ul>");
        var active = ul.Find(tag => tag.HasClass("active")).Single();
        active.As<object>().Should().Be(HtmlTags.Li.Class("active").Append("This is the first"));
      }

      [Fact]
      public void WhenThereAreNoChildren_ShouldReturnEmptyEnumerable() {
        HtmlTags.Li.Find(tag => tag.TagName == "li").Should().BeEmpty();
      }
    }

    public class Insert : HtmlTagTests {
      [Fact]
      public void WhenIndexIsEqualToContentsCount_AddsTheElement() {
        var div = HtmlTag.Parse("<ul><li>This is the first item</li><li>This is the second item</li></ul>");
        div.Insert(2, HtmlTags.Li.Append("This is the third item")).Children.Count().Should().Be(3);
      }

      [Fact]
      public void WhenIndexIsLargerThanContentsCount_ThrowsArgumentOutOfRangeException() {
        var div = HtmlTag.Parse("<ul><li>This is the first item</li><li>This is the second item</li></ul>");
        new Action(() => div.Insert(3, HtmlTags.Li.Append("This is the fourth item"))).Should().Throw<IndexOutOfRangeException>();
      }

      [Fact]
      public void WhenIndexIsLowerThanZero_ThrowsArgumentOutOfRangeException() {
        var div = HtmlTag.Parse("<ul><li>This is the first item</li><li>This is the second item</li></ul>");
        new Action(() => div.Insert(-1, HtmlTags.Li.Append("This is the minus first item"))).Should().Throw<IndexOutOfRangeException>();
      }

      [Fact]
      public void WhenInsertingMultipleElements_ShouldRetainOrderOfAddedElements() {
        var div = HtmlTag.Parse("<ul><li>This is the first item</li><li>This is the fourth item</li></ul>");
        div = div.Insert(1, HtmlTag.Parse("<li>This is the second item</li>"), HtmlTag.Parse("<li>This is the third item</li>"));
        var children = div.Children.ToArray();
        children.Length.Should().Be(4);
        children[0].As<object>().Should().Be(HtmlTag.Parse("<li>This is the first item</li>"));
        children[1].As<object>().Should().Be(HtmlTag.Parse("<li>This is the second item</li>"));
        children[2].As<object>().Should().Be(HtmlTag.Parse("<li>This is the third item</li>"));
        children[3].As<object>().Should().Be(HtmlTag.Parse("<li>This is the fourth item</li>"));
      }
    }

    public class ParseAll : HtmlTagTests {
      [Fact]
      public void WhenEmpty_ShouldReturnEmpty() {
        HtmlTag.ParseAll(string.Empty).Should().BeEmpty();
      }

      [Fact]
      public void WhenHtmlIsSelectWithTwoOptions_ShouldContain2Children() {
        var tags = HtmlTag.ParseAll("<select " +
                                    "data-val=\"true\" " +
                                    "data-val-number=\"The field Tarief must be a number.\" " +
                                    "data-val-required=\"Tarief is een vereist veld\" " +
                                    "id=\"DriverDto_Tarrif_ID\" " +
                                    "name=\"DriverDto.Tarrif_ID\">" +
                                    "<option value=\"41\">test</option>" +
                                    "<option value=\"42\">tweede</option>" +
                                    "</select>")
          .OfType<HtmlTag>()
          .ToList();
        tags.Should().HaveCount(1);
        var select = tags.Single();
        select["data-val"].Should().Be("true");
        select["data-val-number"].Should().Be("The field Tarief must be a number.");
        select["data-val-required"].Should().Be("Tarief is een vereist veld");
        select.Children.ToList().Should().HaveCount(2);
        var option41 = select.Children.ElementAt(0);
        option41["value"].Should().Be("41");
        option41.Contents.ToList().Should().HaveCount(1);
        option41.Contents.First().Should().BeOfType<HtmlText>();
        option41.Contents.First().ToString().Should().Be("test");
        var option42 = select.Children.ElementAt(1);
        option42["value"].Should().Be("42");
        option42.Contents.ToList().Should().HaveCount(1);
        option42.Contents.First().Should().BeOfType<HtmlText>();
        option42.Contents.First().ToString().Should().Be("tweede");
      }

      [Fact]
      public void WhenOneElement_ShouldReturnOneElement() {
        var tags = HtmlTag.ParseAll("<select class='test'>" +
                                    "<option>Select</option>" +
                                    "<option value='0'>1</option>" +
                                    "<option value='1'>2</option>" +
                                    "<option value='2'>3</option>" +
                                    "</select>")
          .OfType<HtmlTag>()
          .ToList();
        tags.Count.Should().Be(1);
        var tag = tags.Single();
        tag.TagName.Should().Be("select");
        tag.Children.Count().Should().Be(4);
      }

      [Fact]
      public void WhenTwoElements_ShouldReturnTwoElements() {
        var tags = HtmlTag.ParseAll("<li>The first</li><li>The second</li>").ToArray();
        tags.Length.Should().Be(2);
        tags[0].As<object>().Should().Be(HtmlTags.Li.Append("The first"));
        tags[1].As<object>().Should().Be(HtmlTags.Li.Append("The second"));
      }

      [Fact]
      public void WhenTwoElementsAndSomeText_ShouldReturnTwoElementsAndText() {
        var tags = HtmlTag.ParseAll("Hello I am Jeff<li>The first</li>Is that even valid<li>The second</li>And this is my story").ToArray();
        tags.Length.Should().Be(5);
        tags[0].As<object>().Should().Be(new HtmlText("Hello I am Jeff"));
        tags[1].As<object>().Should().Be(HtmlTags.Li.Append("The first"));
        tags[2].As<object>().Should().Be(new HtmlText("Is that even valid"));
        tags[3].As<object>().Should().Be(HtmlTags.Li.Append("The second"));
        tags[4].As<object>().Should().Be(new HtmlText("And this is my story"));
      }
    }

    public class Parse : HtmlTagTests {
      [Fact]
      public void DivWithoutAttributesWith2Children_ReturnsHtmlTagWithoutAttributesWith2Children() {
        var div = HtmlTag.Parse("<div><a href='testhref'></a><img src='testsrc'/></div>");
        div.TagName.Should().Be("div");
        div.Children.Count().Should().Be(2);

        var a = div.Children.First();
        a.TagName.Should().Be("a");
        a.HasAttribute("href").Should().BeTrue();
        a["href"].Should().Be("testhref");

        var img = div.Children.Last();
        img.TagName.Should().Be("img");
        img.HasAttribute("src").Should().BeTrue();
        img["src"].Should().Be("testsrc");
      }

      [Fact]
      public void EmptyDivWith2Attributes_ReturnsHtmlTagWith2AttributesWithoutContent() {
        var div = HtmlTag.Parse("<div id='testid' name='testname'></div>");
        div.TagName.Should().Be("div");
        div.HasAttribute("id").Should().BeTrue();
        div.HasAttribute("name").Should().BeTrue();
        div["id"].Should().Be("testid");
        div["name"].Should().Be("testname");
        div.Attributes.Count.Should().Be(2);
        div.Contents.Should().BeEmpty();
      }

      [Fact]
      public void EmptyDiv_ReturnsHtmlTagWithoutAttributesOrContent() {
        var div = HtmlTag.Parse("<div></div>");
        div.TagName.Should().Be("div");
        div.Contents.Should().BeEmpty();
        div.Children.Should().BeEmpty();
      }

      [Fact]
      public void WhenHtmlIsSelectWithTwoOptions_ShouldContain2Children() {
        var select = HtmlTag.Parse("<select " +
                                   "data-val=\"true\" " +
                                   "data-val-number=\"The field Tarief must be a number.\" " +
                                   "data-val-required=\"Tarief is een vereist veld\" " +
                                   "id=\"DriverDto_Tarrif_ID\" " +
                                   "name=\"DriverDto.Tarrif_ID\">" +
                                   "<option value=\"41\">test</option>" +
                                   "<option value=\"42\">tweede</option>" +
                                   "</select>");
        select["data-val"].Should().Be("true");
        select["data-val-number"].Should().Be("The field Tarief must be a number.");
        select["data-val-required"].Should().Be("Tarief is een vereist veld");
        select.Children.ToList().Should().HaveCount(2);
        var option41 = select.Children.ElementAt(0);
        option41["value"].Should().Be("41");
        option41.Contents.ToList().Should().HaveCount(1);
        option41.Contents.First().Should().BeOfType<HtmlText>();
        option41.Contents.First().ToString().Should().Be("test");
        var option42 = select.Children.ElementAt(1);
        option42["value"].Should().Be("42");
        option42.Contents.ToList().Should().HaveCount(1);
        option42.Contents.First().Should().BeOfType<HtmlText>();
        option42.Contents.First().ToString().Should().Be("tweede");
      }

      [Fact]
      public void WhenHtmlIsSingleDiv_ShouldReturnSingleDiv() {
        var div = HtmlTag.Parse(@"<div>" +
                                "<input id=\"VehicleDto_Counter1Limit\" name=\"VehicleDto.Counter1Limit\" type=\"text\" value=\"0\" />" +
                                "<div class=\"row minuteSplitter\">" +
                                "<div class=\"col-md-6\">" +
                                "<div class=\"input-group\">" +
                                "<input type=\"number\" class=\"hours\" />" +
                                "<span class=\"input-group-addon\">h</span>" +
                                "</div>" +
                                "</div>" +
                                "<div class=\"col-md-6\">" +
                                "<div class=\"input-group\">" +
                                "<input type=\"number\" class=\"minutes\" />" +
                                "<span class=\"input-group-addon\">m</span>" +
                                "</div>" +
                                "</div>" +
                                "</div>" +
                                "</div>");
        div.Should().NotBeNull();
      }
    }


    public class Prepend : HtmlTagTests {
      [Fact]
      public void HtmlElementOnHtmlTagWith1Child_ShouldPrependHtmlElement() {
        var div = HtmlTag.Parse("<div><div id='child1'></div></div>");
        div = div.Prepend(HtmlTags.Div.Id("child2"));
        div.Children.Count().Should().Be(2);
        div.Children.First()["id"].Should().Be("child2");
      }

      [Fact]
      public void HtmlElementOnHtmlTagWith1TextChild_ShouldPrependHtmlElement() {
        var div = HtmlTag.Parse("<label>This is a label</label>");
        div = div.Prepend(HtmlTags.I.Class("icon icon-label"));
        div.Children.Count().Should().Be(1); // text nodes don't count as a child, so the count should be 1
        div.Contents.Count().Should().Be(2);
        div.Children.First()["class"].Should().Be("icon icon-label");
        div.Contents.Last().ToHtmlString().Should().Be("This is a label");
      }

      [Fact]
      public void HtmlElementOnHtmlTagWithNoChildren_ShouldAddHtmlElement() {
        var div = HtmlTags.Div;
        var child = HtmlTags.Div;
        div = div.Prepend(child);
        div.Children.Count().Should().Be(1);
        div.Children.Single().As<object>().Should().Be(child);
      }

      [Fact]
      public void HtmlTextOnHtmlTagWith1ElementChild_ShouldPrependHtmlText() {
        var div = HtmlTag.Parse("<ul><li>This is the first item</li></ul>");
        div = div.Prepend("These are the items");
        div.Children.Count().Should().Be(1);
        div.Contents.Count().Should().Be(2);
        div.Contents.First().Should().Be(new HtmlText("These are the items"));
        div.Contents.Last().Should().Be(HtmlTag.Parse("<li>This is the first item</li>"));
      }

      [Fact]
      public void ManyElementsOnHtmlTagWith1ElementChild_ShouldPrependHtmlText() {
        var div = HtmlTag.Parse("<ul><li>This is the first item</li></ul>");
        div = div.Prepend(new HtmlText("These are the items"), new HtmlText("So much prepending"));
        div.Children.Count().Should().Be(1);
        var contents = div.Contents.ToArray();
        contents.Length.Should().Be(3);
        contents[0].Should().Be(new HtmlText("These are the items"));
        contents[1].Should().Be(new HtmlText("So much prepending"));
        contents[2].Should().Be(HtmlTag.Parse("<li>This is the first item</li>"));
      }
    }

    public class RemoveClass : HtmlTagTests {
      [Fact]
      public void WhenElementDoesNotHaveClassAttribute_ShouldDoNothing() {
        new Action(() => HtmlTags.Div.RemoveClass("test")).Should().NotThrow();
      }

      [Fact]
      public void WhenElementDoesNotHaveThatClass_ShouldDoNothing() {
        new Action(() => HtmlTags.Div.Class("some other classes").RemoveClass("test")).Should().NotThrow();
      }

      [Fact]
      public void WhenElementHasOnlyThatClass_ShouldRemoveItAndRemoveAttribute() {
        var div = HtmlTags.Div.Class("test").RemoveClass("test");
        div.HasAttribute("class").Should().BeFalse();
        div.HasClass("test").Should().BeFalse();
      }

      [Fact]
      public void WhenElementHasThatClassButAlsoOthers_ShouldRemoveTheClassButKeepTheAttribute() {
        var div = HtmlTags.Div.Class("test othertest").RemoveClass("test");
        div.HasAttribute("class").Should().BeTrue();
        div.HasClass("test").Should().BeFalse();
        div.HasClass("othertest").Should().BeTrue();
      }
    }

    public class RemoveStyle : HtmlTagTests {
      [Fact]
      public void WhenElementDoesNotHaveSuchAStyle_ShouldDoNothing() {
        new Action(() => HtmlTags.Div.Style("height", "15px").RemoveStyle("width")).Should().NotThrow();
      }

      [Fact]
      public void WhenElementDoesntEvenHaveStyleAttribute_ShouldDoNothing() {
        new Action(() => HtmlTags.Div.RemoveStyle("width")).Should().NotThrow();
      }

      [Fact]
      public void WhenElementHasOnlyThatStyle_ShouldRemoveStyle() {
        var div = HtmlTags.Div.Style("width", "15px").RemoveStyle("width");
        div.Styles.ContainsKey("width").Should().BeFalse();
      }

      [Fact]
      public void WhenElementHasThatStyleAndOthers_ShouldRemoveStyle() {
        var div = HtmlTags.Div.Style("width", "15px").Style("height", "15px").RemoveStyle("width");
        div.Styles.ContainsKey("width").Should().BeFalse();
        div.Styles.ContainsKey("height").Should().BeTrue();
        div.Styles["height"].Should().Be("15px");
      }
    }

    public class Style : HtmlTagTests {
      [Fact]
      public void AddingNewStyleToElementWithStyleAttribute_ShouldUpdateStyle() {
        var div = HtmlTags.Div.Style("width", "10px").Style("height", "15px");
        div.HasAttribute("style").Should().BeTrue();
        div.Styles.Count.Should().Be(2);
        div.Styles.ContainsKey("width").Should().BeTrue();
        div.Styles["width"].Should().Be("10px");
        div.Styles.ContainsKey("height").Should().BeTrue();
        div.Styles["height"].Should().Be("15px");
      }

      [Fact]
      public void AddingNewStyleToElementWithoutStyleAttribute_ShouldAddStyle() {
        var div = HtmlTags.Div.Style("width", "10px");
        div.HasAttribute("style").Should().BeTrue();
        div.Styles.Count.Should().Be(1);
        div.Styles.ContainsKey("width").Should().BeTrue();
        div.Styles["width"].Should().Be("10px");
      }

      [Fact]
      public void UpdatingStyleWithReplaceExistingFalse_ShouldNotUpdateStyle() {
        var div = HtmlTags.Div.Style("width", "10px").Style("width", "25px", false);
        div.HasAttribute("style").Should().BeTrue();
        div.Styles.Count.Should().Be(1);
        div.Styles.ContainsKey("width").Should().BeTrue();
        div.Styles["width"].Should().Be("10px");
      }

      [Fact]
      public void UpdatingStyleWithReplaceExistingTrue_ShouldUpdateStyle() {
        var div = HtmlTags.Div.Style("width", "10px").Style("width", "25px");
        div.HasAttribute("style").Should().BeTrue();
        div.Styles.Count.Should().Be(1);
        div.Styles.ContainsKey("width").Should().BeTrue();
        div.Styles["width"].Should().Be("25px");
      }

      [Fact]
      public void WhenStyleIsIEFilter_ShouldNotCrashStyles() {
        var div = HtmlTags.Div.Style("filter",
          "progid:DXImageTransform.Microsoft.gradient(startColorstr='#cccccc', endColorstr='#000000')");
        div.HasAttribute("style").Should().BeTrue();
        div.Styles.Count.Should().Be(1);
        div.Styles.ContainsKey("filter").Should().BeTrue();
        div.Styles["filter"].Should().Be("progid:DXImageTransform.Microsoft.gradient(startColorstr='#cccccc', endColorstr='#000000')");
        var toHtml = div.ToHtmlString();
        toHtml.Should().Be("<div style=\"filter:progid:DXImageTransform.Microsoft.gradient(startColorstr=&#x27;#cccccc&#x27;, endColorstr=&#x27;#000000&#x27;)\"></div>");
      }
    }

    public class ToHtml : HtmlTagTests {
      [Fact]
      public void DivWithThreeLevelsOfChildrenWithAttributesWithTagRenderModeNormal_ShouldRenderNormally() {
        var div =
          HtmlTag.Parse(
            "<div><ul><li class='active'><label data-url='/index'>This is the label &lgt;</label></li></ul></div>");
        var html = div.ToHtmlString();
        var reparsedDiv = HtmlTag.Parse(html);
        div.Equals(reparsedDiv).Should().BeTrue();
      }

      [Fact]
      public void DivWithThreeLevelsOfChildrenWithTagRenderModeNormal_ShouldRenderNormally() {
        var div = HtmlTag.Parse("<div><ul><li><label>This is the label</label></li></ul></div>");
        var html = div.ToHtmlString();
        var reparsedDiv = HtmlTag.Parse(html);
        div.Equals(reparsedDiv).Should().BeTrue();
      }

      [Fact]
      public void DivWithThreeLevelsOfChildrenWithTagRenderModeSelfClosingTag_ShouldThrowInvalidOperationException() {
        var div = HtmlTag.Parse("<div><ul><li><label>This is the label</label></li></ul></div>");
        new Action(() => div.Render(TagRenderMode.SelfClosing).ToHtmlString()).Should().Throw<InvalidOperationException>();
      }

      [Fact]
      public void DivWithThreeLevelsOfChildrenWithTagrenderModeEndTag_ShouldCloseTagsOfDivOnly() {
        var div = HtmlTag.Parse("<div><ul><li><label>This is the label</label></li></ul></div>");
        div.Render(TagRenderMode.EndTag).ToHtmlString().Should().Be("</div>");
      }

      [Fact]
      public void DivWithThreeLevelsOfChildrenWithTagrenderModeStartTag_ShouldOpenTagsOfDivOnly() {
        var div = HtmlTag.Parse("<div><ul><li><label>This is the label</label></li></ul></div>");
        div.Render(TagRenderMode.StartTag).ToHtmlString().Should().Be("<div>");
      }

      [Fact]
      public void ImgShouldBeSelfclosingByDefault() {
        var img = HtmlTags.Img;
        img.ToHtmlString().Should().Be("<img />");
      }

      [Fact]
      public void InputAsChildWithRenderModeSelfClosing_ShouldRenderAsSelfClosing() {
        var div = HtmlTag.Parse("<div><input/></div>");
        var input = div.Children.Single();
        input.Render(TagRenderMode.SelfClosing);
        var html = div.ToHtmlString().Replace(" ", "");
        html.Should().Be("<div><input/></div>");
      }

      [Fact]
      public void InputWithRenderModeSelfClosing_ShouldRenderAsSelfClosing() {
        var input = HtmlTag.Parse("<input/>").Render(TagRenderMode.SelfClosing);
        var html = input.ToHtmlString().Replace(" ", "");
        html.Should().Be("<input/>");
      }
    }

    public class ToggleAttribute : HtmlTagTests {
      [Fact]
      public void WhenFalse_AttributeShouldBeRemoved() {
        var input = new HtmlTag("input").ToggleAttribute("disabled", true).ToggleAttribute("disabled", false);
        input.HasAttribute("disabled").Should().BeFalse();
      }

      [Fact]
      public void WhenTrue_AttributeShouldBeAdded() {
        var input = new HtmlTag("input").ToggleAttribute("disabled", true);
        input.HasAttribute("disabled").Should().BeTrue();
        input["disabled"].Should().Be("disabled");
      }
    }
  }
}