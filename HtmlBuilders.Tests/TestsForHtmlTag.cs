using System;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;

namespace HtmlBuilders.Tests {
  [TestFixture]
  public class TestsForHtmlTag {
    [Test]
    public void Append_HtmlElementOnHtmlTagWith1Child_ShouldAppendHtmlElement() {
      var div = HtmlTag.Parse("<div><div id='child1'></div></div>");
      div.Append(HtmlTags.Div.Id("child2"));
      Assert.That(div.Children.Count(), Is.EqualTo(2));
      Assert.That(div.Children.Last()["id"], Is.EqualTo("child2"));
    }

    [Test]
    public void Append_HtmlElementOnHtmlTagWith1TextChild_ShouldAppendHtmlElement() {
      var div = HtmlTag.Parse("<label>This is a label</label>");
      div.Append(HtmlTags.I.Class("icon icon-label"));
      Assert.That(div.Children.Count(), Is.EqualTo(1)); // text nodes don't count as a child, so the count should be 1
      Assert.That(div.Contents.Count(), Is.EqualTo(2));
      Assert.That(div.Children.Last()["class"], Is.EqualTo("icon icon-label"));
      Assert.That(div.Contents.First().ToHtml().ToString(), Is.EqualTo("This is a label"));
    }

    [Test]
    public void Append_HtmlElementOnHtmlTagWithNoChildren_ShouldAddHtmlElement() {
      var div = HtmlTags.Div;
      var child = HtmlTags.Div;
      div.Append(child);
      Assert.That(div.Children.Count(), Is.EqualTo(1));
      Assert.That(div.Children.Single(), Is.EqualTo(child));
    }

    [Test]
    public void Append_HtmlTextOnHtmlTagWith1ElementChild_ShouldAppendHtmlText() {
      var ul = HtmlTag.Parse("<ul><li>This is the first item</li></ul>");
      ul.Append("These are the items");
      var s = ul.ToHtml();
      Assert.That(ul.Children.Count(), Is.EqualTo(1));
      Assert.That(ul.Contents.Count(), Is.EqualTo(2));
      Assert.That(ul.Contents.Last().ToHtml().ToHtmlString(), Is.EqualTo("These are the items"));
      Assert.That(ul.Contents.First().ToHtml().ToString(), Is.EqualTo("<li>This is the first item</li>"));
    }

    [Test]
    public void Append_WhenAppendingMultipleElements_ShouldRetainOrderOfAddedElements() {
      var div = HtmlTag.Parse("<ul><li>This is the first item</li><li>This is the second item</li></ul>");
      div.Append(HtmlTag.Parse("<li>This is the third item</li>"), HtmlTag.Parse("<li>This is the fourth item</li>"));
      var children = div.Children.ToArray();
      Assert.That(children.Length, Is.EqualTo(4));
      Assert.That(children[0], Is.EqualTo(HtmlTag.Parse("<li>This is the first item</li>")));
      Assert.That(children[1], Is.EqualTo(HtmlTag.Parse("<li>This is the second item</li>")));
      Assert.That(children[2], Is.EqualTo(HtmlTag.Parse("<li>This is the third item</li>")));
      Assert.That(children[3], Is.EqualTo(HtmlTag.Parse("<li>This is the fourth item</li>")));
    }

    [Test]
    public void Attribute_AddingNewAttribute_ShouldHaveNewAttribute() {
      var div = HtmlTags.Div.Attribute("id", "testid");
      Assert.That(div.HasAttribute("id"), Is.True);
      Assert.That(div["id"], Is.EqualTo("testid"));
    }

    [Test]
    public void Attribute_UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue() {
      var div = HtmlTags.Div.Attribute("id", "testid");
      div.Attribute("id", "newid", false);
      Assert.That(div.HasAttribute("id"), Is.True);
      Assert.That(div["id"], Is.EqualTo("testid"));
    }

    [Test]
    public void Attribute_UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue() {
      var div = HtmlTags.Div.Attribute("id", "testid");
      div.Attribute("id", "newid");
      Assert.That(div.HasAttribute("id"), Is.True);
      Assert.That(div["id"], Is.EqualTo("newid"));
    }

    [Test]
    public void Class_SettingClassToElementWithClassAttribute_ShouldUpdateClassAttributeWithNewValue() {
      var div = HtmlTags.Div.Class("existing").Class("new");
      Assert.That(div.HasAttribute("class"), Is.True);
      Assert.That(div.HasClass("existing"), Is.True);
      Assert.That(div.HasClass("new"), Is.True);
    }

