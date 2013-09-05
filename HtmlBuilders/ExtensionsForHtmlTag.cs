using System;

namespace HtmlBuilders
{
    /// <summary>
    ///     Contains utility extensions for <see cref="HtmlTag"/>
    /// </summary>
    public static class ExtensionsForHtmlTag
    {
        #region Attribute shorthands
        ///  <summary>
        ///      Sets the name property. This is a shorthand for the <see cref="HtmlTag.Attribute"/> method with 'name' as the attribute parameter value.
        ///  </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="name">The value for the 'name' attribute</param>
        /// <param name="replaceExisting">A value indicating whether the existing attribute, if any, should have its value replaced by the <paramref name="name"/> provided.</param>
        ///  <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Name(this HtmlTag htmlTag, string name, bool replaceExisting = true)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            return htmlTag.Attribute("name", name, replaceExisting);
        }

        /// <summary>
        ///     Sets the title property. This is a shorthand for the <see cref="HtmlTag.Attribute"/> method with 'title' as the attribute parameter value.
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="title">The value for the 'title' attribute</param>
        ///<param name="replaceExisting">A value indicating whether the existing attribute, if any, should have its value replaced by the <paramref name="title"/> provided.</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Title(this HtmlTag htmlTag, string title, bool replaceExisting = true)
        {
            if (title == null)
                throw new ArgumentNullException("title");
            return htmlTag.Attribute("title", title, replaceExisting);
        }

        /// <summary>
        ///     Sets the id property. This is a shorthand for the <see cref="HtmlTag.Attribute"/> method with 'id' as the attribute parameter value.
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="id">The value for the 'id' attribute</param>
        ///<param name="replaceExisting">A value indicating whether the existing attribute, if any, should have its value replaced by the <paramref name="id"/> provided.</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Id(this HtmlTag htmlTag, string id, bool replaceExisting = true)
        {
            if (id == null)
                throw new ArgumentNullException("id");
            return htmlTag.Attribute("id", id, replaceExisting);
        }

