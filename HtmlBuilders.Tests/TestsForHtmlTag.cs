using System;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;

namespace HtmlBuilders.Tests
{
    [TestFixture]
    public class TestsForHtmlTag
    {
        #region Constructor

        [Test]
        public void Constructor_TestQuickInitializationWithTwoAttributes_ShouldAddTwoAttributes()
        {
            var div = new HtmlTag("div") {{"name", "div-name"}, {"id", "div-id"}};
            Assert.That(div.HasAttribute("name"));
            Assert.That(div.HasAttribute("id"));
            Assert.That(div["name"], Is.EqualTo("div-name"));
            Assert.That(div["id"], Is.EqualTo("div-id"));
        }

        #endregion

        #region Contents
        [Test]
        public void Contents_WhenSettingNewValue_ContentsShouldBeReplace()
        {
            var tag = HtmlTag.Parse("<div><span>This is the span</span></div>");
            Assert.That(tag.Contents.Count(), Is.EqualTo(1));
            Assert.That(tag.Contents.First(), Is.EqualTo(new HtmlTag("span").Append("this is the span")));
            tag.Contents = new[] { HtmlTag.Parse("<span>This is the new span!</span>") };
            Assert.That(tag.Contents.Count(), Is.EqualTo(1));
            Assert.That(tag.Contents.First(), Is.EqualTo(new HtmlTag("span").Append("this is the new span!")));
        }

        [Test]
        public void Contents_WhenSettingEmptyValue_ContentsShouldBeEmpty()
        {
            var tag = HtmlTag.Parse("<div><span>This is the span</span></div>");
            Assert.That(tag.Contents.Count(), Is.EqualTo(1));
            Assert.That(tag.Contents.First(), Is.EqualTo(HtmlTags.Span.Append("this is the span")));
            tag.Contents = Enumerable.Empty<HtmlTag>();
            Assert.That(tag.Contents.Count(), Is.EqualTo(0));
        } 
        #endregion

