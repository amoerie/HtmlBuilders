using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NUnit.Framework;

namespace HtmlBuilders.Tests
{
    [TestFixture]
    public class TestsForHtmlText
    {
        [Test]
        public void ToString_WhenTextIsAbc_ShouldBeAbc()
        {
            Assert.That(new HtmlText("abc").ToString(), Is.EqualTo("abc"));
        }

        [Test]
        public void Equals_WhenTextsAreEqual_ShouldBeTrue()
        {
            Assert.That(new HtmlText("abc"), Is.EqualTo(new HtmlText("abc")));
        }

        [Test]
        public void Equals_WhenTextsAreNotEqual_ShouldBeFalse()
        {
            Assert.That(new HtmlText("abc"), Is.Not.EqualTo(new HtmlText("cba")));
        }

        [Test]
        public void ToHtml_WhenTextIsAbc_ShouldAlwaysReturnAbc()
        {
            var htmlText = new HtmlText("abc");
            Assert.That(htmlText.ToHtml(TagRenderMode.EndTag).ToString(), Is.EqualTo("abc"));
            Assert.That(htmlText.ToHtml(TagRenderMode.StartTag).ToString(), Is.EqualTo("abc"));
            Assert.That(htmlText.ToHtml(TagRenderMode.SelfClosing).ToString(), Is.EqualTo("abc"));
            Assert.That(htmlText.ToHtml(TagRenderMode.Normal).ToString(), Is.EqualTo("abc"));
            Assert.That(htmlText.ToHtml().ToString(), Is.EqualTo("abc"));
        }
    }
}
