using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using HtmlBuilders.Comparers;

namespace HtmlBuilders
{
    /// <summary>
    ///     Represents an html tag that can have a parent, children, attributes, etc.
    /// </summary>
    public class HtmlTag : IHtmlElement, IDictionary<string, string>
    {
        /// <summary>
        ///     The inner list of contents
        /// </summary>
        private IList<IHtmlElement> _contents = new List<IHtmlElement>();

        /// <summary>
        ///     The inner <see cref="TagBuilder"/>
        /// </summary>
        private readonly TagBuilder _tagBuilder;

        /// <summary>
        ///     The inner <see cref="TagRenderMode"/>
        /// </summary>
        private TagRenderMode? _tagRenderMode;

        /// <summary>
        ///     Initializes a new instance of <see cref="HtmlTag"/>
        /// </summary>
        /// <param name="tagName">The tag name</param>
        public HtmlTag(string tagName)
        {
            if(tagName == null)
                throw new ArgumentNullException("tagName");
            _tagBuilder = new TagBuilder(tagName);
        }

        /// <summary>
        ///     Gets the tag name
        /// </summary>
        public string TagName
        {
            get { return _tagBuilder.TagName; }
        }
        
        #region DOM Traversal

        /// <summary>
        ///     Gets the children in the order that they were added.
        ///     <br/><strong>WARNING</strong>: Text nodes (<see cref="HtmlText"/>) do not count as children and will not be included in this property.
        ///     See <see cref="Contents"/> if you want the text nodes to be included.
        /// </summary>
        public IEnumerable<HtmlTag> Children
        {
            get { return Contents.Where(c => c is HtmlTag).Cast<HtmlTag>(); }
        }

        /// <summary>
        ///     Gets or sets the contents.
        ///     This property is very similar to the <see cref="TagBuilder.InnerHtml"/> property, save for the fact that instead of just a string 
        ///     this is now a collection of elements. This allows for more extensive manipulation and DOM traversal similar to what can be done with jQuery.
        /// </summary>
        public IEnumerable<IHtmlElement> Contents { get { return _contents; } set { _contents = value.ToList(); } }

        /// <summary>
        ///     Gets the (optional) parent of this <see cref="HtmlTag"/>.
        /// </summary>
        public HtmlTag Parent { get; set; }

        /// <summary>
        ///     Gets the parents of this <see cref="HtmlTag"/> in a 'from inside out' order.
        /// </summary>
        public IEnumerable<HtmlTag> Parents
        {
            get
            {
                return Parent == null ? Enumerable.Empty<HtmlTag>() : new[] { Parent }.Concat(Parent.Parents);
            }
        }

        /// <summary>
        ///     Gets the siblings of this <see cref="HtmlTag"/>
        /// </summary>
        public IEnumerable<HtmlTag> Siblings
        {
            get
            {
                if (Parent == null)
                    return Enumerable.Empty<HtmlTag>();
                return Parent.Children.Where(child => !ReferenceEquals(child, this));
            }
        }

        /// <summary>
        ///     Finds the children or the children of those children, etc. that match the <paramref name="filter"/>
        /// </summary>
        /// <param name="filter">The filter that specifies the conditions that each subnode must satisfy</param>
        /// <returns>The sub elements that satisfied the filter</returns>
        public IEnumerable<HtmlTag> Find(Func<HtmlTag, bool> filter)
        {
            return Children.Where(filter).Concat(Children.SelectMany(c => c.Find(filter)));
        }  

        /// <summary>
        ///     Prepends an <see cref="IHtmlElement"/> to the <see cref="Contents"/>
        /// </summary>
        /// <param name="elements">The elements that will be inserted at the beginning of the contents of this tag, before all other content elements</param>
        /// <returns>this <see cref="HtmlTag"/></returns>
        public HtmlTag Prepend(params IHtmlElement[] elements)
        {
            return Insert(0, elements);
        }

        /// <summary>
        ///     Prepends an <see cref="IHtmlElement"/> to the <see cref="Contents"/>
        /// </summary>
        /// <param name="elements">The elements that will be inserted at the beginning of the contents of this tag, before all other content elements</param>
        /// <returns>this <see cref="HtmlTag"/></returns>
        public HtmlTag Prepend(IEnumerable<IHtmlElement> elements)
        {
            return Insert(0, elements);
        }