    [Test]
    public void Class_SettingClassToElementWithoutClassAttribute_ShouldAddClassAttributeAndValue() {
      var div = HtmlTags.Div.Class("new");
      Assert.That(div.HasAttribute("class"), Is.True);
      Assert.That(div.HasClass("new"), Is.True);
    }

    [Test]
    public void Constructor_TestQuickInitializationWithTwoAttributes_ShouldAddTwoAttributes() {
      var div = new HtmlTag("div") {{"name", "div-name"}, {"id", "div-id"}};
      Assert.That(div.HasAttribute("name"));
      Assert.That(div.HasAttribute("id"));
      Assert.That(div["name"], Is.EqualTo("div-name"));
      Assert.That(div["id"], Is.EqualTo("div-id"));
    }

    [Test]
    public void Contents_WhenSettingEmptyValue_ContentsShouldBeEmpty() {
      var tag = HtmlTag.Parse("<div><span>This is the span</span></div>");
      Assert.That(tag.Contents.Count(), Is.EqualTo(1));
      Assert.That(tag.Contents.First(), Is.EqualTo(HtmlTags.Span.Append("this is the span")));
      tag.Contents = Enumerable.Empty<HtmlTag>();
      Assert.That(tag.Contents.Count(), Is.EqualTo(0));
    }

    [Test]
    public void Contents_WhenSettingNewValue_ContentsShouldBeReplace() {
      var tag = HtmlTag.Parse("<div><span>This is the span</span></div>");
      Assert.That(tag.Contents.Count(), Is.EqualTo(1));
      Assert.That(tag.Contents.First(), Is.EqualTo(new HtmlTag("span").Append("this is the span")));
      tag.Contents = new[] {HtmlTag.Parse("<span>This is the new span!</span>")};
      Assert.That(tag.Contents.Count(), Is.EqualTo(1));
      Assert.That(tag.Contents.First(), Is.EqualTo(new HtmlTag("span").Append("this is the new span!")));
    }

    [Test]
    public void Data_WhenAddingAnonymousObjectAndReplaceExistingIsFalse_OnlyNewAttributesAreAdded() {
      var div =
        HtmlTags.Div.Data("data-test", "data test")
          .Data(new {data_test = "new data test", data_test2 = "data test 2", data_test3 = "data test 3"}, false);
      Assert.That(div.HasAttribute("data-test"), Is.True);
      Assert.That(div["data-test"], Is.EqualTo("data test"));
      Assert.That(div.HasAttribute("data-test2"), Is.True);
      Assert.That(div["data-test2"], Is.EqualTo("data test 2"));
      Assert.That(div.HasAttribute("data-test3"), Is.True);
      Assert.That(div["data-test3"], Is.EqualTo("data test 3"));
    }

    [Test]
    public void Data_WhenAddingAnonymousObjectAndReplaceExistingIsTrue_AllAttributesOfObjectShouldBeAdded() {
      var div =
        HtmlTags.Div.Data(new {data_test = "data test", data_test2 = "data test 2", data_test3 = "data test 3"});
      Assert.That(div.HasAttribute("data-test"), Is.True);
      Assert.That(div["data-test"], Is.EqualTo("data test"));
      Assert.That(div.HasAttribute("data-test2"), Is.True);
      Assert.That(div["data-test2"], Is.EqualTo("data test 2"));
      Assert.That(div.HasAttribute("data-test3"), Is.True);
      Assert.That(div["data-test3"], Is.EqualTo("data test 3"));
    }

    [Test]
    public void Data_WhenAttributeIsAlreadyPrefixedWithData_AttributeShouldNotBePrefixedAgain() {
      var div = HtmlTags.Div.Data("data-test", "datatest");
      Assert.That(div.HasAttribute("data-test"), Is.True);
      Assert.That(div["data-test"], Is.EqualTo("datatest"));
    }

    [Test]
    public void Data_WhenExistingAttributeAndReplaceExistingIsFalse_OldAttributeValueIsStillPresent() {
      var div = HtmlTags.Div.Data("test", "datatest").Data("test", "new datatest", false);
      Assert.That(div.HasAttribute("data-test"), Is.True);
      Assert.That(div["data-test"], Is.EqualTo("datatest"));
    }

    [Test]
    public void Data_WhenExistingAttributeAndReplaceExistingIsTrue_ExistingAttributeValueShouldBeUpdated() {
      var div = HtmlTags.Div.Data("test", "datatest").Data("test", "new datatest");
      Assert.That(div.HasAttribute("data-test"), Is.True);
      Assert.That(div["data-test"], Is.EqualTo("new datatest"));
    }