        #region Siblings
        [Test]
        public void Siblings_WhenThereAreThreeChildren_ShouldReturnTwoSiblings()
        {
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
        public void Siblings_WhenThereIsJustOneChild_ShouldReturnEmptyEnumerable()
        {
            var first = HtmlTags.Li.Id("first");
            var ul = new HtmlTag("ul").Append(first);
            var siblings = first.Siblings.ToArray();
            Assert.That(siblings.Length, Is.EqualTo(0));
        }

        [Test]
        public void Siblings_WhenThereIsNoParent_ShouldReturnEmptyEnumerable()
        {
            var first = HtmlTags.Li.Id("first");
            Assert.That(first.Siblings, Is.Empty);
        }
        #endregion

        [Test]
        public void Find_WhenThereAreNoChildren_ShouldReturnEmptyEnumerable()
        {
            Assert.That(HtmlTags.Li.Find(tag => tag.TagName == "li"), Is.Empty);
        }

        [Test]
        public void Find_WhenOnlyOneChildMatches_ShouldReturnThatChild()
        {
            var ul = HtmlTag.Parse("<ul><li class='active'>This is the first</li><li>This is the second</li></ul>");
            var active = ul.Find(tag => tag.HasClass("active")).Single();
            Assert.That(active, Is.EqualTo(HtmlTags.Li.Class("active").Append("This is the first")));
        }

        [Test]
        public void Find_WhenOneGrandchildMatches_ShouldReturnGrandChild()
        {
            var ul =
                HtmlTag.Parse(
                    "<ul><li class='active'><span id='first'>This is the first</span></li><li><label>This is the second</label></li></ul>");
            var first = ul.Find(tag => tag.HasAttribute("id") && tag["id"] == "first").Single();
            Assert.That(first, Is.EqualTo(new HtmlTag("span").Id("first").Append("This is the first")));
        }

        #region Parse
        [Test]
        public void Parse_EmptyDiv_ReturnsHtmlTagWithoutAttributesOrContent()
        {
            var div = HtmlTag.Parse("<div></div>");
            Assert.That(div.TagName, Is.EqualTo("div"));
            Assert.That(div.Contents, Is.Empty);
            Assert.That(div.Children, Is.Empty);
        }

        [Test]
        public void Parse_EmptyDivWith2Attributes_ReturnsHtmlTagWith2AttributesWithoutContent()
        {
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
        public void Parse_DivWithoutAttributesWith2Children_ReturnsHtmlTagWithoutAttributesWith2Children()
        {
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
        #endregion

        #region ParseAll
        [Test]
        public void ParseAll_WhenEmpty_ShouldReturnEmpty()
        {
            Assert.That(HtmlTag.ParseAll(string.Empty), Is.Empty);
        }

        [Test]
        public void ParseAll_WhenTwoElements_ShouldReturnTwoElements()
        {
            var tags = HtmlTag.ParseAll("<li>The first</li><li>The second</li>").ToArray();
            Assert.That(tags.Length, Is.EqualTo(2));
            Assert.That(tags[0], Is.EqualTo(HtmlTags.Li.Append("The first")));
            Assert.That(tags[1], Is.EqualTo(HtmlTags.Li.Append("The second")));
        } 
        #endregion

        #region Prepend
        [Test]
        public void Prepend_HtmlElementOnHtmlTagWithNoChildren_ShouldAddHtmlElement()
        {
            var div = HtmlTags.Div;
            var child = HtmlTags.Div;
            div.Prepend(child);
            Assert.That(div.Children.Count(), Is.EqualTo(1));
            Assert.That(div.Children.Single(), Is.EqualTo(child));
        }

        [Test]
        public void Prepend_HtmlElementOnHtmlTagWith1Child_ShouldPrependHtmlElement()
        {
            var div = HtmlTag.Parse("<div><div id='child1'></div></div>");
            div.Prepend(HtmlTags.Div.Id("child2"));
            Assert.That(div.Children.Count(), Is.EqualTo(2));
            Assert.That(div.Children.First()["id"], Is.EqualTo("child2"));
        }

        [Test]
        public void Prepend_HtmlElementOnHtmlTagWith1TextChild_ShouldPrependHtmlElement()
        {
            var div = HtmlTag.Parse("<label>This is a label</label>");
            div.Prepend(HtmlTags.I.Class("icon icon-label"));
            Assert.That(div.Children.Count(), Is.EqualTo(1)); // text nodes don't count as a child, so the count should be 1
            Assert.That(div.Contents.Count(), Is.EqualTo(2));
            Assert.That(div.Children.First()["class"], Is.EqualTo("icon icon-label"));
            Assert.That(div.Contents.Last().ToHtml().ToString(), Is.EqualTo("This is a label"));
        }

        [Test]
        public void Prepend_HtmlTextOnHtmlTagWith1ElementChild_ShouldPrependHtmlText()
        {
            var div = HtmlTag.Parse("<ul><li>This is the first item</li></ul>");
            div.Prepend("These are the items");
            Assert.That(div.Children.Count(), Is.EqualTo(1));
            Assert.That(div.Contents.Count(), Is.EqualTo(2));
            Assert.That(div.Contents.First(), Is.EqualTo(new HtmlText("These are the items")));
            Assert.That(div.Contents.Last(), Is.EqualTo(HtmlTag.Parse("<li>This is the first item</li>")));
        } 

        [Test]
        public void Prepend_ManyElementsOnHtmlTagWith1ElementChild_ShouldPrependHtmlText()
        {
            var div = HtmlTag.Parse("<ul><li>This is the first item</li></ul>");
            div.Prepend(new HtmlText("These are the items"), new HtmlText("So much prepending"));
            Assert.That(div.Children.Count(), Is.EqualTo(1));
            var contents = div.Contents.ToArray();
            Assert.That(contents.Length, Is.EqualTo(3));
            Assert.That(contents[0], Is.EqualTo(new HtmlText("These are the items")));
            Assert.That(contents[1], Is.EqualTo(new HtmlText("So much prepending")));
            Assert.That(contents[2], Is.EqualTo(HtmlTag.Parse("<li>This is the first item</li>")));
        } 
        #endregion

        #region Insert
        [Test]
        public void Insert_WhenIndexIsLargerThanContentsCount_ThrowsArgumentOutOfRangeException()
        {
            var div = HtmlTag.Parse("<ul><li>This is the first item</li><li>This is the second item</li></ul>");
            Assert.That(() => div.Insert(3, HtmlTags.Li.Append("This is the fourth item")), Throws.InstanceOf<IndexOutOfRangeException>());
        }

        [Test]
        public void Insert_WhenIndexIsLowerThanZero_ThrowsArgumentOutOfRangeException()
        {
            var div = HtmlTag.Parse("<ul><li>This is the first item</li><li>This is the second item</li></ul>");
            Assert.That(() => div.Insert(-1, HtmlTags.Li.Append("This is the minus first item")), Throws.InstanceOf<IndexOutOfRangeException>());
        }

        [Test]
        public void Insert_WhenIndexIsEqualToContentsCount_AddsTheElement()
        {
            var div = HtmlTag.Parse("<ul><li>This is the first item</li><li>This is the second item</li></ul>");
            Assert.That(div.Insert(2, HtmlTags.Li.Append("This is the third item")).Children.Count(), Is.EqualTo(3));
        } 

        [Test]
        public void Insert_WhenInsertingMultipleElements_ShouldRetainOrderOfAddedElements()
        {
            var div = HtmlTag.Parse("<ul><li>This is the first item</li><li>This is the fourth item</li></ul>");
            div.Insert(1, HtmlTag.Parse("<li>This is the second item</li>"), HtmlTag.Parse("<li>This is the third item</li>"));
            var children = div.Children.ToArray();
            Assert.That(children.Length, Is.EqualTo(4));
            Assert.That(children[0], Is.EqualTo(HtmlTag.Parse("<li>This is the first item</li>")));
            Assert.That(children[1], Is.EqualTo(HtmlTag.Parse("<li>This is the second item</li>")));
            Assert.That(children[2], Is.EqualTo(HtmlTag.Parse("<li>This is the third item</li>")));
            Assert.That(children[3], Is.EqualTo(HtmlTag.Parse("<li>This is the fourth item</li>")));
        } 
        #endregion

        #region Append
        [Test]
        public void Append_HtmlElementOnHtmlTagWithNoChildren_ShouldAddHtmlElement()
        {
            var div = HtmlTags.Div;
            var child = HtmlTags.Div;
            div.Append(child);
            Assert.That(div.Children.Count(), Is.EqualTo(1));
            Assert.That(div.Children.Single(), Is.EqualTo(child));
        }

        [Test]
        public void Append_HtmlElementOnHtmlTagWith1Child_ShouldAppendHtmlElement()
        {
            var div = HtmlTag.Parse("<div><div id='child1'></div></div>");
            div.Append(HtmlTags.Div.Id("child2"));
            Assert.That(div.Children.Count(), Is.EqualTo(2));
            Assert.That(div.Children.Last()["id"], Is.EqualTo("child2"));
        }

        [Test]
        public void Append_HtmlElementOnHtmlTagWith1TextChild_ShouldAppendHtmlElement()
        {
            var div = HtmlTag.Parse("<label>This is a label</label>");
            div.Append(HtmlTags.I.Class("icon icon-label"));
            Assert.That(div.Children.Count(), Is.EqualTo(1)); // text nodes don't count as a child, so the count should be 1
            Assert.That(div.Contents.Count(), Is.EqualTo(2));
            Assert.That(div.Children.Last()["class"], Is.EqualTo("icon icon-label"));
            Assert.That(div.Contents.First().ToHtml().ToString(), Is.EqualTo("This is a label"));
        }

        [Test]
        public void Append_HtmlTextOnHtmlTagWith1ElementChild_ShouldAppendHtmlText()
        {
            var div = HtmlTag.Parse("<ul><li>This is the first item</li></ul>");
            div.Append("These are the items");
            Assert.That(div.Children.Count(), Is.EqualTo(1));
            Assert.That(div.Contents.Count(), Is.EqualTo(2));
            Assert.That(div.Contents.Last().ToHtml().ToHtmlString(), Is.EqualTo("These are the items"));
            Assert.That(div.Contents.First().ToHtml().ToString(), Is.EqualTo("<li>This is the first item</li>"));
        } 

        [Test]
        public void Append_WhenAppendingMultipleElements_ShouldRetainOrderOfAddedElements()
        {
            var div = HtmlTag.Parse("<ul><li>This is the first item</li><li>This is the second item</li></ul>");
            div.Append(HtmlTag.Parse("<li>This is the third item</li>"), HtmlTag.Parse("<li>This is the fourth item</li>"));
            var children = div.Children.ToArray();
            Assert.That(children.Length, Is.EqualTo(4));
            Assert.That(children[0], Is.EqualTo(HtmlTag.Parse("<li>This is the first item</li>")));
            Assert.That(children[1], Is.EqualTo(HtmlTag.Parse("<li>This is the second item</li>")));
            Assert.That(children[2], Is.EqualTo(HtmlTag.Parse("<li>This is the third item</li>")));
            Assert.That(children[3], Is.EqualTo(HtmlTag.Parse("<li>This is the fourth item</li>")));
        } 
        #endregion

        #region Attribute
        [Test]
        public void Attribute_AddingNewAttribute_ShouldHaveNewAttribute()
        {
            var div = HtmlTags.Div.Attribute("id", "testid");
            Assert.That(div.HasAttribute("id"), Is.True);
            Assert.That(div["id"], Is.EqualTo("testid"));
        }

        [Test]
        public void Attribute_UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue()
        {
            var div = HtmlTags.Div.Attribute("id", "testid");
            div.Attribute("id", "newid");
            Assert.That(div.HasAttribute("id"), Is.True);
            Assert.That(div["id"], Is.EqualTo("newid"));
        }

        [Test]
        public void Attribute_UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue()
        {
            var div = HtmlTags.Div.Attribute("id", "testid");
            div.Attribute("id", "newid", replaceExisting: false);
            Assert.That(div.HasAttribute("id"), Is.True);
            Assert.That(div["id"], Is.EqualTo("testid"));
        } 
        #endregion        
        
        #region Name
        [Test]
        public void Name_AddingNewAttribute_ShouldHaveNewAttribute()
        {
            var div = HtmlTags.Div.Name("test name");
            Assert.That(div.HasAttribute("name"), Is.True);
            Assert.That(div["name"], Is.EqualTo("test name"));
        }

        [Test]
        public void Name_UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue()
        {
            var div = HtmlTags.Div.Name("test name");
            div.Name("new name");
            Assert.That(div.HasAttribute("name"), Is.True);
            Assert.That(div["name"], Is.EqualTo("new name"));
        }

        [Test]
        public void Name_UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue()
        {
            var div = HtmlTags.Div.Name("test name");
            div.Name("new name", replaceExisting:false);
            Assert.That(div.HasAttribute("name"), Is.True);
            Assert.That(div["name"], Is.EqualTo("test name"));
        } 
        #endregion
        
        #region Title
        [Test]
        public void Title_AddingNewAttribute_ShouldHaveNewAttribute()
        {
            var div = HtmlTags.Div.Title("test title");
            Assert.That(div.HasAttribute("title"), Is.True);
            Assert.That(div["title"], Is.EqualTo("test title"));
        }

        [Test]
        public void Title_UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue()
        {
            var div = HtmlTags.Div.Title("test title");
            div.Title("new title");
            Assert.That(div.HasAttribute("title"), Is.True);
            Assert.That(div["title"], Is.EqualTo("new title"));
        }

        [Test]
        public void Title_UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue()
        {
            var div = HtmlTags.Div.Title("test title");
            div.Title("new title", replaceExisting:false);
            Assert.That(div.HasAttribute("title"), Is.True);
            Assert.That(div["title"], Is.EqualTo("test title"));
        } 
        #endregion
        
        #region Id
        [Test]
        public void Id_AddingNewAttribute_ShouldHaveNewAttribute()
        {
            var div = HtmlTags.Div.Id("test id");
            Assert.That(div.HasAttribute("id"), Is.True);
            Assert.That(div["id"], Is.EqualTo("test id"));
        }

        [Test]
        public void Id_UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue()
        {
            var div = HtmlTags.Div.Id("test id");
            div.Id("new id");
            Assert.That(div.HasAttribute("id"), Is.True);
            Assert.That(div["id"], Is.EqualTo("new id"));
        }

        [Test]
        public void Id_UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue()
        {
            var div = HtmlTags.Div.Id("test id");
            div.Id("new id", replaceExisting:false);
            Assert.That(div.HasAttribute("id"), Is.True);
            Assert.That(div["id"], Is.EqualTo("test id"));
        } 
        #endregion

        #region Type
        [Test]
        public void Type_AddingNewAttribute_ShouldHaveNewAttribute()
        {
            var div = HtmlTags.Div.Type("test type");
            Assert.That(div.HasAttribute("type"), Is.True);
            Assert.That(div["type"], Is.EqualTo("test type"));
        }

        [Test]
        public void Type_UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue()
        {
            var div = HtmlTags.Div.Type("test type");
            div.Type("new type");
            Assert.That(div.HasAttribute("type"), Is.True);
            Assert.That(div["type"], Is.EqualTo("new type"));
        }

        [Test]
        public void Type_UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue()
        {
            var div = HtmlTags.Div.Type("test type");
            div.Type("new type", replaceExisting:false);
            Assert.That(div.HasAttribute("type"), Is.True);
            Assert.That(div["type"], Is.EqualTo("test type"));
        } 
        #endregion

        #region ToggleAttribute
        [Test]
        public void ToggleAttribute_WhenTrue_AttributeShouldBeAdded()
        {
            var input = new HtmlTag("input").ToggleAttribute("disabled", true);
            Assert.That(input.HasAttribute("disabled"), Is.True);
            Assert.That(input["disabled"], Is.EqualTo("disabled"));
        }

        [Test]
        public void ToggleAttribute_WhenFalse_AttributeShouldBeRemoved()
        {
            var input = new HtmlTag("input").ToggleAttribute("disabled", true).ToggleAttribute("disabled", false);
            Assert.That(input.HasAttribute("disabled"), Is.False);
        } 
        #endregion

        #region Checked
        [Test]
        public void Checked_WhenTrue_AttributeShouldBeAdded()
        {
            var input = new HtmlTag("input").Checked(true);
            Assert.That(input.HasAttribute("checked"), Is.True);
            Assert.That(input["checked"], Is.EqualTo("checked"));
        }

        [Test]
        public void Checked_WhenFalse_AttributeShouldBeRemoved()
        {
            var input = new HtmlTag("input").Checked(true).Checked(false);
            Assert.That(input.HasAttribute("checked"), Is.False);
        } 
        #endregion

        #region Disabled
        [Test]
        public void Disabled_WhenTrue_AttributeShouldBeAdded()
        {
            var input = new HtmlTag("input").Disabled(true);
            Assert.That(input.HasAttribute("disabled"), Is.True);
            Assert.That(input["disabled"], Is.EqualTo("disabled"));
        }

        [Test]
        public void Disabled_WhenFalse_AttributeShouldBeRemoved()
        {
            var input = new HtmlTag("input").Disabled(true).Disabled(false);
            Assert.That(input.HasAttribute("disabled"), Is.False);
        } 
        #endregion

        #region Selected
        [Test]
        public void Selected_WhenTrue_AttributeShouldBeAdded()
        {
            var input = new HtmlTag("input").Selected(true);
            Assert.That(input.HasAttribute("selected"), Is.True);
            Assert.That(input["selected"], Is.EqualTo("selected"));
        }

        [Test]
        public void Selected_WhenFalse_AttributeShouldBeRemoved()
        {
            var input = new HtmlTag("input").Selected(true).Selected(false);
            Assert.That(input.HasAttribute("selected"), Is.False);
        } 
        #endregion

        #region Data
        [Test]
        public void Data_WhenNewAttribute_NewDataAttributeShouldBeAdded()
        {
            var div = HtmlTags.Div.Data("test", "datatest");
            Assert.That(div.HasAttribute("data-test"), Is.True);
            Assert.That(div["data-test"], Is.EqualTo("datatest"));
        }

        [Test]
        public void Data_WhenExistingAttributeAndReplaceExistingIsTrue_ExistingAttributeValueShouldBeUpdated()
        {
            var div = HtmlTags.Div.Data("test", "datatest").Data("test", "new datatest");
            Assert.That(div.HasAttribute("data-test"), Is.True);
            Assert.That(div["data-test"], Is.EqualTo("new datatest"));
        }

        [Test]
        public void Data_WhenExistingAttributeAndReplaceExistingIsFalse_OldAttributeValueIsStillPresent()
        {
            var div = HtmlTags.Div.Data("test", "datatest").Data("test", "new datatest", replaceExisting: false);
            Assert.That(div.HasAttribute("data-test"), Is.True);
            Assert.That(div["data-test"], Is.EqualTo("datatest"));
        }

        [Test]
        public void Data_WhenAttributeIsAlreadyPrefixedWithData_AttributeShouldNotBePrefixedAgain()
        {
            var div = HtmlTags.Div.Data("data-test", "datatest");
            Assert.That(div.HasAttribute("data-test"), Is.True);
            Assert.That(div["data-test"], Is.EqualTo("datatest"));
        }

        [Test]
        public void Data_WhenAddingAnonymousObjectAndReplaceExistingIsTrue_AllAttributesOfObjectShouldBeAdded()
        {
            var div = HtmlTags.Div.Data(new { data_test = "data test", data_test2 = "data test 2", data_test3 = "data test 3" });
            Assert.That(div.HasAttribute("data-test"), Is.True);
            Assert.That(div["data-test"], Is.EqualTo("data test"));
            Assert.That(div.HasAttribute("data-test2"), Is.True);
            Assert.That(div["data-test2"], Is.EqualTo("data test 2"));
            Assert.That(div.HasAttribute("data-test3"), Is.True);
            Assert.That(div["data-test3"], Is.EqualTo("data test 3"));
        }

        [Test]
        public void Data_WhenAddingAnonymousObjectAndReplaceExistingIsFalse_OnlyNewAttributesAreAdded()
        {
            var div = HtmlTags.Div.Data("data-test", "data test").Data(new { data_test = "new data test", data_test2 = "data test 2", data_test3 = "data test 3" }, replaceExisting: false);
            Assert.That(div.HasAttribute("data-test"), Is.True);
            Assert.That(div["data-test"], Is.EqualTo("data test"));
            Assert.That(div.HasAttribute("data-test2"), Is.True);
            Assert.That(div["data-test2"], Is.EqualTo("data test 2"));
            Assert.That(div.HasAttribute("data-test3"), Is.True);
            Assert.That(div["data-test3"], Is.EqualTo("data test 3"));
        } 
        #endregion

        #region Style
        [Test]
        public void Style_AddingNewStyleToElementWithoutStyleAttribute_ShouldAddStyle()
        {
            var div = HtmlTags.Div.Style("width", "10px");
            Assert.That(div.HasAttribute("style"), Is.True);
            Assert.That(div.Styles.Count, Is.EqualTo(1));
            Assert.That(div.Styles.ContainsKey("width"), Is.True);
            Assert.That(div.Styles["width"], Is.EqualTo("10px"));
        }

        [Test]
        public void Style_AddingNewStyleToElementWithStyleAttribute_ShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Style("width", "10px").Style("height", "15px");
            Assert.That(div.HasAttribute("style"), Is.True);
            Assert.That(div.Styles.Count, Is.EqualTo(2));
            Assert.That(div.Styles.ContainsKey("width"), Is.True);
            Assert.That(div.Styles["width"], Is.EqualTo("10px"));
            Assert.That(div.Styles.ContainsKey("height"), Is.True);
            Assert.That(div.Styles["height"], Is.EqualTo("15px"));
        }

        [Test]
        public void Style_UpdatingStyleWithReplaceExistingTrue_ShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Style("width", "10px").Style("width", "25px");
            Assert.That(div.HasAttribute("style"), Is.True);
            Assert.That(div.Styles.Count, Is.EqualTo(1));
            Assert.That(div.Styles.ContainsKey("width"), Is.True);
            Assert.That(div.Styles["width"], Is.EqualTo("25px"));
        }

        [Test]
        public void Style_UpdatingStyleWithReplaceExistingFalse_ShouldNotUpdateStyle()
        {
            var div = HtmlTags.Div.Style("width", "10px").Style("width", "25px", replaceExisting: false);
            Assert.That(div.HasAttribute("style"), Is.True);
            Assert.That(div.Styles.Count, Is.EqualTo(1));
            Assert.That(div.Styles.ContainsKey("width"), Is.True);
            Assert.That(div.Styles["width"], Is.EqualTo("10px"));
        } 
        #endregion

        #region RemoveStyle
        [Test]
        public void RemoveStyle_WhenElementDoesntEvenHaveStyleAttribute_ShouldDoNothing()
        {
            Assert.DoesNotThrow(() => HtmlTags.Div.RemoveStyle("width"));
        }

        [Test]
        public void RemoveStyle_WhenElementDoesNotHaveSuchAStyle_ShouldDoNothing()
        {
            Assert.DoesNotThrow(() => HtmlTags.Div.Style("height", "15px").RemoveStyle("width"));
        }

        [Test]
        public void RemoveStyle_WhenElementHasOnlyThatStyle_ShouldRemoveStyle()
        {
            var div = HtmlTags.Div.Style("width", "15px").RemoveStyle("width");
            Assert.That(div.Styles.ContainsKey("width"), Is.False);
        }

        [Test]
        public void RemoveStyle_WhenElementHasThatStyleAndOthers_ShouldRemoveStyle()
        {
            var div = HtmlTags.Div.Style("width", "15px").Style("height", "15px").RemoveStyle("width");
            Assert.That(div.Styles.ContainsKey("width"), Is.False);
            Assert.That(div.Styles.ContainsKey("height"), Is.True);
            Assert.That(div.Styles["height"], Is.EqualTo("15px"));
        } 
        #endregion

        #region Width
        [Test]
        public void Width_AddingNewWidthToElementWithoutStyleAttribute_ShouldAddStyleAndWidth()
        {
            var div = HtmlTags.Div.Width("10px");
            Assert.That(div.HasAttribute("style"), Is.True);
            Assert.That(div.Styles.Count, Is.EqualTo(1));
            Assert.That(div.Styles.ContainsKey("width"), Is.True);
            Assert.That(div.Styles["width"], Is.EqualTo("10px"));
        }

        [Test]
        public void Width_AddingNewWidthToElementWithStyleAttribute_ShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Style("padding", "10px").Width("15px");
            Assert.That(div.HasAttribute("style"), Is.True);
            Assert.That(div.Styles.Count, Is.EqualTo(2));
            Assert.That(div.Styles.ContainsKey("width"), Is.True);
            Assert.That(div.Styles["padding"], Is.EqualTo("10px"));
            Assert.That(div.Styles.ContainsKey("width"), Is.True);
            Assert.That(div.Styles["width"], Is.EqualTo("15px"));
        }