        /// <summary>
        ///     Prepends an <see cref="HtmlText"/> to the <see cref="Contents"/>
        /// </summary>
        /// <param name="text">The text that will be inserted as a <see cref="HtmlText"/> at the beginning of the contents of this tag, before all other content elements</param>
        /// <returns>this <see cref="HtmlTag"/></returns>
        public HtmlTag Prepend(string text)
        {
            return Insert(0, new HtmlText(text));
        }

        /// <summary>
        ///     Inserts an <see cref="IHtmlElement"/> to the <see cref="Contents"/> at the given <paramref name="index"/>
        /// </summary>
        /// <param name="index">The index at which the <paramref name="elements"/> should be inserted</param>
        /// <param name="elements">The elements that will be inserted at the specifix <paramref name="index"/> of the contents of this tag</param>
        /// <returns>this <see cref="HtmlTag"/></returns>
        public HtmlTag Insert(int index, params IHtmlElement[] elements)
        {
            if (elements == null)
                throw new ArgumentNullException("elements");
            return Insert(index, elements.AsEnumerable());
        }

        /// <summary>
        ///     Inserts an <see cref="IHtmlElement"/> to the <see cref="Contents"/> at the given <paramref name="index"/>
        /// </summary>
        /// <param name="index">The index at which the <paramref name="elements"/> should be inserted</param>
        /// <param name="elements">The elements that will be inserted at the specifix <paramref name="index"/> of the contents of this tag</param>
        /// <returns>this <see cref="HtmlTag"/></returns>
        public HtmlTag Insert(int index, IEnumerable<IHtmlElement> elements)
        {
            if (elements == null)
                throw new ArgumentNullException("elements");
            if (index < 0 || index > _contents.Count)
                throw new IndexOutOfRangeException(string.Format("Cannot insert anything at index '{0}', content elements count = {1}", index, Contents.Count()));
            foreach (var element in elements.Reverse())
            {
                _contents.Insert(index, element);
                element.Parent = this;
            }
            return this;
        }

        /// <summary>
        ///     Inserts an <see cref="IHtmlElement"/> to the <see cref="Contents"/> at the given <paramref name="index"/>
        /// </summary>
        /// <param name="index">The index at which the <paramref name="text"/> should be inserted</param>
        /// <param name="text">The text that will be inserted as a <see cref="HtmlText"/> at the specifix <paramref name="index"/> of the contents of this tag</param>
        /// <returns>this <see cref="HtmlTag"/></returns>
        public HtmlTag Insert(int index, string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            return Insert(index, new HtmlText(text));
        }

        /// <summary>
        ///     Appends an <see cref="IHtmlElement"/> to the <see cref="Contents"/>
        /// </summary>
        /// <param name="elements">The elements that will be inserted at the end of the contents of this tag, after all other content elements</param>
        /// <returns>this <see cref="HtmlTag"/></returns>
        public HtmlTag Append(params IHtmlElement[] elements)
        {
            if (elements == null)
                throw new ArgumentNullException("elements");
            return Append(elements.AsEnumerable());
        }

        /// <summary>
        ///     Appends an <see cref="IHtmlElement"/> to the <see cref="Contents"/>
        /// </summary>
        /// <param name="elements">The elements that will be inserted at the end of the contents of this tag, after all other content elements</param>
        /// <returns>this <see cref="HtmlTag"/></returns>
        public HtmlTag Append(IEnumerable<IHtmlElement> elements)
        {
            if (elements == null)
                throw new ArgumentNullException("elements");
            foreach (var element in elements)
            {
                _contents.Add(element);
                element.Parent = this;
            }
            return this;
        }

        /// <summary>
        ///     Appends an <see cref="IHtmlElement"/> to the <see cref="Contents"/>
        /// </summary>
        /// <param name="text">The text that will be inserted as a <see cref="HtmlText"/> at the end of the contents of this tag, after all other content elements</param>
        /// <returns>this <see cref="HtmlTag"/></returns>
        public HtmlTag Append(string text)
        {
            return Append(new HtmlText(text));
        }

        #endregion

        #region IDictionary<string, string> implementation for Attributes

        public int Count
        {
            get { return _tagBuilder.Attributes.Count; }
        }

        public bool IsReadOnly
        {
            get { return _tagBuilder.Attributes.IsReadOnly; }
        }

