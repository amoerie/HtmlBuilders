using System.Web;
using System.Web.Mvc;

namespace HtmlBuilders
{
    /// <summary>
    ///     Represents an html element
    /// </summary>
    public abstract class HtmlElement
    {
        /// <summary>
        ///      Gets or sets the parent of this element 
        ///  </summary>
        public abstract HtmlTag Parent { get; internal set; }

        /// <summary>
        ///     Renders this <see cref="HtmlElement"/> to an <see cref="IHtmlString"/>
        /// </summary>
        /// <param name="tagRenderMode">The mode with which to render this node</param>
        /// <returns>An <see cref="IHtmlString"/> representation of this <see cref="HtmlElement"/> using the specified <paramref name="tagRenderMode"/></returns>
        public abstract IHtmlString ToHtml(TagRenderMode tagRenderMode = TagRenderMode.Normal);
    }
}
