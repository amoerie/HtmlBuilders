using NUnit.Framework;

namespace HtmlBuilders.Tests
{
    [TestFixture]
    public class TestsForExtensionsForHtmlTag
    {
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
            div.Name("new name", replaceExisting: false);
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
            div.Title("new title", replaceExisting: false);
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
            div.Id("new id", replaceExisting: false);
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
            div.Type("new type", replaceExisting: false);
            Assert.That(div.HasAttribute("type"), Is.True);
            Assert.That(div["type"], Is.EqualTo("test type"));
        }
        #endregion

        #region Value
        [Test]
        public void Value_AddingNewAttribute_ShouldHaveNewAttribute()
        {
            var div = HtmlTags.Div.Value("test value");
            Assert.That(div.HasAttribute("value"), Is.True);
            Assert.That(div["value"], Is.EqualTo("test value"));
        }

        [Test]
        public void Value_UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue()
        {
            var div = HtmlTags.Div.Value("test value");
            div.Value("new value");
            Assert.That(div.HasAttribute("value"), Is.True);
            Assert.That(div["value"], Is.EqualTo("new value"));
        }

        [Test]
        public void Value_UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue()
        {
            var div = HtmlTags.Div.Value("test value");
            div.Value("new value", replaceExisting: false);
            Assert.That(div.HasAttribute("value"), Is.True);
            Assert.That(div["value"], Is.EqualTo("test value"));
        }
        #endregion

        #region Href
        [Test]
        public void Href_AddingNewAttribute_ShouldHaveNewAttribute()
        {
            var div = HtmlTags.Div.Href("test href");
            Assert.That(div.HasAttribute("href"), Is.True);
            Assert.That(div["href"], Is.EqualTo("test href"));
        }

        [Test]
        public void Href_UpdatingOldAttributeWithReplaceExistingTrue_ShouldHaveUpdatedAttributeValue()
        {
            var div = HtmlTags.Div.Href("test href");
            div.Href("new href");
            Assert.That(div.HasAttribute("href"), Is.True);
            Assert.That(div["href"], Is.EqualTo("new href"));
        }

        [Test]
        public void Href_UpdatingOldAttributeWithReplaceExistingFalse_ShouldStillHaveOldAttributeValue()
        {
            var div = HtmlTags.Div.Href("test href");
            div.Href("new href", replaceExisting: false);
            Assert.That(div.HasAttribute("href"), Is.True);
            Assert.That(div["href"], Is.EqualTo("test href"));
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
    }
}
