using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Xunit;

namespace HtmlBuilders.Tests;

public class HtmlTagTests
{
    public class Append : HtmlTagTests
    {
        [Fact]
        public void HtmlElementOnHtmlTagWith1ChildShouldAppendHtmlElement()
        {
            var div = HtmlTag.Parse("<div><div id='child1'></div></div>");
            div = div.Append(HtmlTags.Div.Id("child2"));
            Assert.Equal(2, div.Children.Count());
            Assert.Equal("child2", div.Children.Last()["id"]);
        }

        [Fact]
        public void HtmlElementOnHtmlTagWith1TextChildShouldAppendHtmlElement()
        {
            var div = HtmlTag.Parse("<label>This is a label</label>");
            div = div.Append(HtmlTags.I.Class("icon icon-label"));
            Assert.Single(div.Children); // text nodes don't count as a child, so the count should be 1
            Assert.Equal(2, div.Contents.Count);
            Assert.Equal("icon icon-label", div.Children.Last()["class"]);
            Assert.Equal("This is a label", div.Contents[0].ToHtmlString());
        }

        [Fact]
        public void HtmlElementOnHtmlTagWithNoChildrenShouldAddHtmlElement()
        {
            var div = HtmlTags.Div;
            var child = HtmlTags.Div;
            div = div.Append(child);
            Assert.Single(div.Children);
            Assert.Equal(child, div.Children.Single());
        }

        [Fact]
        public void HtmlTextOnHtmlTagWith1ElementChildShouldAppendHtmlText()
        {
            var ul = HtmlTag.Parse("<ul><li>This is the first item</li></ul>");
            ul = ul.Append("These are the items");
            Assert.Single(ul.Children);
            Assert.Equal(2, ul.Contents.Count);
            Assert.Equal("<li>This is the first item</li>", ul.Contents[0].ToHtmlString());
            Assert.Equal("These are the items", ul.Contents[1].ToHtmlString());
        }

        [Fact]
        public void WhenAppendingMultipleElementsShouldRetainOrderOfAddedElements()
        {
            var div = HtmlTag.Parse(
                "<ul><li>This is the first item</li><li>This is the second item</li></ul>"
            );
            div = div.Append(
                HtmlTag.Parse("<li>This is the third item</li>"),
                HtmlTag.Parse("<li>This is the fourth item</li>")
            );
            var children = div.Children.ToArray();
            Assert.Equal(4, children.Length);
            Assert.Equal(HtmlTag.Parse("<li>This is the first item</li>"), children[0]);
            Assert.Equal(HtmlTag.Parse("<li>This is the second item</li>"), children[1]);
            Assert.Equal(HtmlTag.Parse("<li>This is the third item</li>"), children[2]);
            Assert.Equal(HtmlTag.Parse("<li>This is the fourth item</li>"), children[3]);
        }

        [Fact]
        public void AppendingHtmlContentShouldAppendCorrectly()
        {
            IHtmlContent hiDoctorNick = new HtmlString("<div>Hi doctor Nick!</div>");
            var hiEverybody = HtmlTag.Parse("<div>Hi everybody!</div>");

            var result = hiEverybody.Append(hiDoctorNick);

            Assert.Equal(
                "<div>Hi everybody!<div>Hi doctor Nick!</div></div>",
                result.ToHtmlString()
            );
        }

        [Fact]
        public void AppendingFreeHtmlContentShouldAppendCorrectly()
        {
            var script = HtmlTags.Script.Append(
                new HtmlString("var pathToToc = \"/toc-placeholder.json\";")
            );

            Assert.Equal(
                "<script>var pathToToc = \"/toc-placeholder.json\";</script>",
                script.ToHtmlString()
            );
        }

        [Fact]
        public void AppendingNonBreakingSpaceShouldAppendCorrectly()
        {
            var label = HtmlTags.Label.Append("Bonjour");

            label = label.Append(new HtmlString("&nbsp;"));
            label = label.Append("It is I");

            Assert.Equal("<label>Bonjour&nbsp;It is I</label>", label.ToHtmlString());
        }

        [Fact]
        public void AppendingNullShouldReturnThis()
        {
            var label = HtmlTags.Label.Append("Bonjour");

            Assert.Same(label, label.Append((IEnumerable<IHtmlContent?>?)null));
            Assert.Same(label, label.Append((IEnumerable<IHtmlElement?>?)null));
            Assert.Same(label, label.Append((IHtmlContent?[]?)null));
            Assert.Same(label, label.Append((IHtmlElement?[]?)null));
            Assert.Same(label, label.Append((IHtmlContent?)null));
            Assert.Same(label, label.Append((IHtmlElement?)null));
        }

        [Fact]
        public void AppendingSomeNullsShouldIgnoreNulls()
        {
            var label = HtmlTags.Label;

            label = label.Append(
                HtmlTags.Span.Append("This is "),
                null,
                HtmlTags.Span.Append("Sparta")
            );

            Assert.Equal(
                "<label><span>This is </span><span>Sparta</span></label>",
                label.ToHtmlString()
            );
        }
    }

