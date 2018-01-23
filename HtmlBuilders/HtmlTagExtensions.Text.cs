using System.Collections.Generic;

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
      return string.Join(string.Empty, TextContents(htmlTag));
    }

    private static IEnumerable<string> TextContents(this HtmlTag htmlTag) {
      foreach (var content in htmlTag.Contents) {
        if (content is HtmlText) {
          var text = content as HtmlText;
          if (!string.IsNullOrEmpty(text.Text)) {
            yield return text.Text;
          }
        }
        else if (content is HtmlTag) {
          var tag = content as HtmlTag;
          yield return tag.Text();
        }
      }
    }
  }
}