    [Test]
    public void Data_WhenNewAttribute_NewDataAttributeShouldBeAdded() {
      var div = HtmlTags.Div.Data("test", "datatest");
      Assert.That(div.HasAttribute("data-test"), Is.True);
      Assert.That(div["data-test"], Is.EqualTo("datatest"));
    }

    [Test]
    public void Equals_TwoEmptyHtmlTagsWithDifferentTagNames_ShouldNotBeEqual() {
      var tag1 = HtmlTags.Span;
      var tag2 = HtmlTags.Div;
      Assert.That(tag1.Equals(tag2), Is.False);
    }

    [Test]
    public void Equals_TwoEmptyHtmlTagsWithSameTagName_ShouldBeEqual() {
      var tag1 = HtmlTags.Div;
      var tag2 = HtmlTags.Div;
      Assert.That(tag1.Equals(tag2), Is.True);
    }

    [Test]
    public void Equals_TwoHtmlTagsWithDifferentAmountClasses_ShouldNotBeEqual() {
      var tag1 = new HtmlTag("span").Class("class1 class2");
      var tag2 = new HtmlTag("span").Class("class1 class2 class3");
      Assert.That(tag1.Equals(tag2), Is.False);
    }

    [Test]
    public void Equals_TwoHtmlTagsWithDifferentAmountOfStyles_ShouldNotBeEqual() {
      var tag1 = new HtmlTag("span").Width("10px").Height("15px");
      var tag2 = new HtmlTag("span").Height("15px").Width("10px").Style("padding", "5px");
      Assert.That(tag1.Equals(tag2), Is.False);
    }

    [Test]
    public void Equals_TwoHtmlTagsWithDifferentChildren_ShouldNotBeEqual() {
      var tag1 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
      var tag2 = HtmlTag.Parse("<div><ul><li>This is some other text<li></ul></div>");
      Assert.That(tag1.Equals(tag2), Is.False);
    }

    [Test]
    public void Equals_TwoHtmlTagsWithDifferentClasses_ShouldNotBeEqual() {
      var tag1 = new HtmlTag("span").Class("class1 class2");
      var tag2 = new HtmlTag("span").Class("class1 class3");
      Assert.That(tag1.Equals(tag2), Is.False);
    }

    [Test]
    public void Equals_TwoHtmlTagsWithDifferentStyles_ShouldNotBeEqual() {
      var tag1 = new HtmlTag("span").Width("9px").Height("15px");
      var tag2 = new HtmlTag("span").Height("15px").Width("10px");
      Assert.That(tag1.Equals(tag2), Is.False);
    }

    [Test]
    public void Equals_TwoHtmlTagsWithSameAttributes_ShouldBeEqual() {
      var tag1 = HtmlTags.Span.Name("test");
      var tag2 = HtmlTags.Span.Name("test");
      Assert.That(tag1.Equals(tag2), Is.True);
    }

    [Test]
    public void Equals_TwoHtmlTagsWithSameChildrenButWithDifferentAttributes_ShouldNotBeEqual() {
      var tag1 = HtmlTag.Parse("<div><ul><li class='active'>This is some text<li></ul></div>");
      var tag2 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
      Assert.That(tag1.Equals(tag2), Is.False);
    }

    [Test]
    public void Equals_TwoHtmlTagsWithSameChildren_ShouldBeEqual() {
      var tag1 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
      var tag2 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
      Assert.That(tag1.Equals(tag2), Is.True);
    }

    [Test]
    public void Equals_TwoHtmlTagsWithSameClasses_ShouldBeEqual() {
      var tag1 = new HtmlTag("span").Class("class1 class2");
      var tag2 = new HtmlTag("span").Class("class1 class2");
      Assert.That(tag1.Equals(tag2), Is.True);
    }

    [Test]
    public void Equals_TwoHtmlTagsWithSameStyles_ShouldBeEqual() {
      var tag1 = new HtmlTag("span").Width("10px").Height("15px");
      var tag2 = new HtmlTag("span").Height("15px").Width("10px");
      Assert.That(tag1.Equals(tag2), Is.True);
    }

    [Test]
    public void Find_WhenOneGrandchildMatches_ShouldReturnGrandChild() {
      var ul =
        HtmlTag.Parse(
          "<ul><li class='active'><span id='first'>This is the first</span></li><li><label>This is the second</label></li></ul>");
      var first = ul.Find(tag => tag.HasAttribute("id") && tag["id"] == "first").Single();
      Assert.That(first, Is.EqualTo(new HtmlTag("span").Id("first").Append("This is the first")));
    }

