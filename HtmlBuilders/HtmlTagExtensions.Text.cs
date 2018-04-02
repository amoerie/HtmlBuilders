using System.IO;
using System.Text.Encodings.Web;

namespace HtmlBuilders {
  public static partial class HtmlTagExtensions {
    /// <summary>
    ///   Gets the HTML stripped content of this <see cref="HtmlTag"/>
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
    /// <returns>The HTML stripped contents.</returns>
    public static string Text(this HtmlTag htmlTag) {
      if (htmlTag == null)
        return string.Empty;
      using (var writer = new StringWriter()) {
        Write(htmlTag, writer, HtmlEncoder.Default);
        return writer.ToString();
      }
    }

    private static void Write(HtmlTag htmlTag, TextWriter writer, HtmlEncoder encoder) {
      foreach (var content in htmlTag.Contents) {
        if (content is HtmlText text) {
          text.WriteTo(writer, encoder);
        }
        else if (content is HtmlTag tag) {
          Write(tag, writer, encoder);
        }
      }
    }
  }
}