        [Test]
        public void Width_UpdatingWidthWithReplaceExistingTrue_ShouldUpdateWidth()
        {
            var div = HtmlTags.Div.Width("10px").Width("25px");
            Assert.That(div.HasAttribute("style"), Is.True);
            Assert.That(div.Styles.Count, Is.EqualTo(1));
            Assert.That(div.Styles.ContainsKey("width"), Is.True);
            Assert.That(div.Styles["width"], Is.EqualTo("25px"));
        }

        [Test]
        public void Width_UpdatingWidthWithReplaceExistingFalse_ShouldNotUpdateWidth()
        {
            var div = HtmlTags.Div.Width("10px").Width("25px", false);
            Assert.That(div.HasAttribute("style"), Is.True);
            Assert.That(div.Styles.Count, Is.EqualTo(1));
            Assert.That(div.Styles.ContainsKey("width"), Is.True);
            Assert.That(div.Styles["width"], Is.EqualTo("10px"));
        }
        #endregion

        #region Height
        [Test]
        public void Height_AddingNewHeightToElementWithoutStyleAttribute_ShouldAddStyleAndHeight()
        {
            var div = HtmlTags.Div.Height("10px");
            Assert.That(div.HasAttribute("style"), Is.True);
            Assert.That(div.Styles.Count, Is.EqualTo(1));
            Assert.That(div.Styles.ContainsKey("height"), Is.True);
            Assert.That(div.Styles["height"], Is.EqualTo("10px"));
        }