    public class Attribute : HtmlTagTests
    {
        [Fact]
        public void AddingNewAttributeShouldHaveNewAttribute()
        {
            var div = HtmlTags.Div.Attribute("id", "testid");
            Assert.True(div.HasAttribute("id"));
            Assert.Equal("testid", div["id"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingFalseShouldStillHaveOldAttributeValue()
        {
            var div = HtmlTags.Div.Attribute("id", "testid");
            div.Attribute("id", "newid", false);
            Assert.True(div.HasAttribute("id"));
            Assert.Equal("testid", div["id"]);
        }

        [Fact]
        public void UpdatingOldAttributeWithReplaceExistingTrueShouldHaveUpdatedAttributeValue()
        {
            var div = HtmlTags.Div.Attribute("id", "testid").Attribute("id", "newid");
            Assert.True(div.HasAttribute("id"));
            Assert.Equal("newid", div["id"]);
        }
    }

    public class Class : HtmlTagTests
    {
        [Fact]
        public void SettingClassToElementWithClassAttributeShouldUpdateClassAttributeWithNewValue()
        {
            var div = HtmlTags.Div.Class("existing").Class("new");
            Assert.True(div.HasAttribute("class"));
            Assert.True(div.HasClass("existing"));
            Assert.True(div.HasClass("new"));
        }

        [Fact]
        public void SettingClassToElementWithoutClassAttributeShouldAddClassAttributeAndValue()
        {
            var div = HtmlTags.Div.Class("new");
            Assert.True(div.HasAttribute("class"));
            Assert.True(div.HasClass("new"));
        }
    }

    public class Contents : HtmlTagTests
    {
        [Fact]
        public void WhenGettingShouldReturnCorrectHtmlTag()
        {
            var tag = HtmlTag.Parse("<div><span>This is the span</span></div>");
            Assert.Single(tag.Contents);
            Assert.Equal(
                HtmlTags.Span.Append("This is the span").ToString(),
                tag.Contents[0].ToString()
            );
        }

        [Fact]
        public void WhenGettingAndContentsAreEmptyShouldReturnEmpty()
        {
            var tag = HtmlTag.Parse("<div></div>");
            Assert.Empty(tag.Contents);
        }
    }

    public class Data : HtmlTagTests
    {
        [Fact]
        public void WhenAddingAnonymousObjectAndReplaceExistingIsFalseOnlyNewAttributesAreAdded()
        {
            var div = HtmlTags
                .Div.Data("data-test", "data test")
                .Data(
                    new
                    {
                        data_test = "new data test",
                        data_test2 = "data test 2",
                        data_test3 = "data test 3",
                    },
                    false
                );
            Assert.True(div.HasAttribute("data-test"));
            Assert.Equal("data test", div["data-test"]);
            Assert.True(div.HasAttribute("data-test2"));
            Assert.Equal("data test 2", div["data-test2"]);
            Assert.True(div.HasAttribute("data-test3"));
            Assert.Equal("data test 3", div["data-test3"]);
        }

        [Fact]
        public void WhenAddingAnonymousObjectAndReplaceExistingIsTrueAllAttributesOfObjectShouldBeAdded()
        {
            var div = HtmlTags.Div.Data(
                new
                {
                    data_test = "data test",
                    data_test2 = "data test 2",
                    data_test3 = "data test 3",
                }
            );
            Assert.True(div.HasAttribute("data-test"));
            Assert.Equal("data test", div["data-test"]);
            Assert.True(div.HasAttribute("data-test2"));
            Assert.Equal("data test 2", div["data-test2"]);
            Assert.True(div.HasAttribute("data-test3"));
            Assert.Equal("data test 3", div["data-test3"]);
        }

        [Fact]
        public void WhenAttributeIsAlreadyPrefixedWithDataAttributeShouldNotBePrefixedAgain()
        {
            var div = HtmlTags.Div.Data("data-test", "datatest");
            Assert.True(div.HasAttribute("data-test"));
            Assert.Equal("datatest", div["data-test"]);
        }

        [Fact]
        public void WhenExistingAttributeAndReplaceExistingIsFalseOldAttributeValueIsStillPresent()
        {
            var div = HtmlTags.Div.Data("test", "datatest").Data("test", "new datatest", false);
            Assert.True(div.HasAttribute("data-test"));
            Assert.Equal("datatest", div["data-test"]);
        }

        [Fact]
        public void WhenExistingAttributeAndReplaceExistingIsTrueExistingAttributeValueShouldBeUpdated()
        {
            var div = HtmlTags.Div.Data("test", "datatest").Data("test", "new datatest");
            Assert.True(div.HasAttribute("data-test"));
            Assert.Equal("new datatest", div["data-test"]);
        }

        [Fact]
        public void WhenNewAttributeNewDataAttributeShouldBeAdded()
        {
            var div = HtmlTags.Div.Data("test", "datatest");
            Assert.True(div.HasAttribute("data-test"));
            Assert.Equal("datatest", div["data-test"]);
        }
    }

    public new class Equals : HtmlTagTests
    {
        [Fact]
        public void TwoEmptyHtmlTagsWithDifferentTagNamesShouldNotBeEqual()
        {
            var tag1 = HtmlTags.Span;
            var tag2 = HtmlTags.Div;
            Assert.False(tag1.Equals(tag2));
        }

        [Fact]
        public void TwoEmptyHtmlTagsWithSameTagNameShouldBeEqual()
        {
            var tag1 = HtmlTags.Div;
            var tag2 = HtmlTags.Div;
            Assert.True(tag1.Equals(tag2));
        }

        [Fact]
        public void TwoHtmlTagsWithDifferentAmountClassesShouldNotBeEqual()
        {
            var tag1 = new HtmlTag("span").Class("class1 class2");
            var tag2 = new HtmlTag("span").Class("class1 class2 class3");
            Assert.False(tag1.Equals(tag2));
        }

        [Fact]
        public void TwoHtmlTagsWithDifferentAmountOfStylesShouldNotBeEqual()
        {
            var tag1 = new HtmlTag("span").Width("10px").Height("15px");
            var tag2 = new HtmlTag("span").Height("15px").Width("10px").Style("padding", "5px");
            Assert.False(tag1.Equals(tag2));
        }

        [Fact]
        public void TwoHtmlTagsWithDifferentChildrenShouldNotBeEqual()
        {
            var tag1 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
            var tag2 = HtmlTag.Parse("<div><ul><li>This is some other text<li></ul></div>");
            Assert.False(tag1.Equals(tag2));
        }

        [Fact]
        public void TwoHtmlTagsWithDifferentClassesShouldNotBeEqual()
        {
            var tag1 = new HtmlTag("span").Class("class1 class2");
            var tag2 = new HtmlTag("span").Class("class1 class3");
            Assert.False(tag1.Equals(tag2));
        }

        [Fact]
        public void TwoHtmlTagsWithDifferentStylesShouldNotBeEqual()
        {
            var tag1 = new HtmlTag("span").Width("9px").Height("15px");
            var tag2 = new HtmlTag("span").Height("15px").Width("10px");
            Assert.False(tag1.Equals(tag2));
        }

        [Fact]
        public void TwoHtmlTagsWithSameAttributesShouldBeEqual()
        {
            var tag1 = HtmlTags.Span.Name("test");
            var tag2 = HtmlTags.Span.Name("test");
            Assert.True(tag1.Equals(tag2));
        }

        [Fact]
        public void TwoHtmlTagsWithSameChildrenButWithDifferentAttributesShouldNotBeEqual()
        {
            var tag1 = HtmlTag.Parse(
                "<div><ul><li class='active'>This is some text<li></ul></div>"
            );
            var tag2 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
            Assert.False(tag1.Equals(tag2));
        }

        [Fact]
        public void TwoHtmlTagsWithSameChildrenShouldBeEqual()
        {
            var tag1 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
            var tag2 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
            Assert.True(tag1.Equals(tag2));
        }

        [Fact]
        public void TwoHtmlTagsWithSameClassesShouldBeEqual()
        {
            var tag1 = new HtmlTag("span").Class("class1 class2");
            var tag2 = new HtmlTag("span").Class("class1 class2");
            Assert.True(tag1.Equals(tag2));
        }

        [Fact]
        public void TwoHtmlTagsWithSameStylesShouldBeEqual()
        {
            var tag1 = new HtmlTag("span").Width("10px").Height("15px");
            var tag2 = new HtmlTag("span").Height("15px").Width("10px");
            Assert.True(tag1.Equals(tag2));
        }
    }

    public class Find : HtmlTagTests
    {
        [Fact]
        public void WhenOneGrandchildMatchesShouldReturnGrandChild()
        {
            var ul = HtmlTag.Parse(
                "<ul><li class='active'><span id='first'>This is the first</span></li><li><label>This is the second</label></li></ul>"
            );
            var first = ul.Find(tag => tag.HasAttribute("id") && tag["id"] == "first").Single();
            Assert.Equal(new HtmlTag("span").Id("first").Append("This is the first"), first);
        }

        [Fact]
        public void WhenOnlyOneChildMatchesShouldReturnThatChild()
        {
            var ul = HtmlTag.Parse(
                "<ul><li class='active'>This is the first</li><li>This is the second</li></ul>"
            );
            var active = ul.Find(tag => tag.HasClass("active")).Single();
            Assert.Equal(HtmlTags.Li.Class("active").Append("This is the first"), active);
        }

        [Fact]
        public void WhenThereAreNoChildrenShouldReturnEmptyEnumerable() =>
            Assert.Empty(HtmlTags.Li.Find(tag => tag.TagName == "li") ?? []);
    }

    public class Insert : HtmlTagTests
    {
        [Fact]
        public void WhenIndexIsEqualToContentsCountAddsTheElement()
        {
            var div = HtmlTag.Parse(
                "<ul><li>This is the first item</li><li>This is the second item</li></ul>"
            );
            Assert.Equal(
                3,
                div.Insert(2, HtmlTags.Li.Append("This is the third item")).Children.Count()
            );
        }

        [Fact]
        public void WhenIndexIsLargerThanContentsCountThrowsArgumentOutOfRangeException()
        {
            var div = HtmlTag.Parse(
                "<ul><li>This is the first item</li><li>This is the second item</li></ul>"
            );
            Assert.Throws<ArgumentException>(() =>
                div.Insert(3, HtmlTags.Li.Append("This is the fourth item"))
            );
        }

        [Fact]
        public void WhenIndexIsLowerThanZeroThrowsArgumentOutOfRangeException()
        {
            var div = HtmlTag.Parse(
                "<ul><li>This is the first item</li><li>This is the second item</li></ul>"
            );
            Assert.Throws<ArgumentException>(() =>
                div.Insert(-1, HtmlTags.Li.Append("This is the minus first item"))
            );
        }

        [Fact]
        public void WhenInsertingMultipleElementsShouldRetainOrderOfAddedElements()
        {
            var div = HtmlTag.Parse(
                "<ul><li>This is the first item</li><li>This is the fourth item</li></ul>"
            );
            div = div.Insert(
                1,
                HtmlTag.Parse("<li>This is the second item</li>"),
                HtmlTag.Parse("<li>This is the third item</li>")
            );
            var children = div.Children.ToArray();
            Assert.Equal(4, children.Length);
            Assert.Equal(HtmlTag.Parse("<li>This is the first item</li>"), children[0]);
            Assert.Equal(HtmlTag.Parse("<li>This is the second item</li>"), children[1]);
            Assert.Equal(HtmlTag.Parse("<li>This is the third item</li>"), children[2]);
            Assert.Equal(HtmlTag.Parse("<li>This is the fourth item</li>"), children[3]);
        }

        [Fact]
        public void InsertingNullShouldReturnThis()
        {
            var label = HtmlTags.Label.Append("Bonjour");

            Assert.Same(label, label.Insert(0, (IEnumerable<IHtmlElement?>?)null));
            Assert.Same(label, label.Insert(0, (IHtmlElement?[]?)null));
            Assert.Same(label, label.Insert(0, (IHtmlElement?)null));
        }

        [Fact]
        public void AppendingSomeNullsShouldIgnoreNulls()
        {
            var label = HtmlTags.Label;

            label = label.Insert(
                0,
                HtmlTags.Span.Append("This is "),
                null,
                HtmlTags.Span.Append("Sparta")
            );

            Assert.Equal(
                "<label><span>This is </span><span>Sparta</span></label>",
                label.ToHtmlString()
            );
        }
    }

    public class ParseAll : HtmlTagTests
    {
        [Fact]
        public void WhenEmptyShouldReturnEmpty() =>
            Assert.Empty(HtmlTag.ParseAll(string.Empty) ?? []);

        [Fact]
        public void WhenHtmlIsSelectWithTwoOptionsShouldContain2Children()
        {
            var tags = HtmlTag
                .ParseAll(
                    "<select "
                        + "data-val=\"true\" "
                        + "data-val-number=\"The field Tarief must be a number.\" "
                        + "data-val-required=\"Tarief is een vereist veld\" "
                        + "id=\"DriverDto_Tarrif_ID\" "
                        + "name=\"DriverDto.Tarrif_ID\">"
                        + "<option value=\"41\">test</option>"
                        + "<option value=\"42\">tweede</option>"
                        + "</select>"
                )
                .OfType<HtmlTag>()
                .ToList();
            Assert.Single(tags);
            var select = tags.Single();
            Assert.Equal("true", select["data-val"]);
            Assert.Equal("The field Tarief must be a number.", select["data-val-number"]);
            Assert.Equal("Tarief is een vereist veld", select["data-val-required"]);
            Assert.Equal(2, select.Children.Count());
            var option41 = select.Children.ElementAt(0);
            Assert.Equal("41", option41["value"]);
            Assert.Single(option41.Contents.ToList() ?? []);
            Assert.True((option41.Contents[0]) is HtmlText);
            Assert.Equal("test", option41.Contents[0].ToString());
            var option42 = select.Children.ElementAt(1);
            Assert.Equal("42", option42["value"]);
            Assert.Single(option42.Contents.ToList() ?? []);
            Assert.True((option42.Contents[0]) is HtmlText);
            Assert.Equal("tweede", option42.Contents[0].ToString());
        }

        [Fact]
        public void WhenOneElementShouldReturnOneElement()
        {
            var tags = HtmlTag
                .ParseAll(
                    "<select class='test'>"
                        + "<option>Select</option>"
                        + "<option value='0'>1</option>"
                        + "<option value='1'>2</option>"
                        + "<option value='2'>3</option>"
                        + "</select>"
                )
                .OfType<HtmlTag>()
                .ToList();
            Assert.Single(tags);
            var tag = tags.Single();
            Assert.Equal("select", tag.TagName);
            Assert.Equal(4, tag.Children.Count());
        }

        [Fact]
        public void WhenTwoElementsShouldReturnTwoElements()
        {
            var tags = HtmlTag.ParseAll("<li>The first</li><li>The second</li>").ToArray();
            Assert.Equal(2, tags.Length);
            Assert.Equal(HtmlTags.Li.Append("The first"), tags[0]);
            Assert.Equal(HtmlTags.Li.Append("The second"), tags[1]);
        }

        [Fact]
        public void WhenTwoElementsAndSomeTextShouldReturnTwoElementsAndText()
        {
            var tags = HtmlTag
                .ParseAll(
                    "Hello I am Jeff<li>The first</li>Is that even valid<li>The second</li>And this is my story"
                )
                .ToArray();
            Assert.Equal(5, tags.Length);
            Assert.Equal(new HtmlText("Hello I am Jeff"), tags[0]);
            Assert.Equal(HtmlTags.Li.Append("The first"), tags[1]);
            Assert.Equal(new HtmlText("Is that even valid"), tags[2]);
            Assert.Equal(HtmlTags.Li.Append("The second"), tags[3]);
            Assert.Equal(new HtmlText("And this is my story"), tags[4]);
        }

        [Fact]
        public void WhenElementIsAlreadyAnHtmlTagShouldReuseTag()
        {
            var content = HtmlTags.Label.Append("Hello");
            var parsed = HtmlTag.ParseAll(content).ToArray();
            Assert.Single(parsed);
            Assert.Same(content, parsed[0]);
        }

        [Fact]
        public void WhenElementIsAlreadyAStringHtmlContentShouldReuseContent()
        {
            var content = new StringHtmlContent("Hello");
            var parsed = HtmlTag.ParseAll(content).ToArray();
            Assert.Single(parsed);
            var text = parsed[0];
            Assert.True((text) is HtmlText);
            Assert.Equal("Hello", ((HtmlText)text).ToHtmlString());
        }

        [Fact]
        public void WhenElementIsAlreadyATagBuilderShouldReuseProperties()
        {
            var ul = new TagBuilder("ul");
            var li = new TagBuilder("li");
            ul.MergeAttribute("class", "sample-list");
            li.MergeAttribute("class", "sample-list-item");
            ul.InnerHtml.AppendHtml(li);
            var parsed = HtmlTag.ParseAll(ul).ToArray();
            Assert.Single(parsed);
            Assert.Equal(
                HtmlTags.Ul.Class("sample-list").Append(HtmlTags.Li.Class("sample-list-item")),
                parsed[0]
            );
        }
    }

    public class Parse : HtmlTagTests
    {
        [Fact]
        public void DivWithoutAttributesWith2ChildrenReturnsHtmlTagWithoutAttributesWith2Children()
        {
            var div = HtmlTag.Parse("<div><a href='testhref'></a><img src='testsrc'/></div>");
            Assert.Equal("div", div.TagName);
            Assert.Equal(2, div.Children.Count());

            var a = div.Children.First();
            Assert.Equal("a", a.TagName);
            Assert.True(a.HasAttribute("href"));
            Assert.Equal("testhref", a["href"]);

            var img = div.Children.Last();
            Assert.Equal("img", img.TagName);
            Assert.True(img.HasAttribute("src"));
            Assert.Equal("testsrc", img["src"]);
        }

        [Fact]
        public void EmptyDivWith2AttributesReturnsHtmlTagWith2AttributesWithoutContent()
        {
            var div = HtmlTag.Parse("<div id='testid' name='testname'></div>");
            Assert.Equal("div", div.TagName);
            Assert.True(div.HasAttribute("id"));
            Assert.True(div.HasAttribute("name"));
            Assert.Equal("testid", div["id"]);
            Assert.Equal("testname", div["name"]);
            Assert.Equal(2, div.Attributes.Count);
            Assert.Empty(div.Contents ?? []);
        }

        [Fact]
        public void EmptyDivReturnsHtmlTagWithoutAttributesOrContent()
        {
            var div = HtmlTag.Parse("<div></div>");
            Assert.Equal("div", div.TagName);
            Assert.Empty(div.Contents ?? []);
            Assert.Empty(div.Children ?? []);
        }

        [Fact]
        public void WhenHtmlIsSelectWithTwoOptionsShouldContain2Children()
        {
            var select = HtmlTag.Parse(
                "<select "
                    + "data-val=\"true\" "
                    + "data-val-number=\"The field Tarief must be a number.\" "
                    + "data-val-required=\"Tarief is een vereist veld\" "
                    + "id=\"DriverDto_Tarrif_ID\" "
                    + "name=\"DriverDto.Tarrif_ID\">"
                    + "<option value=\"41\">test</option>"
                    + "<option value=\"42\">tweede</option>"
                    + "</select>"
            );
            Assert.Equal("true", select["data-val"]);
            Assert.Equal("The field Tarief must be a number.", select["data-val-number"]);
            Assert.Equal("Tarief is een vereist veld", select["data-val-required"]);
            Assert.Equal(2, select.Children.Count());
            var option41 = select.Children.ElementAt(0);
            Assert.Equal("41", option41["value"]);
            Assert.Single(option41.Contents.ToList() ?? []);
            Assert.True((option41.Contents[0]) is HtmlText);
            Assert.Equal("test", option41.Contents[0].ToString());
            var option42 = select.Children.ElementAt(1);
            Assert.Equal("42", option42["value"]);
            Assert.Single(option42.Contents.ToList() ?? []);
            Assert.True((option42.Contents[0]) is HtmlText);
            Assert.Equal("tweede", option42.Contents[0].ToString());
        }

        [Fact]
        public void WhenHtmlIsSingleDivShouldReturnSingleDiv()
        {
            var div = HtmlTag.Parse(
                @"<div>"
                    + "<input id=\"VehicleDto_Counter1Limit\" name=\"VehicleDto.Counter1Limit\" type=\"text\" value=\"0\" />"
                    + "<div class=\"row minuteSplitter\">"
                    + "<div class=\"col-md-6\">"
                    + "<div class=\"input-group\">"
                    + "<input type=\"number\" class=\"hours\" />"
                    + "<span class=\"input-group-addon\">h</span>"
                    + "</div>"
                    + "</div>"
                    + "<div class=\"col-md-6\">"
                    + "<div class=\"input-group\">"
                    + "<input type=\"number\" class=\"minutes\" />"
                    + "<span class=\"input-group-addon\">m</span>"
                    + "</div>"
                    + "</div>"
                    + "</div>"
                    + "</div>"
            );
            Assert.NotNull(div);
        }

        [Fact]
        public void WhenHtmlIsFreeTextShouldNotReEncode()
        {
            var input = new HtmlString("var pathToToc = \"/toc-placeholder.json\";");

            var parsed = HtmlTag.ParseAll(input).Single();

            Assert.True((parsed) is HtmlText);
            Assert.Equal("var pathToToc = \"/toc-placeholder.json\";", parsed.ToHtmlString());
        }
    }

    public class Prepend : HtmlTagTests
    {
        [Fact]
        public void HtmlElementOnHtmlTagWith1ChildShouldPrependHtmlElement()
        {
            var div = HtmlTag.Parse("<div><div id='child1'></div></div>");
            div = div.Prepend(HtmlTags.Div.Id("child2"));
            Assert.Equal(2, div.Children.Count());
            Assert.Equal("child2", div.Children.First()["id"]);
        }

        [Fact]
        public void HtmlElementOnHtmlTagWith1TextChildShouldPrependHtmlElement()
        {
            var div = HtmlTag.Parse("<label>This is a label</label>");
            div = div.Prepend(HtmlTags.I.Class("icon icon-label"));
            Assert.Single(div.Children); // text nodes don't count as a child, so the count should be 1
            Assert.Equal(2, div.Contents.Count);
            Assert.Equal("icon icon-label", div.Children.First()["class"]);
            Assert.Equal("This is a label", div.Contents[1].ToHtmlString());
        }

        [Fact]
        public void HtmlElementOnHtmlTagWithNoChildrenShouldAddHtmlElement()
        {
            var div = HtmlTags.Div;
            var child = HtmlTags.Div;
            div = div.Prepend(child);
            Assert.Single(div.Children);
            Assert.Equal(child, div.Children.Single());
        }

        [Fact]
        public void HtmlTextOnHtmlTagWith1ElementChildShouldPrependHtmlText()
        {
            var div = HtmlTag.Parse("<ul><li>This is the first item</li></ul>");
            div = div.Prepend("These are the items");
            Assert.Single(div.Children);
            Assert.Equal(2, div.Contents.Count);
            Assert.Equal(new HtmlText("These are the items"), div.Contents[0]);
            Assert.Equal(HtmlTag.Parse("<li>This is the first item</li>"), div.Contents[1]);
        }

        [Fact]
        public void ManyElementsOnHtmlTagWith1ElementChildShouldPrependHtmlText()
        {
            var div = HtmlTag.Parse("<ul><li>This is the first item</li></ul>");
            div = div.Prepend(
                new HtmlText("These are the items"),
                new HtmlText("So much prepending")
            );
            Assert.Single(div.Children);
            var contents = div.Contents.ToArray();
            Assert.Equal(3, contents.Length);
            Assert.Equal(new HtmlText("These are the items"), contents[0]);
            Assert.Equal(new HtmlText("So much prepending"), contents[1]);
            Assert.Equal(HtmlTag.Parse("<li>This is the first item</li>"), contents[2]);
        }

        [Fact]
        public void PrependingNullShouldReturnThis()
        {
            var label = HtmlTags.Label.Append("Bonjour");

            Assert.Same(label, label.Prepend((IEnumerable<IHtmlContent?>?)null));
            Assert.Same(label, label.Prepend((IEnumerable<IHtmlElement?>?)null));
            Assert.Same(label, label.Prepend((IHtmlContent?[]?)null));
            Assert.Same(label, label.Prepend((IHtmlElement?[]?)null));
            Assert.Same(label, label.Prepend((IHtmlContent?)null));
            Assert.Same(label, label.Prepend((IHtmlElement?)null));
        }

        [Fact]
        public void PrependingSomeNullsShouldIgnoreNulls()
        {
            var label = HtmlTags.Label;

            label = label.Prepend(
                HtmlTags.Span.Append("This is "),
                null,
                HtmlTags.Span.Append("Sparta")
            );

            Assert.Equal(
                "<label><span>This is </span><span>Sparta</span></label>",
                label.ToHtmlString()
            );
        }
    }

    public class RemoveClass : HtmlTagTests
    {
        [Fact]
        public void WhenElementDoesNotHaveClassAttributeShouldDoNothing() =>
            Assert.Null(Record.Exception(new Action(() => HtmlTags.Div.RemoveClass("test"))));

        [Fact]
        public void WhenElementDoesNotHaveThatClassShouldDoNothing() =>
            Assert.Null(
                Record.Exception(() => HtmlTags.Div.Class("some other classes").RemoveClass("test"))
            );

        [Fact]
        public void WhenElementHasOnlyThatClassShouldRemoveItAndRemoveAttribute()
        {
            var div = HtmlTags.Div.Class("test").RemoveClass("test");
            Assert.False(div.HasAttribute("class"));
            Assert.False(div.HasClass("test"));
        }

        [Fact]
        public void WhenElementHasThatClassButAlsoOthersShouldRemoveTheClassButKeepTheAttribute()
        {
            var div = HtmlTags.Div.Class("test othertest").RemoveClass("test");
            Assert.True(div.HasAttribute("class"));
            Assert.False(div.HasClass("test"));
            Assert.True(div.HasClass("othertest"));
        }
    }

    public class RemoveStyle : HtmlTagTests
    {
        [Fact]
        public void WhenElementDoesNotHaveSuchAStyleShouldDoNothing() =>
            Assert.Null(
                Record.Exception(() => HtmlTags.Div.Style("height", "15px").RemoveStyle("width"))
            );

        [Fact]
        public void WhenElementDoesntEvenHaveStyleAttributeShouldDoNothing() =>
            Assert.Null(Record.Exception(new Action(() => HtmlTags.Div.RemoveStyle("width"))));

        [Fact]
        public void WhenElementHasOnlyThatStyleShouldRemoveStyle()
        {
            var div = HtmlTags.Div.Style("width", "15px").RemoveStyle("width");
            Assert.False(div.Styles.ContainsKey("width"));
        }

        [Fact]
        public void WhenElementHasThatStyleAndOthersShouldRemoveStyle()
        {
            var div = HtmlTags
                .Div.Style("width", "15px")
                .Style("height", "15px")
                .RemoveStyle("width");
            Assert.False(div.Styles.ContainsKey("width"));
            Assert.True(div.Styles.ContainsKey("height"));
            Assert.Equal("15px", div.Styles["height"]);
        }
    }

    public class Style : HtmlTagTests
    {
        [Fact]
        public void AddingNewStyleToElementWithStyleAttributeShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Style("width", "10px").Style("height", "15px");
            Assert.True(div.HasAttribute("style"));
            Assert.Equal(2, div.Styles.Count);
            Assert.True(div.Styles.ContainsKey("width"));
            Assert.Equal("10px", div.Styles["width"]);
            Assert.True(div.Styles.ContainsKey("height"));
            Assert.Equal("15px", div.Styles["height"]);
        }

        [Fact]
        public void AddingNewStyleToElementWithoutStyleAttributeShouldAddStyle()
        {
            var div = HtmlTags.Div.Style("width", "10px");
            Assert.True(div.HasAttribute("style"));
            Assert.Single(div.Styles);
            Assert.True(div.Styles.ContainsKey("width"));
            Assert.Equal("10px", div.Styles["width"]);
        }

        [Fact]
        public void UpdatingStyleWithReplaceExistingFalseShouldNotUpdateStyle()
        {
            var div = HtmlTags.Div.Style("width", "10px").Style("width", "25px", false);
            Assert.True(div.HasAttribute("style"));
            Assert.Single(div.Styles);
            Assert.True(div.Styles.ContainsKey("width"));
            Assert.Equal("10px", div.Styles["width"]);
        }

        [Fact]
        public void UpdatingStyleWithReplaceExistingTrueShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Style("width", "10px").Style("width", "25px");
            Assert.True(div.HasAttribute("style"));
            Assert.Single(div.Styles);
            Assert.True(div.Styles.ContainsKey("width"));
            Assert.Equal("25px", div.Styles["width"]);
        }

        [Fact]
        public void WhenStyleIsIEFilterShouldNotCrashStyles()
        {
            var div = HtmlTags.Div.Style(
                "filter",
                "progid:DXImageTransform.Microsoft.gradient(startColorstr='#cccccc', endColorstr='#000000')"
            );
            Assert.True(div.HasAttribute("style"));
            Assert.Single(div.Styles);
            Assert.True(div.Styles.ContainsKey("filter"));
            Assert.Equal(
                "progid:DXImageTransform.Microsoft.gradient(startColorstr='#cccccc', endColorstr='#000000')",
                div.Styles["filter"]
            );
            Assert.Equal(
                "<div style=\"filter:progid:DXImageTransform.Microsoft.gradient(startColorstr=&#x27;#cccccc&#x27;, endColorstr=&#x27;#000000&#x27;);\"></div>",
                div.ToHtmlString()
            );
        }
    }

