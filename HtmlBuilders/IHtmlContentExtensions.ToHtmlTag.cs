using System;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

// ReSharper disable InconsistentNaming

namespace HtmlBuilders;

/// <summary>
///     Contains utility methods to convert <see cref="IHtmlContent" /> into other things, such as an <see cref="HtmlTag" />.
/// </summary>
public static class IHtmlContentExtensions
{
    /// <summary>
    ///     Renders this html content to an HTML string using the default HTML encoder
    /// </summary>
    /// <param name="htmlContent">The HTML content to render</param>
    /// <returns>A string representation of the HTML content</returns>
    public static string ToHtmlString(this IHtmlContent htmlContent)
    {
        using var writer = new StringWriter();
        htmlContent.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }

    /// <summary>
    ///     Parses the provided HTML content and tries to extract an HTML tag from it.
    /// </summary>
    /// <param name="htmlContent"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static HtmlTag ToHtmlTag(this IHtmlContent htmlContent)
    {
        var htmlTags = HtmlTag.ParseAll(htmlContent).OfType<HtmlTag>().ToList();
        return htmlTags.Count switch
        {
            > 1 => throw new ArgumentException($"Multiple tags parsed from html: {Environment.NewLine}" +
                                               $"{string.Join(Environment.NewLine, htmlTags.Select(t => t.ToString()))}"),
            0 => throw new ArgumentException("No HTML tags found in html"),
            _ => htmlTags[0]
        };
    }
}
