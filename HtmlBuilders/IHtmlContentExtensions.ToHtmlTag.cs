using System;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

// ReSharper disable InconsistentNaming

namespace HtmlBuilders {
  /// <summary>
  /// Contains useful methods that begin from <see cref="IHtmlContent"/>.
  /// </summary>
  public static class IHtmlContentExtensions {
    /// <summary>
    /// Renders this <paramref name="htmlContent"/> to an HTML encoded string
    /// </summary>
    /// <param name="htmlContent">The HTML content that should be stringified</param>
    /// <returns>An HTML encoded string</returns>
    public static string ToHtmlString(this IHtmlContent htmlContent) {
      using (var writer = new StringWriter()) {
        htmlContent.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
      }
    }

    /// <summary>
    /// Parses the provided <paramref name="htmlContent"/> to an HtmlTag instance
    /// </summary>
    /// <param name="htmlContent">The HTML content that should be parsed</param>
    /// <returns>An instance of HtmlTag if any valid HTML is found, or null when the input did not contain an HTML element.</returns>
    /// <exception cref="ArgumentException">When multiple HTML elements are found in the <paramref name="htmlContent"/></exception>
    public static HtmlTag ToHtmlTag(this IHtmlContent htmlContent) {
      var htmlTags = HtmlTag.ParseAll(htmlContent).OfType<HtmlTag>().ToList();
      if (htmlTags.Count > 1)
        throw new ArgumentException($"Multiple tags parsed from html: {Environment.NewLine}" +
                                    $"{string.Join(Environment.NewLine, htmlTags.Select(t => t.ToString()))}");
      return htmlTags.SingleOrDefault();
    }
  }
}