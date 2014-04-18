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
        public string Text { get; set; }
        
        /// <summary>
        ///     Initializes a new instance of <see cref="HtmlText"/>
        /// </summary>
        /// <param name="text">The text</param>
        public HtmlText(string text)
        {
            if(text == null)
                Text = string.Empty;
            Text = text;
        }

        public virtual HtmlTag Parent { get; set; }

        public virtual IHtmlString ToHtml(TagRenderMode? tagRenderMode = null)
        {
            return MvcHtmlString.Create(Text);
        }

        public override string ToString()
        {
            return Text;
        }

        private bool Equals(HtmlText other)
        {
            return string.Equals(Text, other.Text);
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
            return Text.GetHashCode();
        }
    }
}