    [Test]
    public void Find_WhenOnlyOneChildMatches_ShouldReturnThatChild() {
      var ul = HtmlTag.Parse("<ul><li class='active'>This is the first</li><li>This is the second</li></ul>");
      var active = ul.Find(tag => tag.HasClass("active")).Single();
      Assert.That(active, Is.EqualTo(HtmlTags.Li.Class("active").Append("This is the first")));
    }

    [Test]
    public void Find_WhenThereAreNoChildren_ShouldReturnEmptyEnumerable() {
      Assert.That(HtmlTags.Li.Find(tag => tag.TagName == "li"), Is.Empty);
    }

    [Test]
    public void Insert_WhenIndexIsEqualToContentsCount_AddsTheElement() {
      var div = HtmlTag.Parse("<ul><li>This is the first item</li><li>This is the second item</li></ul>");
      Assert.That(div.Insert(2, HtmlTags.Li.Append("This is the third item")).Children.Count(), Is.EqualTo(3));
    }

    [Test]
    public void Insert_WhenIndexIsLargerThanContentsCount_ThrowsArgumentOutOfRangeException() {
      var div = HtmlTag.Parse("<ul><li>This is the first item</li><li>This is the second item</li></ul>");
      Assert.That(() => div.Insert(3, HtmlTags.Li.Append("This is the fourth item")),
        Throws.InstanceOf<IndexOutOfRangeException>());
    }

    [Test]
    public void Insert_WhenIndexIsLowerThanZero_ThrowsArgumentOutOfRangeException() {
      var div = HtmlTag.Parse("<ul><li>This is the first item</li><li>This is the second item</li></ul>");
      Assert.That(() => div.Insert(-1, HtmlTags.Li.Append("This is the minus first item")),
        Throws.InstanceOf<IndexOutOfRangeException>());
    }

    [Test]
    public void Insert_WhenInsertingMultipleElements_ShouldRetainOrderOfAddedElements() {
      var div = HtmlTag.Parse("<ul><li>This is the first item</li><li>This is the fourth item</li></ul>");
      div.Insert(1, HtmlTag.Parse("<li>This is the second item</li>"), HtmlTag.Parse("<li>This is the third item</li>"));
      var children = div.Children.ToArray();
      Assert.That(children.Length, Is.EqualTo(4));
      Assert.That(children[0], Is.EqualTo(HtmlTag.Parse("<li>This is the first item</li>")));
      Assert.That(children[1], Is.EqualTo(HtmlTag.Parse("<li>This is the second item</li>")));
      Assert.That(children[2], Is.EqualTo(HtmlTag.Parse("<li>This is the third item</li>")));
      Assert.That(children[3], Is.EqualTo(HtmlTag.Parse("<li>This is the fourth item</li>")));
    }

    [Test]
    public void ParseAll_WhenEmpty_ShouldReturnEmpty() {
      Assert.That(HtmlTag.ParseAll(string.Empty), Is.Empty);
    }

    [Test]
    public void ParseAll_WhenHtmlIsSelectWithTwoOptions_ShouldContain2Children() {
      var tags = HtmlTag.ParseAll("<select " +
                                  "data-val=\"true\" " +
                                  "data-val-number=\"The field Tarief must be a number.\" " +
                                  "data-val-required=\"Tarief is een vereist veld\" " +
                                  "id=\"DriverDto_Tarrif_ID\" " +
                                  "name=\"DriverDto.Tarrif_ID\">" +
                                  "<option value=\"41\">test</option>" +
                                  "<option value=\"42\">tweede</option>" +
                                  "</select>").ToList();
      Assert.That(tags, Has.Count.EqualTo(1));
      var select = tags.Single();
      Assert.That(select["data-val"], Is.EqualTo("true"));
      Assert.That(select["data-val-number"], Is.EqualTo("The field Tarief must be a number."));
      Assert.That(select["data-val-required"], Is.EqualTo("Tarief is een vereist veld"));
      Assert.That(select.Children.ToList(), Has.Count.EqualTo(2));
      var option41 = select.Children.ElementAt(0);
      Assert.That(option41["value"], Is.EqualTo("41"));
      Assert.That(option41.Contents.ToList(), Has.Count.EqualTo(1));
      Assert.That(option41.Contents.First(), Is.TypeOf<HtmlText>());
      Assert.That(option41.Contents.First().ToString(), Is.EqualTo("test"));
      var option42 = select.Children.ElementAt(1);
      Assert.That(option42["value"], Is.EqualTo("42"));
      Assert.That(option42.Contents.ToList(), Has.Count.EqualTo(1));
      Assert.That(option42.Contents.First(), Is.TypeOf<HtmlText>());
      Assert.That(option42.Contents.First().ToString(), Is.EqualTo("tweede"));
    }