        public ICollection<string> Keys
        {
            get { return _tagBuilder.Attributes.Keys; }
        }

        public ICollection<string> Values
        {
            get { return _tagBuilder.Attributes.Values; }
        }
        
        public string this[string attribute]
        {
            get { return _tagBuilder.Attributes[attribute]; }
            set { _tagBuilder.Attributes[attribute] = value; }
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _tagBuilder.Attributes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tagBuilder.Attributes.GetEnumerator();
        }

        public void Add(KeyValuePair<string, string> item)
        {
            _tagBuilder.Attributes.Add(item);
        }

        public void Clear()
        {
            _tagBuilder.Attributes.Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return _tagBuilder.Attributes.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            _tagBuilder.Attributes.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return _tagBuilder.Attributes.Remove(item);
        }

        public bool ContainsKey(string attribute)
        {
            return _tagBuilder.Attributes.ContainsKey(attribute);
        }

        public void Add(string attribute, string value)
        {
            _tagBuilder.Attributes.Add(attribute, value);
        }

        public bool Remove(string attribute)
        {
            return _tagBuilder.Attributes.Remove(attribute);
        }

        public bool TryGetValue(string attribute, out string value)
        {
            return _tagBuilder.Attributes.TryGetValue(attribute, out value);
        }

        #endregion

        #region Attribute

        /// <summary>
        ///     Alias method for <see cref="ContainsKey"/>
        /// </summary>
        /// <param name="attribute">The attribute</param>
        /// <returns>True if the attribute was present in the attributes dictionary or false otherwise</returns>
        public bool HasAttribute(string attribute)
        {
            return ContainsKey(attribute);
        }

        /// <summary>
        ///     Sets an attribute on this tag
        /// </summary>
        /// <param name="attribute">The attribute to set</param>
        /// <param name="value">The value to set</param>
        /// <param name="replaceExisting">
        ///     A value indicating whether the <paramref name="value"/> should override the existing value for the <paramref name="attribute"/>, if any.
        /// </param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public HtmlTag Attribute(string attribute, string value, bool replaceExisting = true)
        {
            if (attribute == null)
                throw new ArgumentNullException("attribute");
            _tagBuilder.MergeAttribute(attribute, value, replaceExisting);
            return this;
        }

        #endregion

        #region Attributes that can be toggled

        /// <summary>
        ///     Triggers an attribute on this tag. Common examples include "checked", "selected", "disabled", ...
        /// </summary>
        /// <param name="attribute">The name of the attribute</param>
        /// <param name="value">A value indicating whether this attribute should be set on this tag or not.</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public HtmlTag ToggleAttribute(string attribute, bool value)
        {
            if(attribute == null)
                throw new ArgumentNullException("attribute");
            if (value)
                return Attribute(attribute, attribute);
            Remove(attribute);
            return this;
        }

        #endregion

        #region Data Attributes

        /// <summary>
        ///     Sets a data attribute. This method will automatically prepend 'data-' to the attribute name if the attribute name does not start with 'data-'.
        /// </summary>
        /// <param name="attribute">The name of the attribute</param>
        /// <param name="value">The value</param>
        /// <param name="replaceExisting">A value indicating whether the existing data attribute, if any, should have its value replace by the <paramref name="value"/> provided.</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public HtmlTag Data(string attribute, string value, bool replaceExisting = true)
        {
            if(attribute == null)
                throw new ArgumentNullException("attribute");
            return Attribute(attribute.StartsWith("data-") ? attribute : "data-" + attribute, value, replaceExisting);
        }

        /// <summary>
        ///     Sets a data attribute. This method will automatically prepend 'data-' to the attribute name if the attribute name does not start with 'data-'.
        /// </summary>
        /// <param name="data">The anonymous data object containing properties that should be set as data attributes</param>
        /// <param name="replaceExisting">A value indicating whether the existing data attributes, if any, should have their values replaced by the values found in <paramref name="data"/></param>
        /// <example>
        /// <code>
        ///     // results in &lt;a data-index-url="/index" data-about-url="/about"&gt;&lt;a&gt;
        ///     new HtmlTag('a').Data(new { index_url = "/index", about_url = "/about"}).ToHtml() 
        /// </code>
        /// </example>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public HtmlTag Data(object data, bool replaceExisting = true)
        {
            if(data == null)
                throw new ArgumentNullException("data");
            var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(data);
            foreach (var htmlAttribute in htmlAttributes)
            {
                string attribute = htmlAttribute.Key;
                Attribute(attribute.StartsWith("data-") ? attribute : "data-" + attribute, Convert.ToString(htmlAttribute.Value), replaceExisting);
            }
            return this;
        }