        [Test]
        public void Height_AddingNewHeightToElementWithStyleAttribute_ShouldUpdateStyle()
        {
            var div = HtmlTags.Div.Style("padding", "10px").Height("15px");
            Assert.That(div.HasAttribute("style"), Is.True);
            Assert.That(div.Styles.Count, Is.EqualTo(2));
            Assert.That(div.Styles.ContainsKey("height"), Is.True);
            Assert.That(div.Styles["padding"], Is.EqualTo("10px"));
            Assert.That(div.Styles.ContainsKey("height"), Is.True);
            Assert.That(div.Styles["height"], Is.EqualTo("15px"));
        }

        [Test]
        public void Height_UpdatingHeightWithReplaceExistingTrue_ShouldUpdateHeight()
        {
            var div = HtmlTags.Div.Height("10px").Height("25px");
            Assert.That(div.HasAttribute("style"), Is.True);
            Assert.That(div.Styles.Count, Is.EqualTo(1));
            Assert.That(div.Styles.ContainsKey("height"), Is.True);
            Assert.That(div.Styles["height"], Is.EqualTo("25px"));
        }

        [Test]
        public void Height_UpdatingHeightWithReplaceExistingFalse_ShouldNotUpdateHeight()
        {
            var div = HtmlTags.Div.Height("10px").Height("25px", false);
            Assert.That(div.HasAttribute("style"), Is.True);
            Assert.That(div.Styles.Count, Is.EqualTo(1));
            Assert.That(div.Styles.ContainsKey("height"), Is.True);
            Assert.That(div.Styles["height"], Is.EqualTo("10px"));
        }
        #endregion

