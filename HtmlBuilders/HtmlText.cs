using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HtmlBuilders {
  /// <summary>
  ///   Represents a text node that has an optional parent and some text
  /// </summary>
  public class HtmlText : IHtmlElement {
    /// <summary>
    ///   Initializes a new instance of <see cref="HtmlText" />
    /// </summary>
    /// <param name="text">The text</param>
    public HtmlText(string text) {
      if (text == null)
        Text = string.Empty;
      Text = text;
    }

    /// <summary>
    ///   The inner text
    /// </summary>
    public string Text { get; }

    public HtmlString ToHtml(TagRenderMode? tagRenderMode = null) {
      return new HtmlString(Text);
    }

    public void WriteTo(TextWriter writer, HtmlEncoder encoder, TagRenderMode? tagRenderMode = null) {
      new HtmlString(Text).WriteTo(writer, encoder);
    }

    public override string ToString() {
      return Text;
    }

    private bool Equals(HtmlText other) {
      return string.Equals(Text, other.Text);
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(null, obj))
        return false;
      if (ReferenceEquals(this, obj))
        return true;
      if (obj.GetType() != GetType())
        return false;
      return Equals((HtmlText)obj);
    }

    public override int GetHashCode() {
      return Text.GetHashCode();
    }
  }
}