        #endregion

        #region Styles

        /// <summary>
        ///     Gets or sets the 'style' attribute of this <see cref="HtmlTag"/>.
        ///     Note that this is a utility method that parses the 'style' attribute from a string into a <see cref="IReadOnlyDictionary{TKey,TValue}"/>
        /// </summary>
        public IReadOnlyDictionary<string, string> Styles
        {
            get
            {
                string styles;
                if (!TryGetValue("style", out styles))
                    return new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
                var styleRules = styles.Split(';').Select(styleRule => styleRule.Split(':')).ToArray();
                if (styleRules.All(s => s.Length == 2))
                    return styleRules.ToDictionary(styleRule => styleRule[0], styleRule => styleRule[1]);
                var invalidStyleRules = styleRules.Where(s => s.Length != 2);
                throw new InvalidOperationException(string.Format("Detected invalid style rules: {0}", string.Join(",", invalidStyleRules.Select(s => string.Join(":", s)))));
            }
            set
            {
                if (value.Count == 0)
                {
                    Remove("style");
                }
                else
                {
                    string newStyle = string.Join(";", value.Select(s => string.Format("{0}:{1}", s.Key, s.Value)));
                    Attribute("style", newStyle);
                }
            }
        }

        /// <summary>
        ///     Sets a css style <paramref name="key"/> and <paramref name="value"/> on the 'style' attribute.
        /// </summary>
        /// <param name="key">The type of the style (width, height, margin, padding, ...)</param>
        /// <param name="value">The value of the style (any valid css value for the given <paramref name="key"/>)</param>
        /// <param name="replaceExisting">A value indicating whether the existing value for the given <paramref name="key"/> should be updated or not, if such a key is already present in the 'style' attribute.</param>
        /// <returns></returns>
        public HtmlTag Style(string key, string value, bool replaceExisting = true)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");
            if (key.Contains(";"))
                throw new ArgumentException(string.Format("Style key cannot contain ';'! Key was '{0}'", key));
            if (value.Contains(";"))
                throw new ArgumentException(string.Format("Style value cannot contain ';'! Value was '{0}'", key));
            var styles = Styles.ToDictionary(s => s.Key, s => s.Value);
            if (!styles.ContainsKey(key) || replaceExisting)
                styles[key] = value;
            Styles = styles;
            return this;
        }

        /// <summary>
        ///     Removes a <paramref name="key"/> from the <see cref="Styles"/>, if such a key is present.
        /// </summary>
        /// <param name="key">The key to remove from the style</param>
        /// <returns></returns>
        public HtmlTag RemoveStyle(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            Styles = Styles.Where(s => !string.Equals(s.Key, key)).ToDictionary(s => s.Key, s => s.Value);
            return this;
        }

        #endregion

        #region Class

        /// <summary>
        ///     Gets or sets the classes of this <see cref="HtmlTag"/>
        ///     This is a utility method to easily manipulate the 'class' attribute
        /// </summary>
        public IEnumerable<string> Classes
        {
            get
            {
                string classes;
                return TryGetValue("class", out classes) ? classes.Split(' ') : Enumerable.Empty<string>();
            }
            set
            {
                if (!value.Any())
                {
                    Remove("class");
                }
                else
                {
                    Attribute("class", string.Join(" ", value));
                }
                
            }
        }

        /// <summary>
        ///     Returns true if this <see cref="HtmlTag"/> has the <paramref name="class"/> or false otherwise
        /// </summary>
        /// <param name="class">The class</param>
        /// <returns>True if this <see cref="HtmlTag"/> has the <paramref name="class"/> or false otherwise</returns>
        public bool HasClass(string @class)
        {
            return Classes.Any(c => string.Equals(c, @class));
        }

        /// <summary>
        ///     Adds a class to this tag.
        /// </summary>
        /// <param name="class">The class(es) to add</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public HtmlTag Class(string @class)
        {
            if(@class == null)
                throw new ArgumentNullException("class");
            var classesToAdd = @class.Split(' ');
            Classes = Classes.Concat(classesToAdd).Distinct();
            return this;
        }