    [Test]
    public void ParseAll_WhenOneElement_ShouldReturnOneElement() {
      var tags = HtmlTag.ParseAll("<select class='test'>" +
                                  "<option>Select</option>" +
                                  "<option value='0'>1</option>" +
                                  "<option value='1'>2</option>" +
                                  "<option value='2'>3</option>" +
                                  "</select>");
      Assert.That(tags.Count(), Is.EqualTo(1));
      var tag = tags.Single();
      Assert.That(tag.TagName, Is.EqualTo("select"));
      Assert.That(tag.Children.Count(), Is.EqualTo(4));
    }

    [Test]
    public void ParseAll_WhenTwoElements_ShouldReturnTwoElements() {
      var tags = HtmlTag.ParseAll("<li>The first</li><li>The second</li>").ToArray();
      Assert.That(tags.Length, Is.EqualTo(2));
      Assert.That(tags[0], Is.EqualTo(HtmlTags.Li.Append("The first")));
      Assert.That(tags[1], Is.EqualTo(HtmlTags.Li.Append("The second")));
    }

    [Test]
    public void Parse_DivWithoutAttributesWith2Children_ReturnsHtmlTagWithoutAttributesWith2Children() {
      var div = HtmlTag.Parse("<div><a href='testhref'></a><img src='testsrc'/></div>");
      Assert.That(div.TagName, Is.EqualTo("div"));
      Assert.That(div.Children.Count(), Is.EqualTo(2));

      var a = div.Children.First();
      Assert.That(a.TagName, Is.EqualTo("a"));
      Assert.That(a.HasAttribute("href"), Is.True);
      Assert.That(a["href"], Is.EqualTo("testhref"));

      var img = div.Children.Last();
      Assert.That(img.TagName, Is.EqualTo("img"));
      Assert.That(img.HasAttribute("src"), Is.True);
      Assert.That(img["src"], Is.EqualTo("testsrc"));
    }

    [Test]
    public void Parse_EmptyDivWith2Attributes_ReturnsHtmlTagWith2AttributesWithoutContent() {
      var div = HtmlTag.Parse("<div id='testid' name='testname'></div>");
      Assert.That(div.TagName, Is.EqualTo("div"));
      Assert.That(div.HasAttribute("id"), Is.True);
      Assert.That(div.HasAttribute("name"), Is.True);
      Assert.That(div["id"], Is.EqualTo("testid"));
      Assert.That(div["name"], Is.EqualTo("testname"));
      Assert.That(div.Count, Is.EqualTo(2));
      Assert.That(div.Contents, Is.Empty);
    }

    [Test]
    public void Parse_EmptyDiv_ReturnsHtmlTagWithoutAttributesOrContent() {
      var div = HtmlTag.Parse("<div></div>");
      Assert.That(div.TagName, Is.EqualTo("div"));
      Assert.That(div.Contents, Is.Empty);
      Assert.That(div.Children, Is.Empty);
    }

    [Test]
    public void Parse_WhenHtmlIsSelectWithTwoOptions_ShouldContain2Children() {
      var select = HtmlTag.Parse("<select " +
                                 "data-val=\"true\" " +
                                 "data-val-number=\"The field Tarief must be a number.\" " +
                                 "data-val-required=\"Tarief is een vereist veld\" " +
                                 "id=\"DriverDto_Tarrif_ID\" " +
                                 "name=\"DriverDto.Tarrif_ID\">" +
                                 "<option value=\"41\">test</option>" +
                                 "<option value=\"42\">tweede</option>" +
                                 "</select>");
      Assert.That(select["data-val"], Is.EqualTo("true"));
      Assert.That(select["data-val-number"], Is.EqualTo("The field Tarief must be a number."));
      Assert.That(select["data-val-required"], Is.EqualTo("Tarief is een vereist veld"));
      Assert.That(select.Children.ToList(), Has.Count.EqualTo(2));
      var option41 = select.Children.ElementAt(0);
      Assert.That(option41["value"], Is.EqualTo("41"));
      Assert.That(option41.Contents.ToList(), Has.Count.EqualTo(1));
      Assert.That(option41.Contents.First(), Is.TypeOf<HtmlText>());
      Assert.That(option41.Contents.First().ToString(), Is.EqualTo("test"));
      var option42 = select.Children.ElementAt(1);
      Assert.That(option42["value"], Is.EqualTo("42"));
      Assert.That(option42.Contents.ToList(), Has.Count.EqualTo(1));
      Assert.That(option42.Contents.First(), Is.TypeOf<HtmlText>());
      Assert.That(option42.Contents.First().ToString(), Is.EqualTo("tweede"));
    }