    public class ToHtml : HtmlTagTests
    {
        [Fact]
        public void DivWithThreeLevelsOfChildrenWithAttributesWithTagRenderModeNormalShouldRenderNormally()
        {
            var div = HtmlTag.Parse(
                "<div><ul><li class='active'><label data-url='/index'>This is the label &lgt;</label></li></ul></div>"
            );
            var html = div.ToHtmlString();
            var reparsedDiv = HtmlTag.Parse(html);
            Assert.True(div.Equals(reparsedDiv));
        }

        [Fact]
        public void DivWithThreeLevelsOfChildrenWithTagRenderModeNormalShouldRenderNormally()
        {
            var div = HtmlTag.Parse(
                "<div><ul><li><label>This is the label</label></li></ul></div>"
            );
            var html = div.ToHtmlString();
            var reparsedDiv = HtmlTag.Parse(html);
            Assert.True(div.Equals(reparsedDiv));
        }

        [Fact]
        public void DivWithThreeLevelsOfChildrenWithTagRenderModeSelfClosingTagShouldThrowInvalidOperationException()
        {
            var div = HtmlTag.Parse(
                "<div><ul><li><label>This is the label</label></li></ul></div>"
            );
            Assert.Throws<InvalidOperationException>(() =>
                div.Render(TagRenderMode.SelfClosing).ToHtmlString()
            );
        }