        /// <summary>
        ///     Removes one or more classes from this tag.
        /// </summary>
        /// <param name="class">The class(es) to remove</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public HtmlTag RemoveClass(string @class)
        {
            if(@class == null)
                throw new ArgumentNullException("class");
            var classesToRemove = @class.Split(' ');
            Classes = Classes.Where(c => !classesToRemove.Contains(c));
            return this;
        }

        #endregion

        #region Merge attributes by dictionary or anonymous object

        /// <summary>
        ///     Adds new attributes or optionally replaces existing attributes in the tag.
        /// </summary>
        /// <param name="attributes">The collection of attributes to add or replace.</param>
        /// <typeparam name="TKey">The type of the key object.</typeparam>
        /// <typeparam name="TValue">The type of the value object.</typeparam>
        public HtmlTag Merge<TKey, TValue>(IDictionary<TKey, TValue> attributes)
        {
            _tagBuilder.MergeAttributes(attributes);
            return this;
        }

        /// <summary>
        ///     Adds new attributes or optionally replaces existing attributes in the tag.
        /// </summary>
        /// <param name="attributes">The collection of attributes to add or replace.</param>
        /// <param name="replaceExisting">
        ///     For each attribute in <paramref name="attributes" />, true to replace the attribute if an
        ///     attribute already exists that has the same key, or false to leave the original attribute unchanged.
        /// </param>
        /// <typeparam name="TKey">The type of the key object.</typeparam>
        /// <typeparam name="TValue">The type of the value object.</typeparam>
        public HtmlTag Merge<TKey, TValue>(IDictionary<TKey, TValue> attributes, bool replaceExisting)
        {
            _tagBuilder.MergeAttributes(attributes, replaceExisting);
            return this;
        }

        /// <summary>
        ///     Adds new attributes or optionally replaces existing attributes in the tag.
        /// </summary>
        /// <param name="attributes">The collection of attributes to add or replace.</param>
        public HtmlTag Merge(object attributes)
        {
            return Merge(HtmlHelper.AnonymousObjectToHtmlAttributes(attributes));
        }

        /// <summary>
        ///     Adds new attributes or optionally replaces existing attributes in the tag.
        /// </summary>
        /// <param name="attributes">The collection of attributes to add or replace.</param>
        /// <param name="replaceExisting">
        ///     For each attribute in <paramref name="attributes" />, true to replace the attribute if an
        ///     attribute already exists that has the same key, or false to leave the original attribute unchanged.
        /// </param>
        public HtmlTag Merge(object attributes, bool replaceExisting)
        {
            return Merge(HtmlHelper.AnonymousObjectToHtmlAttributes(attributes), replaceExisting);
        }

        #endregion

        #region To Html

        /// <summary>
        ///     Sets the <see cref="TagRenderMode"/> that will be used when rendering this <see cref="HtmlTag"/>.
        ///     This is a convenient way to build up an <see cref="HtmlTag"/> tree, configure the <see cref="TagRenderMode"/> for each node in the tree,
        ///     and then render it entirely in one go.
        /// </summary>
        /// <param name="tagRenderMode">The tag render mode</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public HtmlTag Render(TagRenderMode tagRenderMode)
        {
            _tagRenderMode = tagRenderMode;
            return this;
        }
        