    [Test]
    public void Style_WhenStyleIsIEFilter_ShouldNotCrashStyles() {
      var div = HtmlTags.Div.Style("filter",
        "progid:DXImageTransform.Microsoft.gradient(startColorstr='#cccccc', endColorstr='#000000')");
      Assert.That(div.HasAttribute("style"), Is.True);
      Assert.That(div.Styles.Count, Is.EqualTo(1));
      Assert.That(div.Styles.ContainsKey("filter"), Is.True);
      Assert.That(div.Styles["filter"],
        Is.EqualTo("progid:DXImageTransform.Microsoft.gradient(startColorstr='#cccccc', endColorstr='#000000')"));
      var toHtml = div.ToHtml().ToHtmlString();
      Assert.That(toHtml,
        Is.EqualTo("<div style=\"filter:progid:DXImageTransform.Microsoft.gradient(startColorstr=&#39;#cccccc&#39;, endColorstr=&#39;#000000&#39;)\"></div>"));
    }

    [Test]
    public void Parse_WhenHtmlIsSingleDiv_ShouldReturnSingleDiv() {
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
      Assert.That(div, Is.Not.Null);
    }

    [Test]
    public void Prepend_HtmlElementOnHtmlTagWith1Child_ShouldPrependHtmlElement() {
      var div = HtmlTag.Parse("<div><div id='child1'></div></div>");
      div.Prepend(HtmlTags.Div.Id("child2"));
      Assert.That(div.Children.Count(), Is.EqualTo(2));
      Assert.That(div.Children.First()["id"], Is.EqualTo("child2"));
    }

    [Test]
    public void Prepend_HtmlElementOnHtmlTagWith1TextChild_ShouldPrependHtmlElement() {
      var div = HtmlTag.Parse("<label>This is a label</label>");
      div.Prepend(HtmlTags.I.Class("icon icon-label"));
      Assert.That(div.Children.Count(), Is.EqualTo(1)); // text nodes don't count as a child, so the count should be 1
      Assert.That(div.Contents.Count(), Is.EqualTo(2));
      Assert.That(div.Children.First()["class"], Is.EqualTo("icon icon-label"));
      Assert.That(div.Contents.Last().ToHtml().ToString(), Is.EqualTo("This is a label"));
    }

    [Test]
    public void Prepend_HtmlElementOnHtmlTagWithNoChildren_ShouldAddHtmlElement() {
      var div = HtmlTags.Div;
      var child = HtmlTags.Div;
      div.Prepend(child);
      Assert.That(div.Children.Count(), Is.EqualTo(1));
      Assert.That(div.Children.Single(), Is.EqualTo(child));
    }

    [Test]
    public void Prepend_HtmlTextOnHtmlTagWith1ElementChild_ShouldPrependHtmlText() {
      var div = HtmlTag.Parse("<ul><li>This is the first item</li></ul>");
      div.Prepend("These are the items");
      Assert.That(div.Children.Count(), Is.EqualTo(1));
      Assert.That(div.Contents.Count(), Is.EqualTo(2));
      Assert.That(div.Contents.First(), Is.EqualTo(new HtmlText("These are the items")));
      Assert.That(div.Contents.Last(), Is.EqualTo(HtmlTag.Parse("<li>This is the first item</li>")));
    }

    [Test]
    public void Prepend_ManyElementsOnHtmlTagWith1ElementChild_ShouldPrependHtmlText() {
      var div = HtmlTag.Parse("<ul><li>This is the first item</li></ul>");
      div.Prepend(new HtmlText("These are the items"), new HtmlText("So much prepending"));
      Assert.That(div.Children.Count(), Is.EqualTo(1));
      var contents = div.Contents.ToArray();
      Assert.That(contents.Length, Is.EqualTo(3));
      Assert.That(contents[0], Is.EqualTo(new HtmlText("These are the items")));
      Assert.That(contents[1], Is.EqualTo(new HtmlText("So much prepending")));
      Assert.That(contents[2], Is.EqualTo(HtmlTag.Parse("<li>This is the first item</li>")));
    }

    [Test]
    public void RemoveClass_WhenElementDoesNotHaveClassAttribute_ShouldDoNothing() {
      Assert.DoesNotThrow(() => HtmlTags.Div.RemoveClass("test"));
    }

    [Test]
    public void RemoveClass_WhenElementDoesNotHaveThatClass_ShouldDoNothing() {
      Assert.DoesNotThrow(() => HtmlTags.Div.Class("some other classes").RemoveClass("test"));
    }