        /// <summary>
        ///     Sets the type property. This is a shorthand for the <see cref="HtmlTag.Attribute"/> method with 'type' as the attribute parameter value.
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="type">The value for the 'type' attribute</param>
        ///<param name="replaceExisting">A value indicating whether the existing attribute, if any, should have its value replaced by the <paramref name="type"/> provided.</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Type(this HtmlTag htmlTag, string type, bool replaceExisting = true)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            return htmlTag.Attribute("type", type, replaceExisting);
        } 

        /// <summary>
        ///     Sets the value property. This is a shorthand for the <see cref="HtmlTag.Attribute"/> method with 'value' as the attribute parameter value.
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="value">The value for the 'value' attribute</param>
        ///<param name="replaceExisting">A value indicating whether the existing attribute, if any, should have its value replaced by the <paramref name="value"/> provided.</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Value(this HtmlTag htmlTag, string value, bool replaceExisting = true)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            return htmlTag.Attribute("value", value, replaceExisting);
        }

        /// <summary>
        ///     Sets the href property. This is a shorthand for the <see cref="HtmlTag.Attribute"/> method with 'href' as the attribute parameter value.
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="href">The value for the 'href' attribute</param>
        ///<param name="replaceExisting">A value indicating whether the existing attribute, if any, should have its value replaced by the <paramref name="href"/> provided.</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Href(this HtmlTag htmlTag, string href, bool replaceExisting = true)
        {
            if (href == null)
                throw new ArgumentNullException("href");
            return htmlTag.Attribute("href", href, replaceExisting);
        } 
        #endregion

        #region Attributes that can be toggled
        /// <summary>
        ///     Sets the 'checked' attribute to 'checked' if <paramref name="checked"/> is true or removes the attribute if <paramref name="checked"/> is false
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="checked">A value indicating whether this tag should have the attribute 'checked' with value 'checked' or not.</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Checked(this HtmlTag htmlTag, bool @checked)
        {
            return htmlTag.ToggleAttribute("checked", @checked);
        }

        /// <summary>
        ///     Sets the 'disabled' attribute to 'disabled' if <paramref name="disabled"/> is true or removes the attribute if <paramref name="disabled"/> is false
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="disabled">A value indicating whether this tag should have the attribute 'disabled' with value 'disabled' or not.</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Disabled(this HtmlTag htmlTag, bool disabled)
        {
            return htmlTag.ToggleAttribute("disabled", disabled);
        }

        /// <summary>
        ///     Sets the 'selected' attribute to 'selected' if <paramref name="selected"/> is true or removes the attribute if <paramref name="selected"/> is false
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="selected">A value indicating whether this tag should have the attribute 'selected' with value 'selected' or not.</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Selected(this HtmlTag htmlTag, bool selected)
        {
            return htmlTag.ToggleAttribute("selected", selected);
        } 

        /// <summary>
        ///     Sets the 'readonly' attribute to 'readonly' if <paramref name="readonly"/> is true or removes the attribute if <paramref name="readonly"/> is false
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="readonly">A value indicating whether this tag should have the attribute 'readonly' with value 'readonly' or not.</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Readonly(this HtmlTag htmlTag, bool @readonly)
        {
            return htmlTag.ToggleAttribute("readonly", @readonly);
        } 
        #endregion

        #region Style shorthands
        /// <summary>
        ///     Sets the width style. This is a shorthand for calling the <see cref="HtmlTag.Style"/> method with the 'width' key
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="width">The width. This can be any valid css value for 'width'</param>
        /// <param name="replaceExisting">A value indicating whether the existing width, if any, should be overriden or not</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Width(this HtmlTag htmlTag, string width, bool replaceExisting = true)
        {
            if (width == null)
                throw new ArgumentNullException("width");
            return htmlTag.Style("width", width, replaceExisting);
        }

        /// <summary>
        ///     Sets the height style. This is a shorthand for calling the <see cref="HtmlTag.Style"/> method with the 'height' key
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="height">The height. This can be any valid css value for 'height'</param>
        /// <param name="replaceExisting">A value indicating whether the existing height, if any, should be overriden or not</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Height(this HtmlTag htmlTag, string height, bool replaceExisting = true)
        {
            if (height == null)
                throw new ArgumentNullException("height");
            return htmlTag.Style("height", height, replaceExisting);
        } 
        
        /// <summary>
        ///     Sets the margin style. This is a shorthand for calling the <see cref="HtmlTag.Style"/> method with the 'margin' key
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="margin">The margin. This can be any valid css value for 'margin'</param>
        /// <param name="replaceExisting">A value indicating whether the existing margin, if any, should be overriden or not</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Margin(this HtmlTag htmlTag, string margin, bool replaceExisting = true)
        {
            if (margin == null)
                throw new ArgumentNullException("margin");
            return htmlTag.Style("margin", margin, replaceExisting);
        } 

        /// <summary>
        ///     Sets the padding style. This is a shorthand for calling the <see cref="HtmlTag.Style"/> method with the 'padding' key
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="padding">The padding. This can be any valid css value for 'padding'</param>
        /// <param name="replaceExisting">A value indicating whether the existing padding, if any, should be overriden or not</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Padding(this HtmlTag htmlTag, string padding, bool replaceExisting = true)
        {
            if (padding == null)
                throw new ArgumentNullException("padding");
            return htmlTag.Style("padding", padding, replaceExisting);
        } 

        /// <summary>
        ///     Sets the color style. This is a shorthand for calling the <see cref="HtmlTag.Style"/> method with the 'color' key
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="color">The color. This can be any valid css value for 'color'</param>
        /// <param name="replaceExisting">A value indicating whether the existing color, if any, should be overriden or not</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Color(this HtmlTag htmlTag, string color, bool replaceExisting = true)
        {
            if (color == null)
                throw new ArgumentNullException("color");
            return htmlTag.Style("color", color, replaceExisting);
        } 
        
        /// <summary>
        ///     Sets the text-align style. This is a shorthand for calling the <see cref="HtmlTag.Style"/> method with the 'text-align' key
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="textAlign">The text alignment. This can be any valid css value for 'text-align'</param>
        /// <param name="replaceExisting">A value indicating whether the existing text-align, if any, should be overriden or not</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag TextAlign(this HtmlTag htmlTag, string textAlign, bool replaceExisting = true)
        {
            if (textAlign == null)
                throw new ArgumentNullException("textAlign");
            return htmlTag.Style("text-align", textAlign, replaceExisting);
        }

        /// <summary>
        ///     Sets the border style. This is a shorthand for calling the <see cref="HtmlTag.Style"/> method with the 'border' key
        /// </summary>
        /// <param name="htmlTag">This <see cref="HtmlTag"/></param>
        /// <param name="border">The border. This can be any valid css value for 'border'</param>
        /// <param name="replaceExisting">A value indicating whether the existing border, if any, should be overriden or not</param>
        /// <returns>This <see cref="HtmlTag"/></returns>
        public static HtmlTag Border(this HtmlTag htmlTag, string border, bool replaceExisting = true)
        {
            if (border == null)
                throw new ArgumentNullException("border");
            return htmlTag.Style("border", border, replaceExisting);
        } 
        
        #endregion
    }
}