        /// <summary>
        ///     Renders and returns the HTML tag by using the specified render mode.
        /// </summary>
        /// <param name="tagRenderMode">
        ///     The render mode. If this parameter is not specified, the <see cref="TagRenderMode"/> that was specified with the <see cref="Render"/> method will be used.
        ///     If the <see cref="Render"/> method was never called for this <see cref="HtmlTag"/>, the <see cref="TagRenderMode"/> will default to <see cref="TagRenderMode.Normal"/>
        ///     <br/><strong>IMPORTANT: </strong> When using <see cref="TagRenderMode.StartTag"/> or <see cref="TagRenderMode.EndTag"/>, 
        ///     the <see cref="Contents"/> of this <see cref="HtmlTag"/> will not be rendered.
        ///     This is because when you have more than 1 content element, it does not make sense to only render the start or end tags. Since the API exposes the
        ///     <see cref="Contents"/> and <see cref="Children"/> separately, the responsibility is then with the developer to render the HTML as he or she wishes.
        ///     However, when using <see cref="TagRenderMode.Normal"/> (or passing no parameters, since <see cref="TagRenderMode.Normal"/> is the default value),
        ///     the <see cref="Contents"/> <strong> will</strong> be taken into account since there can't be any confusion as to what the expected HTML output would be.
        ///     You can specify <see cref="TagRenderMode"/> for this <see cref="HtmlTag"/> (or any of its <see cref="Children"/> ) by using the <see cref="Render"/> method.
        /// </param>
        /// <returns>The rendered HTML tag by using the specified render mode</returns>
        /// <exception cref="InvalidOperationException">When <see cref="TagRenderMode.SelfClosing"/> is used but the <see cref="HtmlTag"/> is not empty. (The <see cref="Contents"/> are not empty)</exception>
        public IHtmlString ToHtml(TagRenderMode? tagRenderMode = null)
        {
            tagRenderMode = tagRenderMode ?? _tagRenderMode ?? TagRenderMode.Normal;
            var stringBuilder = new StringBuilder();
            switch (tagRenderMode)
            {
                case TagRenderMode.StartTag:
                    stringBuilder.Append(_tagBuilder.ToString(TagRenderMode.StartTag));
                    break;
                case TagRenderMode.EndTag:
                    stringBuilder.Append(_tagBuilder.ToString(TagRenderMode.EndTag));
                    break;
                case TagRenderMode.SelfClosing:
                    if (Contents.Any())
                    {
                        throw new InvalidOperationException("Cannot render this tag with the self closing TagRenderMode because this tag has inner contents: Count = " + Contents.Count());
                    }
                    stringBuilder.Append(_tagBuilder.ToString(TagRenderMode.SelfClosing));
                    break;
                default:
                    stringBuilder.Append(_tagBuilder.ToString(TagRenderMode.StartTag));
                    foreach (var content in Contents)
                    {
                        stringBuilder.Append(content.ToHtml());
                    }
                    stringBuilder.Append(_tagBuilder.ToString(TagRenderMode.EndTag));
                    break;
            }
            return MvcHtmlString.Create(stringBuilder.ToString());
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return ToHtml().ToHtmlString();
        }

        #endregion 

        #region Factory methods

        /// <summary>
        ///     Parses an <see cref="HtmlTag"/> from the given <paramref name="html"/>
        /// </summary>
        /// <param name="html">The html</param>
        /// <param name="validateSyntax">A value indicating whether the html should be checked for syntax errors.</param>
        /// <returns>A new <see cref="HtmlTag"/> that is an object representation of the <paramref name="html"/></returns>
        /// <exception cref="InvalidOperationException">If <paramref name="validateSyntax"/> is true and syntax errors are encountered in the <paramref name="html"/></exception>
        public static HtmlTag Parse(IHtmlString html, bool validateSyntax = false)
        {
            if (html == null)
                throw new ArgumentNullException("html");
            return Parse(html.ToString(), validateSyntax);
        }

        /// <summary>
        ///     Parses an <see cref="HtmlTag"/> from the given <paramref name="html"/>
        /// </summary>
        /// <param name="html">The html</param>
        /// <param name="validateSyntax">A value indicating whether the html should be checked for syntax errors.</param>
        /// <returns>A new <see cref="HtmlTag"/> that is an object representation of the <paramref name="html"/></returns>
        /// <exception cref="InvalidOperationException">If <paramref name="validateSyntax"/> is true and syntax errors are encountered in the <paramref name="html"/></exception>
        public static HtmlTag Parse(string html, bool validateSyntax = false)
        {
            if (html == null)
                throw new ArgumentNullException("html");
            return Parse(new StringReader(html), validateSyntax);
        }


