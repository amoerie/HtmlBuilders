using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HtmlBuilders;

/// <summary>
///     Represents a text node that has an optional parent and some text
/// </summary>
public sealed class HtmlText : IHtmlElement
{
    private readonly IHtmlContent _content;

    /// <summary>
    ///     Initializes a new instance of <see cref="HtmlText" />
    /// </summary>
    /// <param name="text">The text that still needs to be encoded</param>
    public HtmlText(string? text) => _content = new StringHtmlContent(text ?? string.Empty);

    /// <summary>
    ///     Initializes a new instance of <see cref="HtmlText" />
    /// </summary>
    /// <param name="htmlString">The already encoded HTML string</param>
    public HtmlText(HtmlString? htmlString) => _content = htmlString ?? new HtmlString(string.Empty);

    /// <summary>
    ///     Initializes a new instance of <see cref="HtmlText" />
    /// </summary>
    /// <param name="stringHtmlContent">The string HTML content that still needs to be encoded</param>
    public HtmlText(StringHtmlContent? stringHtmlContent) => _content = stringHtmlContent ?? new StringHtmlContent(string.Empty);

    /// <inheritdoc />
    public void WriteTo(TextWriter writer, HtmlEncoder encoder) => _content.WriteTo(writer, encoder);

    /// <summary>
    ///     Exposes the inner <see cref="IHtmlContent" />, if you must.
    /// </summary>
    /// <returns>A reference to the inner <see cref="IHtmlContent" /> of this HTML text</returns>
    public IHtmlContent ToHtml() => _content;

    /// <inheritdoc />
    public override string ToString()
    {
        using var writer = new StringWriter();
        WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }

    private bool Equals(HtmlText other) => string.Equals(ToString(), other.ToString());

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((HtmlText)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode() => ToString().GetHashCode();
}