        #region Class
        [Test]
        public void Class_SettingClassToElementWithoutClassAttribute_ShouldAddClassAttributeAndValue()
        {
            var div = HtmlTags.Div.Class("new");
            Assert.That(div.HasAttribute("class"), Is.True);
            Assert.That(div.HasClass("new"), Is.True);
        }

        [Test]
        public void Class_SettingClassToElementWithClassAttribute_ShouldUpdateClassAttributeWithNewValue()
        {
            var div = HtmlTags.Div.Class("existing").Class("new");
            Assert.That(div.HasAttribute("class"), Is.True);
            Assert.That(div.HasClass("existing"), Is.True);
            Assert.That(div.HasClass("new"), Is.True);
        } 
        #endregion

        #region RemoveClass
        [Test]
        public void RemoveClass_WhenElementDoesNotHaveClassAttribute_ShouldDoNothing()
        {
            Assert.DoesNotThrow(() => HtmlTags.Div.RemoveClass("test"));
        }

        [Test]
        public void RemoveClass_WhenElementDoesNotHaveThatClass_ShouldDoNothing()
        {
            Assert.DoesNotThrow(() => HtmlTags.Div.Class("some other classes").RemoveClass("test"));
        }

        [Test]
        public void RemoveClass_WhenElementHasOnlyThatClass_ShouldRemoveItAndRemoveAttribute()
        {
            var div = HtmlTags.Div.Class("test").RemoveClass("test");
            Assert.That(div.HasAttribute("class"), Is.False);
            Assert.That(div.HasClass("test"), Is.False);
        }