    [Test]
    public void RemoveClass_WhenElementHasOnlyThatClass_ShouldRemoveItAndRemoveAttribute() {
      var div = HtmlTags.Div.Class("test").RemoveClass("test");
      Assert.That(div.HasAttribute("class"), Is.False);
      Assert.That(div.HasClass("test"), Is.False);
    }

    [Test]
    public void RemoveClass_WhenElementHasThatClassButAlsoOthers_ShouldRemoveTheClassButKeepTheAttribute() {
      var div = HtmlTags.Div.Class("test othertest").RemoveClass("test");
      Assert.That(div.HasAttribute("class"), Is.True);
      Assert.That(div.HasClass("test"), Is.False);
      Assert.That(div.HasClass("othertest"), Is.True);
    }

    [Test]
    public void RemoveStyle_WhenElementDoesNotHaveSuchAStyle_ShouldDoNothing() {
      Assert.DoesNotThrow(() => HtmlTags.Div.Style("height", "15px").RemoveStyle("width"));
    }

    [Test]
    public void RemoveStyle_WhenElementDoesntEvenHaveStyleAttribute_ShouldDoNothing() {
      Assert.DoesNotThrow(() => HtmlTags.Div.RemoveStyle("width"));
    }

    [Test]
    public void RemoveStyle_WhenElementHasOnlyThatStyle_ShouldRemoveStyle() {
      var div = HtmlTags.Div.Style("width", "15px").RemoveStyle("width");
      Assert.That(div.Styles.ContainsKey("width"), Is.False);
    }

    [Test]
    public void RemoveStyle_WhenElementHasThatStyleAndOthers_ShouldRemoveStyle() {
      var div = HtmlTags.Div.Style("width", "15px").Style("height", "15px").RemoveStyle("width");
      Assert.That(div.Styles.ContainsKey("width"), Is.False);
      Assert.That(div.Styles.ContainsKey("height"), Is.True);
      Assert.That(div.Styles["height"], Is.EqualTo("15px"));
    }

    [Test]
    public void Siblings_WhenThereAreThreeChildren_ShouldReturnTwoSiblings() {
      var first = HtmlTags.Li.Id("first");
      var second = HtmlTags.Li.Id("second");
      var third = HtmlTags.Li.Id("third");
      var ul = new HtmlTag("ul").Append(first).Append(second).Append(third);
      var siblings = second.Siblings.ToArray();
      Assert.That(siblings.Length, Is.EqualTo(2));
      Assert.That(siblings[0], Is.EqualTo(first));
      Assert.That(siblings[1], Is.EqualTo(third));
    }

    [Test]
    public void Siblings_WhenThereIsJustOneChild_ShouldReturnEmptyEnumerable() {
      var first = HtmlTags.Li.Id("first");
      var ul = new HtmlTag("ul").Append(first);
      var siblings = first.Siblings.ToArray();
      Assert.That(siblings.Length, Is.EqualTo(0));
    }

    [Test]
    public void Siblings_WhenThereIsNoParent_ShouldReturnEmptyEnumerable() {
      var first = HtmlTags.Li.Id("first");
      Assert.That(first.Siblings, Is.Empty);
    }

    [Test]
    public void Style_AddingNewStyleToElementWithStyleAttribute_ShouldUpdateStyle() {
      var div = HtmlTags.Div.Style("width", "10px").Style("height", "15px");
      Assert.That(div.HasAttribute("style"), Is.True);
      Assert.That(div.Styles.Count, Is.EqualTo(2));
      Assert.That(div.Styles.ContainsKey("width"), Is.True);
      Assert.That(div.Styles["width"], Is.EqualTo("10px"));
      Assert.That(div.Styles.ContainsKey("height"), Is.True);
      Assert.That(div.Styles["height"], Is.EqualTo("15px"));
    }

    [Test]
    public void Style_AddingNewStyleToElementWithoutStyleAttribute_ShouldAddStyle() {
      var div = HtmlTags.Div.Style("width", "10px");
      Assert.That(div.HasAttribute("style"), Is.True);
      Assert.That(div.Styles.Count, Is.EqualTo(1));
      Assert.That(div.Styles.ContainsKey("width"), Is.True);
      Assert.That(div.Styles["width"], Is.EqualTo("10px"));
    }

    [Test]
    public void Style_UpdatingStyleWithReplaceExistingFalse_ShouldNotUpdateStyle() {
      var div = HtmlTags.Div.Style("width", "10px").Style("width", "25px", false);
      Assert.That(div.HasAttribute("style"), Is.True);
      Assert.That(div.Styles.Count, Is.EqualTo(1));
      Assert.That(div.Styles.ContainsKey("width"), Is.True);
      Assert.That(div.Styles["width"], Is.EqualTo("10px"));
    }