        /// <summary>
        ///     Parses an <see cref="HtmlTag"/> from the given <paramref name="textReader"/>
        /// </summary>
        /// <param name="textReader">The text reader</param>
        /// <param name="validateSyntax">A value indicating whether the html should be checked for syntax errors.</param>
        /// <returns>A new <see cref="HtmlTag"/> that is an object representation of the <paramref name="textReader"/></returns>
        /// <exception cref="InvalidOperationException">If <paramref name="validateSyntax"/> is true and syntax errors are encountered in the <paramref name="textReader"/></exception>
        public static HtmlTag Parse(TextReader textReader, bool validateSyntax = false)
        {
            if (textReader == null)
                throw new ArgumentNullException("textReader");
            var htmlDocument = new HtmlDocument {  OptionCheckSyntax = validateSyntax};
            HtmlNode.ElementsFlags.Remove("option");
            htmlDocument.Load(textReader);
            return Parse(htmlDocument, validateSyntax);
        }

        /// <summary>
        ///     Parses an <see cref="HtmlTag"/> from the given <paramref name="htmlDocument"/>
        /// </summary>
        /// <param name="htmlDocument">The html document containing the html</param>
        /// <param name="validateSyntax">A value indicating whether the html should be checked for syntax errors.</param>
        /// <returns>Multiple <see cref="HtmlTag"/>s that is an object representation of the <paramref name="htmlDocument"/></returns>
        /// <exception cref="InvalidOperationException">If <paramref name="validateSyntax"/> is true and syntax errors are encountered in the <paramref name="htmlDocument"/></exception>
        public static HtmlTag Parse(HtmlDocument htmlDocument, bool validateSyntax = false)
        {
            if (htmlDocument.ParseErrors.Any() && validateSyntax)
            {
                var readableErrors = htmlDocument.ParseErrors.Select(e => string.Format("Code = {0}, SourceText = {1}, Reason = {2}", e.Code, e.SourceText, e.Reason));
                throw new InvalidOperationException(string.Format("Parse errors found: \n{0}", string.Join("\n", readableErrors)));
            }
            if(htmlDocument.DocumentNode.ChildNodes.Count != 1)
                throw new ArgumentException("Html contains more than one element. The parse method can only be used for single html tags! Input was : " + htmlDocument.DocumentNode);
            htmlDocument.OptionWriteEmptyNodes = true;
            return ParseHtmlTag(htmlDocument.DocumentNode.ChildNodes.Single());
        }

        /// <summary>
        ///     Parses multiple <see cref="HtmlTag"/>s from the given <paramref name="html"/>
        /// </summary>
        /// <param name="html">The html</param>
        /// <param name="validateSyntax">A value indicating whether the html should be checked for syntax errors.</param>
        /// <returns>A collection of <see cref="HtmlTag"/></returns>
        /// <exception cref="InvalidOperationException">If <paramref name="validateSyntax"/> is true and syntax errors are encountered in the <paramref name="html"/></exception>
        public static IEnumerable<HtmlTag> ParseAll(IHtmlString html, bool validateSyntax = false)
        {
            if (html == null)
                throw new ArgumentNullException("html");
            return ParseAll(html.ToString(), validateSyntax);
        }

        /// <summary>
        ///     Parses multiple <see cref="HtmlTag"/>s from the given <paramref name="html"/>
        /// </summary>
        /// <param name="html">The html</param>
        /// <param name="validateSyntax">A value indicating whether the html should be checked for syntax errors.</param>
        /// <returns>A collection of <see cref="HtmlTag"/></returns>
        /// <exception cref="InvalidOperationException">If <paramref name="validateSyntax"/> is true and syntax errors are encountered in the <paramref name="html"/></exception>
        public static IEnumerable<HtmlTag> ParseAll(string html, bool validateSyntax = false)
        {
            if (html == null)
                throw new ArgumentNullException("html");
            return ParseAll(new StringReader(html), validateSyntax);
        }


        /// <summary>
        ///     Parses multiple <see cref="HtmlTag"/>s from the given <paramref name="textReader"/>
        /// </summary>
        /// <param name="textReader">The text reader</param>
        /// <param name="validateSyntax">A value indicating whether the html should be checked for syntax errors.</param>
        /// <returns>A collection of <see cref="HtmlTag"/></returns>
        /// <exception cref="InvalidOperationException">If <paramref name="validateSyntax"/> is true and syntax errors are encountered in the <paramref name="textReader"/></exception>
        public static IEnumerable<HtmlTag> ParseAll(TextReader textReader, bool validateSyntax = false)
        {
            if (textReader == null)
                throw new ArgumentNullException("textReader");
            var htmlDocument = new HtmlDocument { OptionCheckSyntax = validateSyntax };
            HtmlNode.ElementsFlags.Remove("option");
            htmlDocument.Load(textReader);
            return ParseAll(htmlDocument, validateSyntax);
        }