        [Test]
        public void RemoveClass_WhenElementHasThatClassButAlsoOthers_ShouldRemoveTheClassButKeepTheAttribute()
        {
            var div = HtmlTags.Div.Class("test othertest").RemoveClass("test");
            Assert.That(div.HasAttribute("class"), Is.True);
            Assert.That(div.HasClass("test"), Is.False);
            Assert.That(div.HasClass("othertest"), Is.True);
        } 
        #endregion

        #region ToHtml
        [Test]
        public void ToHtml_DivWithThreeLevelsOfChildrenWithTagrenderModeStartTag_ShouldOpenTagsOfDivOnly()
        {
            var div = HtmlTag.Parse("<div><ul><li><label>This is the label</label></li></ul></div>");
            Assert.That(div.ToHtml(TagRenderMode.StartTag).ToHtmlString(), Is.EqualTo("<div>"));
        }

        [Test]
        public void ToHtml_DivWithThreeLevelsOfChildrenWithTagrenderModeEndTag_ShouldCloseTagsOfDivOnly()
        {
            var div = HtmlTag.Parse("<div><ul><li><label>This is the label</label></li></ul></div>");
            Assert.That(div.ToHtml(TagRenderMode.EndTag).ToHtmlString(), Is.EqualTo("</div>"));
        }

