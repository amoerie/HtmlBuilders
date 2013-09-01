using System;
using System.Web;
using System.Web.Mvc;

namespace HtmlBuilders
{
    /// <summary>
    ///     Represents a text node that has an optional parent and some text
    /// </summary>
    public class HtmlText: IHtmlElement
    {
        /// <summary>
        ///     The inner text
        /// </summary>
        private readonly string _text;
        
        /// <summary>
        ///     Initializes a new instance of <see cref="HtmlText"/>
        /// </summary>
        /// <param name="text">The text</param>
        public HtmlText(string text)
        {
            if(text == null)
                throw new ArgumentNullException("text");
            _text = text;
        }

        public HtmlTag Parent { get; set; }

        public IHtmlString ToHtml(TagRenderMode tagRenderMode = TagRenderMode.Normal)
        {
            return MvcHtmlString.Create(_text);
        }

        public override string ToString()
        {
            return _text;
        }

        private bool Equals(HtmlText other)
        {
            return string.Equals(_text, other._text);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((HtmlText) obj);
        }

        public override int GetHashCode()
        {
            return _text.GetHashCode();
        }
    }
}
