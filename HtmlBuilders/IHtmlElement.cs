using System.Web;
using System.Web.Mvc;

namespace HtmlBuilders
{
    /// <summary>
    ///     Represents an html element
    /// </summary>
    public interface IHtmlElement
    {
        /// <summary>
        ///      Gets the parent of this element 
        ///  </summary>
        HtmlTag Parent { get; set; }

        /// <summary>
        ///     Renders this <see cref="IHtmlElement"/> to an <see cref="IHtmlString"/>
        /// </summary>
        /// <param name="tagRenderMode">The mode with which to render this node</param>
        /// <returns>An <see cref="IHtmlString"/> representation of this <see cref="IHtmlElement"/> using the specified <paramref name="tagRenderMode"/></returns>
        IHtmlString ToHtml(TagRenderMode tagRenderMode = TagRenderMode.Normal);
    }
}
