using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HtmlBuilders {
  /// <summary>
  ///   Represents an html element
  /// </summary>
  public interface IHtmlElement {
    /// <summary>
    ///   Renders this <see cref="IHtmlElement" /> to a <see cref="HtmlString" />
    /// </summary>
    /// <param name="tagRenderMode">The mode with which to render this node</param>
    /// <returns>
    ///   A <see cref="HtmlString" /> representation of this <see cref="IHtmlElement" /> using the specified
    ///   <paramref name="tagRenderMode" />
    /// </returns>
    HtmlString ToHtml(TagRenderMode? tagRenderMode = null);

    /// <summary>
    ///   Renders this <see cref="IHtmlElement" /> to a <see cref="StringWriter" /> using the provided <see cref="HtmlEncoder"/>
    /// </summary>
    /// <param name="writer">The writer to which the HTML should be written</param>
    /// <param name="encoder">The encoding that should be applied when writing the HTML</param>
    /// <param name="tagRenderMode">The mode with which to render this node</param>
    void WriteTo(TextWriter writer, HtmlEncoder encoder, TagRenderMode? tagRenderMode = null);
  }
}