        [Test]
        public void ToHtml_DivWithThreeLevelsOfChildrenWithTagRenderModeSelfClosingTag_ShouldThrowInvalidOperationException()
        {
            var div = HtmlTag.Parse("<div><ul><li><label>This is the label</label></li></ul></div>");
            Assert.That(() => div.ToHtml(TagRenderMode.SelfClosing).ToHtmlString(), Throws.InvalidOperationException);
        }

        [Test]
        public void ToHtml_DivWithThreeLevelsOfChildrenWithTagRenderModeNormal_ShouldRenderNormally()
        {
            var div = HtmlTag.Parse("<div><ul><li><label>This is the label</label></li></ul></div>");
            var html = div.ToHtml().ToHtmlString();
            var reparsedDiv = HtmlTag.Parse(html);
            Assert.That(div.Equals(reparsedDiv), Is.True);
        }

        [Test]
        public void ToHtml_DivWithThreeLevelsOfChildrenWithAttributesWithTagRenderModeNormal_ShouldRenderNormally()
        {
            var div = HtmlTag.Parse("<div><ul><li class='active'><label data-url='/index'>This is the label &lgt;</label></li></ul></div>");
            var html = div.ToHtml().ToHtmlString();
            var reparsedDiv = HtmlTag.Parse(html);
            Assert.That(div.Equals(reparsedDiv), Is.True);
        } 
        #endregion