    [Test]
    public void Style_UpdatingStyleWithReplaceExistingTrue_ShouldUpdateStyle() {
      var div = HtmlTags.Div.Style("width", "10px").Style("width", "25px");
      Assert.That(div.HasAttribute("style"), Is.True);
      Assert.That(div.Styles.Count, Is.EqualTo(1));
      Assert.That(div.Styles.ContainsKey("width"), Is.True);
      Assert.That(div.Styles["width"], Is.EqualTo("25px"));
    }

    [Test]
    public void ToHtml_DivWithThreeLevelsOfChildrenWithAttributesWithTagRenderModeNormal_ShouldRenderNormally() {
      var div =
        HtmlTag.Parse(
          "<div><ul><li class='active'><label data-url='/index'>This is the label &lgt;</label></li></ul></div>");
      var html = div.ToHtml().ToHtmlString();
      var reparsedDiv = HtmlTag.Parse(html);
      Assert.That(div.Equals(reparsedDiv), Is.True);
    }

    [Test]
    public void ToHtml_DivWithThreeLevelsOfChildrenWithTagRenderModeNormal_ShouldRenderNormally() {
      var div = HtmlTag.Parse("<div><ul><li><label>This is the label</label></li></ul></div>");
      var html = div.ToHtml().ToHtmlString();
      var reparsedDiv = HtmlTag.Parse(html);
      Assert.That(div.Equals(reparsedDiv), Is.True);
    }

    [Test]
    public void ToHtml_DivWithThreeLevelsOfChildrenWithTagRenderModeSelfClosingTag_ShouldThrowInvalidOperationException() {
      var div = HtmlTag.Parse("<div><ul><li><label>This is the label</label></li></ul></div>");
      Assert.That(() => div.ToHtml(TagRenderMode.SelfClosing).ToHtmlString(), Throws.InvalidOperationException);
    }

    [Test]
    public void ToHtml_DivWithThreeLevelsOfChildrenWithTagrenderModeEndTag_ShouldCloseTagsOfDivOnly() {
      var div = HtmlTag.Parse("<div><ul><li><label>This is the label</label></li></ul></div>");
      Assert.That(div.ToHtml(TagRenderMode.EndTag).ToHtmlString(), Is.EqualTo("</div>"));
    }

    [Test]
    public void ToHtml_DivWithThreeLevelsOfChildrenWithTagrenderModeStartTag_ShouldOpenTagsOfDivOnly() {
      var div = HtmlTag.Parse("<div><ul><li><label>This is the label</label></li></ul></div>");
      Assert.That(div.ToHtml(TagRenderMode.StartTag).ToHtmlString(), Is.EqualTo("<div>"));
    }

    [Test]
    public void ToHtml_ImgShouldBeSelfclosingByDefault() {
      var img = HtmlTags.Img;
      Assert.That(img.ToHtml().ToHtmlString(), Is.EqualTo("<img />"));
    }

    [Test]
    public void ToHtml_InputAsChildWithRenderModeSelfClosing_ShouldRenderAsSelfClosing() {
      var div = HtmlTag.Parse("<div><input/></div>");
      var input = div.Children.Single();
      input.Render(TagRenderMode.SelfClosing);
      var html = div.ToHtml().ToHtmlString().Replace(" ", "");
      Assert.That(html, Is.EqualTo("<div><input/></div>"));
    }

    [Test]
    public void ToHtml_InputWithRenderModeSelfClosing_ShouldRenderAsSelfClosing() {
      var input = HtmlTag.Parse("<input/>").Render(TagRenderMode.SelfClosing);
      var html = input.ToHtml().ToHtmlString().Replace(" ", "");
      Assert.That(html, Is.EqualTo("<input/>"));
    }

    [Test]
    public void ToggleAttribute_WhenFalse_AttributeShouldBeRemoved() {
      var input = new HtmlTag("input").ToggleAttribute("disabled", true).ToggleAttribute("disabled", false);
      Assert.That(input.HasAttribute("disabled"), Is.False);
    }

    [Test]
    public void ToggleAttribute_WhenTrue_AttributeShouldBeAdded() {
      var input = new HtmlTag("input").ToggleAttribute("disabled", true);
      Assert.That(input.HasAttribute("disabled"), Is.True);
      Assert.That(input["disabled"], Is.EqualTo("disabled"));
    }

    [Test]
    public void Merge_AddingASecondClass_ShouldMergeCorrectly() {
      var div = HtmlTags.Div.Class("class1");
      div.Merge(new {@class = "class2"});
      Assert.That(div.HasClass("class1"));
      Assert.That(div.HasClass("class2"));
    }
  }
}