        [Fact]
        public void DivWithThreeLevelsOfChildrenWithTagrenderModeEndTagShouldCloseTagsOfDivOnly()
        {
            var div = HtmlTag.Parse(
                "<div><ul><li><label>This is the label</label></li></ul></div>"
            );
            Assert.Equal("</div>", div.Render(TagRenderMode.EndTag).ToHtmlString());
        }

        [Fact]
        public void DivWithThreeLevelsOfChildrenWithTagrenderModeStartTagShouldOpenTagsOfDivOnly()
        {
            var div = HtmlTag.Parse(
                "<div><ul><li><label>This is the label</label></li></ul></div>"
            );
            Assert.Equal("<div>", div.Render(TagRenderMode.StartTag).ToHtmlString());
        }

        [Fact]
        public void ImgShouldBeSelfclosingByDefault()
        {
            var img = HtmlTags.Img;
            Assert.Equal("<img />", img.ToHtmlString());
        }

        [Fact]
        public void InputAsChildWithRenderModeSelfClosingShouldRenderAsSelfClosing()
        {
            var div = HtmlTag.Parse("<div><input/></div>");
            var input = div.Children.Single();
            input.Render(TagRenderMode.SelfClosing);
            var html = div.ToHtmlString().Replace(" ", "");
            Assert.Equal("<div><input/></div>", html);
        }

        [Fact]
        public void InputWithRenderModeSelfClosingShouldRenderAsSelfClosing()
        {
            var input = HtmlTag.Parse("<input/>").Render(TagRenderMode.SelfClosing);
            var html = input.ToHtmlString().Replace(" ", "");
            Assert.Equal("<input/>", html);
        }
    }

    public class ToggleAttribute : HtmlTagTests
    {
        [Fact]
        public void WhenFalseAttributeShouldBeRemoved()
        {
            var input = new HtmlTag("input")
                .ToggleAttribute("disabled", true)
                .ToggleAttribute("disabled", false);
            Assert.False(input.HasAttribute("disabled"));
        }

        [Fact]
        public void WhenTrueAttributeShouldBeAdded()
        {
            var input = new HtmlTag("input").ToggleAttribute("disabled", true);
            Assert.True(input.HasAttribute("disabled"));
            Assert.Equal("disabled", input["disabled"]);
        }
    }
}