        /// <summary>
        ///     Parses multiple <see cref="HtmlTag"/>s from the given <paramref name="htmlDocument"/>
        /// </summary>
        /// <param name="htmlDocument">The html document</param>
        /// <param name="validateSyntax">A value indicating whether the html should be checked for syntax errors.</param>
        /// <returns>A collection of <see cref="HtmlTag"/></returns>
        /// <exception cref="InvalidOperationException">If <paramref name="validateSyntax"/> is true and syntax errors are encountered in the <paramref name="htmlDocument"/></exception>
        public static IEnumerable<HtmlTag> ParseAll(HtmlDocument htmlDocument, bool validateSyntax = false)
        {
            if (htmlDocument.ParseErrors.Any() && validateSyntax)
            {
                var readableErrors = htmlDocument.ParseErrors.Select(e => string.Format("Code = {0}, SourceText = {1}, Reason = {2}", e.Code, e.SourceText, e.Reason));
                throw new InvalidOperationException(string.Format("Parse errors found: \n{0}", string.Join("\n", readableErrors)));
            }
            return htmlDocument.DocumentNode.ChildNodes.Select(ParseHtmlTag);
        }

        private static HtmlTag ParseHtmlTag(HtmlNode htmlNode)
        {
            var htmlTag = new HtmlTag(htmlNode.Name);
            if (htmlNode.Closed && !htmlNode.ChildNodes.Any())
                htmlTag.Render(TagRenderMode.SelfClosing);
            foreach (var attribute in htmlNode.Attributes)
            {
                htmlTag.Attribute(attribute.Name, attribute.Value);
            }
            foreach (var childNode in htmlNode.ChildNodes)
            {
                IHtmlElement childElement = null;
                switch (childNode.NodeType)
                {
                    case HtmlNodeType.Element:
                        childElement = ParseHtmlTag(childNode);
                        break;
                    case HtmlNodeType.Text:
                        childElement = ParseHtmlText(childNode);
                        break;
                }
                if (childElement != null)
                    htmlTag.Append(childElement);
            }
            return htmlTag;
        }

        private static HtmlText ParseHtmlText(HtmlNode htmlNode)
        {
            return new HtmlText(htmlNode.InnerText);
        }

        #endregion

        #region Equality

        private bool Equals(HtmlTag other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (!string.Equals(TagName, other.TagName))
                return false;
            if (Count != other.Count)
                return false;
            if (!DictionaryComparer.Equals(this, other, keysToExclude: new[] { "class", "style" }))
                return false;
            if (!DictionaryComparer.Equals(Styles, other.Styles))
                return false;
            return Classes.OrderBy(c => c).SequenceEqual(other.Classes.OrderBy(c => c))
                && Contents.SequenceEqual(other.Contents);
        }

        /// <summary>
        ///     Returns true if this <see cref="HtmlTag"/> is equivalent to <paramref name="other"/>. If any of the attributes or the children are different,
        ///     this method will return false. It is important to note that the order in which styles and classes appear will not affect the equality in any way.
        ///     However, the order of the <see cref="Contents"/> <strong>does</strong> matter. 
        ///     As a rule of thumb, if one <see cref="HtmlTag"/> would have the same display presentation and behavior in a browser as another <see cref="HtmlTag"/>, they are considered equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.GetType() == GetType() && Equals((HtmlTag)other);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + TagName.GetHashCode();
            foreach (var attribute in this.Where(attribute => !string.Equals(attribute.Key, "style") && !string.Equals(attribute.Key, "class"))
                                          .OrderBy(attribute => attribute.Key))
            {
                hash = hash * 23 + attribute.Key.GetHashCode();
                hash = hash * 23 + attribute.Value.GetHashCode();
            }
            foreach (var style in Styles.OrderBy(style => style.Key))
            {
                hash = hash * 23 + style.Key.GetHashCode();
                hash = hash * 23 + style.Value.GetHashCode();
            }
            hash = Classes.OrderBy(c => c).Aggregate(hash, (current, @class) => current*23 + @class.GetHashCode());
            return Contents.Aggregate(hash, (current, content) => current*23 + content.GetHashCode());
        }

        #endregion
    }
}