        #region Equals
        [Test]
        public void Equals_TwoEmptyHtmlTagsWithSameTagName_ShouldBeEqual()
        {
            var tag1 = HtmlTags.Div;
            var tag2 = HtmlTags.Div;
            Assert.That(tag1.Equals(tag2), Is.True);
        }

        [Test]
        public void Equals_TwoEmptyHtmlTagsWithDifferentTagNames_ShouldNotBeEqual()
        {
            var tag1 = new HtmlTag("span");
            var tag2 = HtmlTags.Div;
            Assert.That(tag1.Equals(tag2), Is.False);
        }

        [Test]
        public void Equals_TwoHtmlTagsWithSameAttributes_ShouldBeEqual()
        {
            var tag1 = new HtmlTag("span").Name("test");
            var tag2 = new HtmlTag("span").Name("test");
            Assert.That(tag1.Equals(tag2), Is.True);
        }

        [Test]
        public void Equals_TwoHtmlTagsWithSameStyles_ShouldBeEqual()
        {
            var tag1 = new HtmlTag("span").Width("10px").Height("15px");
            var tag2 = new HtmlTag("span").Height("15px").Width("10px");
            Assert.That(tag1.Equals(tag2), Is.True);
        }

        [Test]
        public void Equals_TwoHtmlTagsWithDifferentStyles_ShouldNotBeEqual()
        {
            var tag1 = new HtmlTag("span").Width("9px").Height("15px");
            var tag2 = new HtmlTag("span").Height("15px").Width("10px");
            Assert.That(tag1.Equals(tag2), Is.False);
        }

        [Test]
        public void Equals_TwoHtmlTagsWithDifferentAmountOfStyles_ShouldNotBeEqual()
        {
            var tag1 = new HtmlTag("span").Width("10px").Height("15px");
            var tag2 = new HtmlTag("span").Height("15px").Width("10px").Style("padding", "5px");
            Assert.That(tag1.Equals(tag2), Is.False);
        }

        [Test]
        public void Equals_TwoHtmlTagsWithSameClasses_ShouldBeEqual()
        {
            var tag1 = new HtmlTag("span").Class("class1 class2");
            var tag2 = new HtmlTag("span").Class("class1 class2");
            Assert.That(tag1.Equals(tag2), Is.True);
        }

        [Test]
        public void Equals_TwoHtmlTagsWithDifferentClasses_ShouldNotBeEqual()
        {
            var tag1 = new HtmlTag("span").Class("class1 class2");
            var tag2 = new HtmlTag("span").Class("class1 class3");
            Assert.That(tag1.Equals(tag2), Is.False);
        }

        [Test]
        public void Equals_TwoHtmlTagsWithDifferentAmountClasses_ShouldNotBeEqual()
        {
            var tag1 = new HtmlTag("span").Class("class1 class2");
            var tag2 = new HtmlTag("span").Class("class1 class2 class3");
            Assert.That(tag1.Equals(tag2), Is.False);
        }

        [Test]
        public void Equals_TwoHtmlTagsWithSameChildren_ShouldBeEqual()
        {
            var tag1 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
            var tag2 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
            Assert.That(tag1.Equals(tag2), Is.True);
        }

        [Test]
        public void Equals_TwoHtmlTagsWithDifferentChildren_ShouldNotBeEqual()
        {
            var tag1 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
            var tag2 = HtmlTag.Parse("<div><ul><li>This is some other text<li></ul></div>");
            Assert.That(tag1.Equals(tag2), Is.False);
        }

        [Test]
        public void Equals_TwoHtmlTagsWithSameChildrenButWithDifferentAttributes_ShouldNotBeEqual()
        {
            var tag1 = HtmlTag.Parse("<div><ul><li class='active'>This is some text<li></ul></div>");
            var tag2 = HtmlTag.Parse("<div><ul><li>This is some text<li></ul></div>");
            Assert.That(tag1.Equals(tag2), Is.False);
        } 
        #endregion
    }
}
