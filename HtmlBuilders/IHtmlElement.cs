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
    ///   Renders this <see cref="IHtmlElement" /> to <see cref="IHtmlContent" /> using the default HTML encoder
    /// </summary>
    /// <returns>
    ///   An <see cref="IHtmlContent" /> representation of this <see cref="IHtmlElement" />
    /// </returns>
    IHtmlContent ToHtml();

    /// <summary>
    ///   Renders this <see cref="IHtmlElement" /> to a <see cref="StringWriter" /> using the provided <see cref="HtmlEncoder"/>
    /// </summary>
    /// <param name="writer">The writer to which the HTML should be written</param>
    /// <param name="encoder">The encoding that should be applied when writing the HTML</param>
    void WriteTo(TextWriter writer, HtmlEncoder encoder);
  }
}