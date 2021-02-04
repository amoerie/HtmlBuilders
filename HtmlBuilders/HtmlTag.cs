using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HtmlBuilders {
  /// <summary>
  ///   Represents an html tag that can have a parent, children, attributes, etc.
  ///   This is a wrapper around the built in <see cref="TagBuilder"/> class but with a lot more convenience and builder style patterns.
  /// </summary>
  public class HtmlTag : IHtmlElement {
    /// <summary>
    ///   The inner <see cref="TagBuilder" />
    /// </summary>
    private readonly string _tagName;

    /// <summary>
    ///   The attributes of this tag
    /// </summary>
    private readonly IImmutableDictionary<string, string> _attributes = ImmutableDictionary<string, string>.Empty;

    /// <summary>
    ///   The inner list of contents
    /// </summary>
    private readonly IImmutableList<IHtmlElement> _contents = ImmutableList<IHtmlElement>.Empty;

    /// <summary>
    ///   The inner <see cref="TagRenderMode" />
    /// </summary>
    private readonly TagRenderMode? _tagRenderMode;

    /// <summary>
    ///   Initializes a new instance of <see cref="HtmlTag" />
    /// </summary>
    /// <param name="tagName">The tag name</param>
    public HtmlTag(string tagName) {
      _tagName = tagName ?? throw new ArgumentNullException(nameof(tagName));
    }

    #region Immutable construction

    HtmlTag(string tagName, IImmutableDictionary<string, string> attributes, IImmutableList<IHtmlElement> contents, TagRenderMode? tagRenderMode) {
      _tagName = tagName;
      _attributes = attributes;
      _contents = contents;
      _tagRenderMode = tagRenderMode;
    }

    /// <summary>
    ///   Creates a new HtmlTag replacing the inner TagBuilder
    /// </summary>
    HtmlTag WithAttributes(IImmutableDictionary<string, string> attributes) {
      return new HtmlTag(_tagName, attributes, _contents, _tagRenderMode);
    }

    /// <summary>
    ///   Creates a new HtmlTag replacing the inner Contents
    /// </summary>
    HtmlTag WithContents(IImmutableList<IHtmlElement> contents) {
      return new HtmlTag(_tagName, _attributes, contents, _tagRenderMode);
    }

    /// <summary>
    ///   Creates a new HtmlTag replacing the inner TagRenderMode
    /// </summary>
    HtmlTag WithTagRenderMode(TagRenderMode? tagRenderMode) {
      return new HtmlTag(_tagName, _attributes, _contents, tagRenderMode);
    }

    #endregion

    /// <summary>
    ///   Gets the tag name
    /// </summary>
    public string TagName => _tagName;

    /// <summary>
    ///   Gets the attributes as an immutable dictionary
    /// </summary>
    public IImmutableDictionary<string, string> Attributes => _attributes;

    /// <summary>
    ///   Gets the contents.
    ///   This property is very similar to the <see cref="TagBuilder.InnerHtml" /> property, save for the fact that instead of
    ///   just a string
    ///   this is now a collection of elements. This allows for more extensive manipulation and DOM traversal similar to what
    ///   can be done with jQuery.
    /// </summary>
    public IImmutableList<IHtmlElement> Contents => _contents;

    #region Attributes that can be toggled

    /// <summary>
    ///   Triggers an attribute on this tag. Common examples include "checked", "selected", "disabled", ...
    /// </summary>
    /// <param name="attribute">The name of the attribute</param>
    /// <param name="value">A value indicating whether this attribute should be set on this tag or not.</param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public HtmlTag ToggleAttribute(string attribute, bool value) {
      if (attribute == null) {
        throw new ArgumentNullException(nameof(attribute));
      }

      return value ? Attribute(attribute, attribute) : RemoveAttribute(attribute);
    }

    #endregion

    #region ToString

    /// <inheritdoc />
    public override string ToString() {
      var tag = TagName;
      var attributes = string.Join(" ", Attributes.Select(kvp => $"{kvp.Key}=\"{kvp.Value}\""));
      var selfClosing = _tagRenderMode == TagRenderMode.SelfClosing ? " /" : "";
      return $"<{tag} {attributes}{selfClosing}>";
    }

    #endregion

    #region DOM Traversal

    /// <summary>
    ///   Gets the children in the order that they were added.
    ///   <br /><strong>WARNING</strong>: Text nodes (<see cref="HtmlText" />) do not count as children and will not be
    ///   included in this property.
    ///   See <see cref="Contents" /> if you want the text nodes to be included.
    /// </summary>
    public IEnumerable<HtmlTag> Children => Contents.OfType<HtmlTag>();

    /// <summary>
    ///   Finds the children or the children of those children, etc. that match the <paramref name="filter" />
    /// </summary>
    /// <param name="filter">The filter that specifies the conditions that each subnode must satisfy</param>
    /// <returns>The sub elements that satisfied the filter</returns>
    public IEnumerable<HtmlTag> Find(Func<HtmlTag, bool> filter) => Children.Where(filter).Concat(Children.SelectMany(c => c.Find(filter)));

    /// <summary>
    ///   Prepends <see cref="IHtmlContent" /> to the <see cref="Contents" />
    /// </summary>
    /// <param name="htmlContents">
    ///   The html contents that will be inserted at the beginning of the contents of this tag, before all other content 
    /// </param>
    /// <returns>this <see cref="HtmlTag" /></returns>
    public HtmlTag Prepend(params IHtmlContent[] htmlContents) => htmlContents == null ? this : Prepend(htmlContents.AsEnumerable());

    /// <summary>
    ///   Prepends <see cref="IHtmlContent" /> to the <see cref="Contents" />
    /// </summary>
    /// <param name="htmlContents">
    ///   The html contents that will be inserted at the beginning of the contents of this tag, before all other content
    /// </param>
    /// <returns>this <see cref="HtmlTag" /></returns>
    public HtmlTag Prepend(IEnumerable<IHtmlContent> htmlContents) => htmlContents == null ? this : Prepend(htmlContents.SelectMany(htmlContent => ParseAll(htmlContent)));

    /// <summary>
    ///   Prepends an <see cref="IHtmlElement" /> to the <see cref="Contents" />
    /// </summary>
    /// <param name="elements">
    ///   The elements that will be inserted at the beginning of the contents of this tag, before all
    ///   other content elements
    /// </param>
    /// <returns>this <see cref="HtmlTag" /></returns>
    public HtmlTag Prepend(params IHtmlElement[] elements) => Insert(0, elements);

    /// <summary>
    ///   Prepends an <see cref="IHtmlElement" /> to the <see cref="Contents" />
    /// </summary>
    /// <param name="elements">
    ///   The elements that will be inserted at the beginning of the contents of this tag, before all
    ///   other content elements
    /// </param>
    /// <returns>this <see cref="HtmlTag" /></returns>
    public HtmlTag Prepend(IEnumerable<IHtmlElement> elements) => Insert(0, elements);

    /// <summary>
    ///   Prepends an <see cref="HtmlText" /> to the <see cref="Contents" />
    /// </summary>
    /// <param name="text">
    ///   The text that will be inserted as a <see cref="HtmlText" /> at the beginning of the contents of this
    ///   tag, before all other content elements
    /// </param>
    /// <returns>this <see cref="HtmlTag" /></returns>
    public HtmlTag Prepend(string text) => Insert(0, new HtmlText(text));

    /// <summary>
    ///   Inserts an <see cref="IHtmlElement" /> to the <see cref="Contents" /> at the given <paramref name="index" />
    /// </summary>
    /// <param name="index">The index at which the <paramref name="elements" /> should be inserted</param>
    /// <param name="elements">
    ///   The elements that will be inserted at the specifix <paramref name="index" /> of the contents of
    ///   this tag
    /// </param>
    /// <returns>this <see cref="HtmlTag" /></returns>
    public HtmlTag Insert(int index, params IHtmlElement[] elements) => elements == null ? this : Insert(index, elements.AsEnumerable());

    /// <summary>
    ///   Inserts an <see cref="IHtmlElement" /> to the <see cref="Contents" /> at the given <paramref name="index" />
    /// </summary>
    /// <param name="index">The index at which the <paramref name="elements" /> should be inserted</param>
    /// <param name="elements">
    ///   The elements that will be inserted at the specifix <paramref name="index" /> of the contents of
    ///   this tag
    /// </param>
    /// <returns>this <see cref="HtmlTag" /></returns>
    public HtmlTag Insert(int index, IEnumerable<IHtmlElement> elements) {
      if (elements == null) {
        return this;
      }

      if (index < 0 || index > _contents.Count) {
        throw new IndexOutOfRangeException(
          $"Cannot insert anything at index '{index}', content elements count = {Contents.Count()}");
      }

      return WithContents(_contents.InsertRange(index, elements.Where(e => e != null)));
    }

    /// <summary>
    ///   Appends <see cref="IHtmlContent" /> to the <see cref="Contents" />
    /// </summary>
    /// <param name="htmlContents">
    ///   The html contents that will be inserted at the end of the contents of this tag, after all other
    ///   content elements
    /// </param>
    /// <returns>this <see cref="HtmlTag" /></returns>
    public HtmlTag Append(params IHtmlContent[] htmlContents) => htmlContents == null ? this : Append(htmlContents.AsEnumerable());

    /// <summary>
    ///   Appends <see cref="IHtmlContent" /> to the <see cref="Contents" />
    /// </summary>
    /// <param name="htmlContents">
    ///   The html contents that will be inserted at the end of the contents of this tag, after all other content
    /// </param>
    /// <returns>this <see cref="HtmlTag" /></returns>
    public HtmlTag Append(IEnumerable<IHtmlContent> htmlContents) => htmlContents == null ? this : Append(htmlContents.SelectMany(htmlContent => ParseAll(htmlContent)));

    /// <summary>
    ///   Inserts an <see cref="IHtmlElement" /> to the <see cref="Contents" /> at the given <paramref name="index" />
    /// </summary>
    /// <param name="index">The index at which the <paramref name="text" /> should be inserted</param>
    /// <param name="text">
    ///   The text that will be inserted as a <see cref="HtmlText" /> at the specifix
    ///   <paramref name="index" /> of the contents of this tag
    /// </param>
    /// <returns>this <see cref="HtmlTag" /></returns>
    public HtmlTag Insert(int index, string text) => text == null ? this : Insert(index, new HtmlText(text));

    /// <summary>
    ///   Appends an <see cref="IHtmlElement" /> to the <see cref="Contents" />
    /// </summary>
    /// <param name="elements">
    ///   The elements that will be inserted at the end of the contents of this tag, after all other
    ///   content elements
    /// </param>
    /// <returns>this <see cref="HtmlTag" /></returns>
    public HtmlTag Append(params IHtmlElement[] elements) => elements == null ? this : Append(elements.AsEnumerable());

    /// <summary>
    ///   Appends an <see cref="IHtmlElement" /> to the <see cref="Contents" />
    /// </summary>
    /// <param name="elements">
    ///   The elements that will be inserted at the end of the contents of this tag, after all other
    ///   content elements
    /// </param>
    /// <returns>this <see cref="HtmlTag" /></returns>
    public HtmlTag Append(IEnumerable<IHtmlElement> elements) => elements == null ? this : WithContents(_contents.AddRange(elements.Where(e => e != null)));

    /// <summary>
    ///   Appends an <see cref="IHtmlElement" /> to the <see cref="Contents" />
    /// </summary>
    /// <param name="text">
    ///   The text that will be inserted as a <see cref="HtmlText" /> at the end of the contents of this tag,
    ///   after all other content elements
    /// </param>
    /// <returns>this <see cref="HtmlTag" /></returns>
    public HtmlTag Append(string text) => Append(new HtmlText(text));

    #endregion

    #region Attribute

    /// <summary>
    ///   Safely reads an attribute or returns null when the attribute is absent
    /// </summary>
    public string this[string attribute] => _attributes.TryGetValue(attribute, out var value) ? value : null;

    /// <summary>
    ///   True when the attribute is known by this HtmlTag
    /// </summary>
    /// <param name="attribute">The attribute</param>
    /// <returns>True if the attribute was present in the attributes dictionary or false otherwise</returns>
    public bool HasAttribute(string attribute) {
      return _attributes.ContainsKey(attribute);
    }

    /// <summary>
    ///   Sets an attribute on this tag
    /// </summary>
    /// <param name="attribute">The attribute to set</param>
    /// <param name="value">The value to set</param>
    /// <param name="replaceExisting">
    ///   A value indicating whether the <paramref name="value" /> should override the existing value for the
    ///   <paramref name="attribute" />, if any.
    /// </param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public HtmlTag Attribute(string attribute, string value, bool replaceExisting = true) {
      if (attribute == null) {
        throw new ArgumentNullException(nameof(attribute));
      }
      return replaceExisting || !_attributes.ContainsKey(attribute)
        ? WithAttributes(_attributes.SetItem(attribute, value))
        : this;
    }

    /// <summary>
    ///   Removes an attribute from this tag
    /// </summary>
    /// <param name="attribute">The attribute to remove</param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public HtmlTag RemoveAttribute(string attribute) {
      if (attribute == null) {
        throw new ArgumentNullException(nameof(attribute));
      }
      return WithAttributes(_attributes.Remove(attribute));
    }

    #endregion

    #region Data Attributes

    /// <summary>
    ///   Sets a data attribute. This method will automatically prepend 'data-' to the attribute name if the attribute name
    ///   does not start with 'data-'.
    /// </summary>
    /// <param name="attribute">The name of the attribute</param>
    /// <param name="value">The value</param>
    /// <param name="replaceExisting">
    ///   A value indicating whether the existing data attribute, if any, should have its value
    ///   replace by the <paramref name="value" /> provided.
    /// </param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public HtmlTag Data(string attribute, string value, bool replaceExisting = true) {
      if (attribute == null) {
        throw new ArgumentNullException(nameof(attribute));
      }

      return Attribute(attribute.StartsWith("data-") ? attribute : "data-" + attribute, value, replaceExisting);
    }

    /// <summary>
    ///   Sets a data attribute. This method will automatically prepend 'data-' to the attribute name if the attribute name
    ///   does not start with 'data-'.
    /// </summary>
    /// <param name="data">The anonymous data object containing properties that should be set as data attributes</param>
    /// <param name="replaceExisting">
    ///   A value indicating whether the existing data attributes, if any, should have their values
    ///   replaced by the values found in <paramref name="data" />
    /// </param>
    /// <example>
    ///   <code>
    ///     // results in &lt;a data-index-url="/index" data-about-url="/about"&gt;&lt;a&gt;
    ///     new HtmlTag('a').Data(new { index_url = "/index", about_url = "/about"}).ToHtml() 
    /// </code>
    /// </example>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public HtmlTag Data(object data, bool replaceExisting = true) {
      if (data == null) {
        throw new ArgumentNullException(nameof(data));
      }

      var newAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(data)
        .Select(entry => new {
          Attribute = entry.Key.StartsWith("data-") ? entry.Key : "data-" + entry.Key,
          Value = Convert.ToString(entry.Value)
        });

      return newAttributes.Aggregate(this, (htmlTag, next) => htmlTag.Attribute(next.Attribute, next.Value, replaceExisting));
    }

    #endregion

    #region Styles

    /// <summary>
    ///   Creates a new HtmlTag replacing the inner style attribute
    /// </summary>
    HtmlTag WithStyles(IImmutableDictionary<string, string> styles) {
      if (styles.Count == 0) return RemoveAttribute("style");
      var style = string.Join(";", styles.Select(kvp => kvp.Key + ":" + kvp.Value));
      return WithAttributes(_attributes.SetItem("style", style));
    }

    /// <summary>
    ///   Gets the 'style' attribute of this <see cref="HtmlTag" />.
    ///   Note that this is a utility method that parses the 'style' attribute from a string into a
    ///   <see cref="IReadOnlyDictionary{TKey,TValue}" />
    /// </summary>
    public IImmutableDictionary<string, string> Styles {
      get {
        if (!_attributes.TryGetValue("style", out string styles)) {
          return ImmutableDictionary<string, string>.Empty;
        }

        var styleRulesSplit = styles.Split(';');
        var styleRuleStep1 =
          styleRulesSplit.Select(styleRule => new {StyleRule = styleRule, SeparatorIndex = styleRule.IndexOf(':')})
            .ToArray();
        var styleRuleStep2 = styleRuleStep1.Select(a =>
          new {
            StyleKey = a.StyleRule.Substring(0, a.SeparatorIndex),
            StyleValue = a.StyleRule.Substring(a.SeparatorIndex + 1, a.StyleRule.Length - a.SeparatorIndex - 1)
          }).ToArray();


        return styleRuleStep2.ToImmutableDictionary(a => a.StyleKey, a => a.StyleValue);
      }
    }

    /// <summary>
    ///   Sets a css style <paramref name="key" /> and <paramref name="value" /> on the 'style' attribute.
    /// </summary>
    /// <param name="key">The type of the style (width, height, margin, padding, ...)</param>
    /// <param name="value">The value of the style (any valid css value for the given <paramref name="key" />)</param>
    /// <param name="replaceExisting">
    ///   A value indicating whether the existing value for the given <paramref name="key" />
    ///   should be updated or not, if such a key is already present in the 'style' attribute.
    /// </param>
    /// <returns></returns>
    public HtmlTag Style(string key, string value, bool replaceExisting = true) {
      if (key == null) {
        throw new ArgumentNullException(nameof(key));
      }

      if (value == null) {
        throw new ArgumentNullException(nameof(value));
      }

      if (key.Contains(";")) {
        throw new ArgumentException($"Style key cannot contain ';'! Key was '{key}'");
      }

      if (value.Contains(";")) {
        throw new ArgumentException($"Style value cannot contain ';'! Value was '{key}'");
      }

      if (!replaceExisting && Styles.ContainsKey(key)) {
        return this;
      }

      return WithStyles(Styles.SetItem(key, value));
    }

    /// <summary>
    ///   Removes a <paramref name="key" /> from the <see cref="Styles" />, if such a key is present.
    /// </summary>
    /// <param name="key">The key to remove from the style</param>
    /// <returns></returns>
    public HtmlTag RemoveStyle(string key) {
      if (key == null) {
        throw new ArgumentNullException(nameof(key));
      }
      return WithStyles(Styles.Remove(key));
    }

    #endregion

    #region Class

    /// <summary>
    ///   Creates a new HtmlTag replacing the inner class attribute
    /// </summary>
    HtmlTag WithClass(IImmutableList<string> classes) {
      if (classes.Count == 0) return RemoveAttribute("class");
      var @class = string.Join(" ", classes.Distinct());
      return WithAttributes(_attributes.SetItem("class", @class));
    }

    /// <summary>
    ///   Gets the classes of this <see cref="HtmlTag" />
    /// </summary>
    public IImmutableList<string> Classes => _attributes.TryGetValue("class", out string classes)
      ? classes.Split(' ').ToImmutableList()
      : ImmutableList<string>.Empty;

    /// <summary>
    ///   Returns true if this <see cref="HtmlTag" /> has the <paramref name="class" /> or false otherwise
    /// </summary>
    /// <param name="class">The class</param>
    /// <returns>True if this <see cref="HtmlTag" /> has the <paramref name="class" /> or false otherwise</returns>
    public bool HasClass(string @class) {
      return Classes.Any(c => string.Equals(c, @class));
    }

    /// <summary>
    ///   Adds a class to this tag.
    /// </summary>
    /// <param name="class">The class(es) to add</param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public HtmlTag Class(string @class) {
      if (@class == null) {
        return this;
      }

      return WithClass(Classes.AddRange(@class.Split(' ')));
    }

    /// <summary>
    ///   Removes one or more classes from this tag.
    /// </summary>
    /// <param name="class">The class(es) to remove</param>
    /// <returns>
    ///   This <see cref="HtmlTag"/></returns>
    public HtmlTag RemoveClass(string @class) {
      if (@class == null) {
        return this;
      }

      return WithClass(Classes.RemoveRange(@class.Split(' ')));
    }

    #endregion

    #region To Html

    /// <summary>
    ///   Sets the <see cref="TagRenderMode" /> that will be used when rendering this <see cref="HtmlTag" />.
    ///   This is a convenient way to build up an <see cref="HtmlTag" /> tree, configure the <see cref="TagRenderMode" /> for
    ///   each node in the tree,
    ///   and then render it entirely in one go.
    /// </summary>
    /// <param name="tagRenderMode">The tag render mode</param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public HtmlTag Render(TagRenderMode tagRenderMode) {
      return WithTagRenderMode(tagRenderMode);
    }

    /// <inheritdoc />
    public void WriteTo(TextWriter writer, HtmlEncoder encoder) {
      var tagBuilder = new TagBuilder(_tagName) {
        TagRenderMode = _tagRenderMode ?? TagRenderMode.Normal
      };
      foreach (var attribute in _attributes) {
        tagBuilder.Attributes.Add(attribute);
      }
      switch (tagBuilder.TagRenderMode) {
        case TagRenderMode.StartTag:
          tagBuilder.RenderStartTag().WriteTo(writer, encoder);
          break;
        case TagRenderMode.EndTag:
          tagBuilder.RenderEndTag().WriteTo(writer, encoder);
          break;
        case TagRenderMode.SelfClosing:
          if (Contents.Any()) {
            throw new InvalidOperationException(
              "Cannot render this tag with the self closing TagRenderMode because this tag has inner contents: " + this);
          }

          tagBuilder.RenderSelfClosingTag().WriteTo(writer, encoder);
          break;
        default:
          tagBuilder.RenderStartTag().WriteTo(writer, encoder);

          foreach (var content in Contents) {
            content.WriteTo(writer, encoder);
          }

          tagBuilder.RenderEndTag().WriteTo(writer, encoder);
          break;
      }
    }

    #endregion

    #region Factory methods
    
    /// <summary>
    ///   Parses an <see cref="HtmlTag" /> from the given <paramref name="html" />
    /// </summary>
    /// <param name="html">The html</param>
    /// <param name="validateSyntax">A value indicating whether the html should be checked for syntax errors.</param>
    /// <returns>A new <see cref="HtmlTag" /> that is an object representation of the <paramref name="html" /></returns>
    /// <exception cref="InvalidOperationException">
    ///   If <paramref name="validateSyntax" /> is true and syntax errors are
    ///   encountered in the <paramref name="html" />
    /// </exception>
    public static HtmlTag Parse(string html, bool validateSyntax = false) {
      if (html == null) {
        throw new ArgumentNullException(nameof(html));
      }

      return Parse(new StringReader(HtmlEntity.DeEntitize(html)), validateSyntax);
    }


    /// <summary>
    ///   Parses an <see cref="HtmlTag" /> from the given <paramref name="textReader" />
    /// </summary>
    /// <param name="textReader">The text reader</param>
    /// <param name="validateSyntax">A value indicating whether the html should be checked for syntax errors.</param>
    /// <returns>A new <see cref="HtmlTag" /> that is an object representation of the <paramref name="textReader" /></returns>
    /// <exception cref="InvalidOperationException">
    ///   If <paramref name="validateSyntax" /> is true and syntax errors are
    ///   encountered in the <paramref name="textReader" />
    /// </exception>
    public static HtmlTag Parse(TextReader textReader, bool validateSyntax = false) {
      if (textReader == null) {
        throw new ArgumentNullException(nameof(textReader));
      }

      var htmlDocument = new HtmlDocument {OptionCheckSyntax = validateSyntax};
      HtmlNode.ElementsFlags.Remove("option");
      htmlDocument.Load(textReader);
      return Parse(htmlDocument, validateSyntax);
    }

    /// <summary>
    ///   Parses an <see cref="HtmlTag" /> from the given <paramref name="htmlDocument" />
    /// </summary>
    /// <param name="htmlDocument">The html document containing the html</param>
    /// <param name="validateSyntax">A value indicating whether the html should be checked for syntax errors.</param>
    /// <returns>
    ///   Multiple <see cref="HtmlTag" />s that is an object representation of the <paramref name="htmlDocument" />
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///   If <paramref name="validateSyntax" /> is true and syntax errors are
    ///   encountered in the <paramref name="htmlDocument" />
    /// </exception>
    public static HtmlTag Parse(HtmlDocument htmlDocument, bool validateSyntax = false) {
      if (htmlDocument.ParseErrors.Any() && validateSyntax) {
        var readableErrors =
          htmlDocument.ParseErrors.Select(
            e => $"Code = {e.Code}, SourceText = {e.SourceText}, Reason = {e.Reason}");
        throw new InvalidOperationException($"Parse errors found: \n{string.Join("\n", readableErrors)}");
      }

      if (htmlDocument.DocumentNode.ChildNodes.Count != 1) {
        throw new ArgumentException(
          "Html contains more than one element. The parse method can only be used for single html tags! Input was : \n" +
          htmlDocument.DocumentNode.OuterHtml);
      }

      htmlDocument.OptionWriteEmptyNodes = true;
      return ParseHtmlTag(htmlDocument.DocumentNode.ChildNodes.Single());
    }

    /// <summary>
    ///   Parses multiple <see cref="HtmlTag" />s from the given <paramref name="htmlContent" />
    /// </summary>
    /// <param name="htmlContent">The html content</param>
    /// <param name="validateSyntax">A value indicating whether the html should be checked for syntax errors.</param>
    /// <returns>A collection of <see cref="HtmlTag" /></returns>
    /// <exception cref="InvalidOperationException">
    ///   If <paramref name="validateSyntax" /> is true and syntax errors are
    ///   encountered in the <paramref name="htmlContent" />
    /// </exception>
    public static IEnumerable<IHtmlElement> ParseAll(IHtmlContent htmlContent, bool validateSyntax = false) {
      if (htmlContent == null) {
        throw new ArgumentNullException(nameof(htmlContent));
      }
      // special case: html content is already an HtmlTag!
      if (htmlContent is HtmlTag alreadyHtmlTag) {
        return new[] {alreadyHtmlTag};
      }
      // special case: string that may contain HTML but must be encoded when writing
      if (htmlContent is StringHtmlContent s) {
        return new[] { new HtmlText(s) };
      }
      // special case: TagBuilder
      if (htmlContent is TagBuilder tagBuilder) {
        var htmlTag = new HtmlTag(tagBuilder.TagName)
          .WithTagRenderMode(tagBuilder.TagRenderMode);

        if (tagBuilder.Attributes.Any()) {
          htmlTag = tagBuilder.Attributes
            .Aggregate(htmlTag,
              (tag, attribute) => tag.Attribute(attribute.Key, HtmlEntity.DeEntitize(attribute.Value)));
        }

        if (tagBuilder.HasInnerHtml)
          htmlTag = htmlTag.WithContents(ParseAll(tagBuilder.InnerHtml, validateSyntax).ToImmutableList());

        return new[] { htmlTag };
      }
      return ParseAll(htmlContent.ToHtmlString(), validateSyntax);
    }

    /// <summary>
    ///   Parses multiple <see cref="HtmlTag" />s from the given <paramref name="html" />
    /// </summary>
    /// <param name="html">The html</param>
    /// <param name="validateSyntax">A value indicating whether the html should be checked for syntax errors.</param>
    /// <returns>A collection of <see cref="HtmlTag" /></returns>
    /// <exception cref="InvalidOperationException">
    ///   If <paramref name="validateSyntax" /> is true and syntax errors are
    ///   encountered in the <paramref name="html" />
    /// </exception>
    public static IEnumerable<IHtmlElement> ParseAll(string html, bool validateSyntax = false) {
      if (html == null) {
        throw new ArgumentNullException(nameof(html));
      }

      using (var reader = new StringReader(HtmlEntity.DeEntitize(html))) {
        return ParseAll(reader, validateSyntax);
      }
    }

    /// <summary>
    ///   Parses multiple <see cref="HtmlTag" />s from the given <paramref name="textReader" />
    /// </summary>
    /// <param name="textReader">The text reader</param>
    /// <param name="validateSyntax">A value indicating whether the html should be checked for syntax errors.</param>
    /// <returns>A collection of <see cref="HtmlTag" /></returns>
    /// <exception cref="InvalidOperationException">
    ///   If <paramref name="validateSyntax" /> is true and syntax errors are
    ///   encountered in the <paramref name="textReader" />
    /// </exception>
    public static IEnumerable<IHtmlElement> ParseAll(TextReader textReader, bool validateSyntax = false) {
      if (textReader == null) {
        throw new ArgumentNullException(nameof(textReader));
      }

      var htmlDocument = new HtmlDocument {OptionCheckSyntax = validateSyntax};
      HtmlNode.ElementsFlags.Remove("option");
      htmlDocument.Load(textReader);
      return ParseAll(htmlDocument, validateSyntax);
    }

    /// <summary>
    ///   Parses multiple <see cref="HtmlTag" />s from the given <paramref name="htmlDocument" />
    /// </summary>
    /// <param name="htmlDocument">The html document</param>
    /// <param name="validateSyntax">A value indicating whether the html should be checked for syntax errors.</param>
    /// <returns>A collection of <see cref="HtmlTag" /></returns>
    /// <exception cref="InvalidOperationException">
    ///   If <paramref name="validateSyntax" /> is true and syntax errors are
    ///   encountered in the <paramref name="htmlDocument" />
    /// </exception>
    public static IEnumerable<IHtmlElement> ParseAll(HtmlDocument htmlDocument, bool validateSyntax = false) {
      if (htmlDocument.ParseErrors.Any() && validateSyntax) {
        var readableErrors =
          htmlDocument.ParseErrors.Select(
            e => $"Code = {e.Code}, SourceText = {e.SourceText}, Reason = {e.Reason}");
        throw new InvalidOperationException($"Parse errors found: \n{string.Join("\n", readableErrors)}");
      }

      foreach (var childNode in htmlDocument.DocumentNode.ChildNodes) {
        if (childNode.NodeType == HtmlNodeType.Text) yield return ParseHtmlText(childNode);
        if (childNode.NodeType == HtmlNodeType.Element) {
          if (string.IsNullOrEmpty(childNode.Name)) 
            continue;
          yield return ParseHtmlTag(childNode);

        }
      }
    }

    private static HtmlTag ParseHtmlTag(HtmlNode htmlNode) {
      var htmlTag = new HtmlTag(htmlNode.Name);
      if (htmlNode.Closed && !htmlNode.ChildNodes.Any()) {
        htmlTag = htmlTag.Render(TagRenderMode.SelfClosing);

        // fix: self closing I tags (used by icon fonts) have rendering issues if they do not have content
        if (htmlTag.TagName.Equals(HtmlTags.I.TagName)) {
          htmlTag = htmlTag.Render(TagRenderMode.Normal).Append(" ");
        }
      }

      foreach (var attribute in htmlNode.Attributes) {
        htmlTag = htmlTag.Attribute(attribute.Name, attribute.Value);
      }

      foreach (var childNode in htmlNode.ChildNodes) {
        IHtmlElement childElement = null;
        switch (childNode.NodeType) {
          case HtmlNodeType.Element:
            childElement = ParseHtmlTag(childNode);
            break;
          case HtmlNodeType.Text:
            childElement = ParseHtmlText(childNode);
            break;
        }

        if (childElement != null) {
          htmlTag = htmlTag.Append(childElement);
        }
      }

      return htmlTag;
    }

    private static HtmlText ParseHtmlText(HtmlNode htmlNode) {
      return new HtmlText(new HtmlString(htmlNode.InnerText));
    }

    #endregion

    #region Equality

    private bool Equals(HtmlTag other) {
      if (ReferenceEquals(null, other)) {
        return false;
      }

      if (ReferenceEquals(this, other)) {
        return true;
      }

      if (!string.Equals(TagName, other.TagName)) {
        return false;
      }

      if (_attributes.Count != other._attributes.Count) {
        return false;
      }

      if (!AttributesComparer.Equals(_attributes, other._attributes, "class", "style")) {
        return false;
      }

      if (!AttributesComparer.Equals(Styles, other.Styles)) {
        return false;
      }

      return Classes.OrderBy(c => c).SequenceEqual(other.Classes.OrderBy(c => c))
             && Contents.SequenceEqual(other.Contents);
    }

    /// <summary>
    ///   Returns true if this <see cref="HtmlTag" /> is equivalent to <paramref name="other" />. If any of the attributes or
    ///   the children are different,
    ///   this method will return false. It is important to note that the order in which styles and classes appear will not
    ///   affect the equality in any way.
    ///   However, the order of the <see cref="Contents" /> <strong>does</strong> matter.
    ///   As a rule of thumb, if one <see cref="HtmlTag" /> would have the same display presentation and behavior in a browser
    ///   as another <see cref="HtmlTag" />, they are considered equal.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public override bool Equals(object other) {
      if (ReferenceEquals(null, other)) {
        return false;
      }

      if (ReferenceEquals(this, other)) {
        return true;
      }

      return other.GetType() == GetType() && Equals((HtmlTag) other);
    }

    /// <inheritdoc />
    public override int GetHashCode() {
      var hash = 17;
      hash = hash * 23 + TagName.GetHashCode();
      foreach (
        var attribute in _attributes.Where(attribute => !string.Equals(attribute.Key, "style") && !string.Equals(attribute.Key, "class"))
          .OrderBy(attribute => attribute.Key)) {
        hash = hash * 23 + attribute.Key.GetHashCode();
        hash = hash * 23 + attribute.Value.GetHashCode();
      }

      foreach (var style in Styles.OrderBy(style => style.Key)) {
        hash = hash * 23 + style.Key.GetHashCode();
        hash = hash * 23 + style.Value.GetHashCode();
      }

      hash = Classes.OrderBy(c => c).Aggregate(hash, (current, @class) => current * 23 + @class.GetHashCode());
      return Contents.Aggregate(hash, (current, content) => current * 23 + content.GetHashCode());
    }

    #endregion
  }
}