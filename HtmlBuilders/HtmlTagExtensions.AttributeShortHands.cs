namespace HtmlBuilders {
  /// <summary>
  /// Contains a whole series of useful methods to manipulate common HTML attributes of an HtmlTag 
  /// </summary>
  public static partial class HtmlTagExtensions {
    /// <summary>
    ///   Sets the name property. This is a shorthand for the <see cref="HtmlTag.Attribute" /> method with 'name' as the
    ///   attribute parameter value.
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="name">The value for the 'name' attribute</param>
    /// <param name="replaceExisting">
    ///   A value indicating whether the existing attribute, if any, should have its value replaced
    ///   by the <paramref name="name" /> provided.
    /// </param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Name(this HtmlTag htmlTag, string name, bool replaceExisting = true) {
      return htmlTag.Attribute("name", name, replaceExisting);
    }

    /// <summary>
    ///   Sets the title property. This is a shorthand for the <see cref="HtmlTag.Attribute" /> method with 'title' as the
    ///   attribute parameter value.
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="title">The value for the 'title' attribute</param>
    /// <param name="replaceExisting">
    ///   A value indicating whether the existing attribute, if any, should have its value replaced
    ///   by the <paramref name="title" /> provided.
    /// </param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Title(this HtmlTag htmlTag, string title, bool replaceExisting = true) {
      return htmlTag.Attribute("title", title, replaceExisting);
    }

    /// <summary>
    ///   Sets the id property. This is a shorthand for the <see cref="HtmlTag.Attribute" /> method with 'id' as the attribute
    ///   parameter value.
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="id">The value for the 'id' attribute</param>
    /// <param name="replaceExisting">
    ///   A value indicating whether the existing attribute, if any, should have its value replaced
    ///   by the <paramref name="id" /> provided.
    /// </param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Id(this HtmlTag htmlTag, string id, bool replaceExisting = true) {
      return htmlTag.Attribute("id", id, replaceExisting);
    }

    /// <summary>
    ///   Sets the type property. This is a shorthand for the <see cref="HtmlTag.Attribute" /> method with 'type' as the
    ///   attribute parameter value.
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="type">The value for the 'type' attribute</param>
    /// <param name="replaceExisting">
    ///   A value indicating whether the existing attribute, if any, should have its value replaced
    ///   by the <paramref name="type" /> provided.
    /// </param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Type(this HtmlTag htmlTag, string type, bool replaceExisting = true) {
      return htmlTag.Attribute("type", type, replaceExisting);
    }

    /// <summary>
    ///   Sets the value property. This is a shorthand for the <see cref="HtmlTag.Attribute" /> method with 'value' as the
    ///   attribute parameter value.
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="value">The value for the 'value' attribute</param>
    /// <param name="replaceExisting">
    ///   A value indicating whether the existing attribute, if any, should have its value replaced
    ///   by the <paramref name="value" /> provided.
    /// </param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Value(this HtmlTag htmlTag, string value, bool replaceExisting = true) {
      return htmlTag.Attribute("value", value, replaceExisting);
    }

    /// <summary>
    ///   Sets the href property. This is a shorthand for the <see cref="HtmlTag.Attribute" /> method with 'href' as the
    ///   attribute parameter value.
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="href">The value for the 'href' attribute</param>
    /// <param name="replaceExisting">
    ///   A value indicating whether the existing attribute, if any, should have its value replaced
    ///   by the <paramref name="href" /> provided.
    /// </param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Href(this HtmlTag htmlTag, string href, bool replaceExisting = true) {
      return htmlTag.Attribute("href", href, replaceExisting);
    }

    /// <summary>
    ///   Sets the src property. This is a shorthand for the <see cref="HtmlTag.Attribute" /> method with 'src' as the
    ///   attribute parameter value.
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="src">The value for the 'src' attribute</param>
    /// <param name="replaceExisting">
    ///   A value indicating whether the existing attribute, if any, should have its value replaced
    ///   by the <paramref name="src" /> provided.
    /// </param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Src(this HtmlTag htmlTag, string src, bool replaceExisting = true) {
      return htmlTag.Attribute("src", src, replaceExisting);
    }
  }
}