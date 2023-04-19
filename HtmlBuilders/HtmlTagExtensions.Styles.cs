using System;

namespace HtmlBuilders;

public static partial class HtmlTagExtensions
{
    /// <summary>
    ///     Sets the width style. This is a shorthand for calling the <see cref="HtmlTag.Style" /> method with the 'width' key
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="width">The width. This can be any valid css value for 'width'</param>
    /// <param name="replaceExisting">A value indicating whether the existing width, if any, should be overriden or not</param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Width(this HtmlTag htmlTag, string width, bool replaceExisting = true)
    {
        ArgumentNullException.ThrowIfNull(width);

        return htmlTag.Style("width", width, replaceExisting);
    }

    /// <summary>
    ///     Sets the height style. This is a shorthand for calling the <see cref="HtmlTag.Style" /> method with the 'height' key
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="height">The height. This can be any valid css value for 'height'</param>
    /// <param name="replaceExisting">A value indicating whether the existing height, if any, should be overriden or not</param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Height(this HtmlTag htmlTag, string height, bool replaceExisting = true)
    {
        ArgumentNullException.ThrowIfNull(height);

        return htmlTag.Style("height", height, replaceExisting);
    }

    /// <summary>
    ///     Sets the margin style. This is a shorthand for calling the <see cref="HtmlTag.Style" /> method with the 'margin' key
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="margin">The margin. This can be any valid css value for 'margin'</param>
    /// <param name="replaceExisting">A value indicating whether the existing margin, if any, should be overriden or not</param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Margin(this HtmlTag htmlTag, string margin, bool replaceExisting = true)
    {
        ArgumentNullException.ThrowIfNull(margin);

        return htmlTag.Style("margin", margin, replaceExisting);
    }

    /// <summary>
    ///     Sets the padding style. This is a shorthand for calling the <see cref="HtmlTag.Style" /> method with the 'padding'
    ///     key
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="padding">The padding. This can be any valid css value for 'padding'</param>
    /// <param name="replaceExisting">A value indicating whether the existing padding, if any, should be overriden or not</param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Padding(this HtmlTag htmlTag, string padding, bool replaceExisting = true)
    {
        ArgumentNullException.ThrowIfNull(padding);

        return htmlTag.Style("padding", padding, replaceExisting);
    }

    /// <summary>
    ///     Sets the color style. This is a shorthand for calling the <see cref="HtmlTag.Style" /> method with the 'color' key
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="color">The color. This can be any valid css value for 'color'</param>
    /// <param name="replaceExisting">A value indicating whether the existing color, if any, should be overriden or not</param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Color(this HtmlTag htmlTag, string color, bool replaceExisting = true)
    {
        ArgumentNullException.ThrowIfNull(color);

        return htmlTag.Style("color", color, replaceExisting);
    }

    /// <summary>
    ///     Sets the text-align style. This is a shorthand for calling the <see cref="HtmlTag.Style" /> method with the
    ///     'text-align' key
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="textAlign">The text alignment. This can be any valid css value for 'text-align'</param>
    /// <param name="replaceExisting">A value indicating whether the existing text-align, if any, should be overriden or not</param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag TextAlign(this HtmlTag htmlTag, string textAlign, bool replaceExisting = true)
    {
        ArgumentNullException.ThrowIfNull(textAlign);

        return htmlTag.Style("text-align", textAlign, replaceExisting);
    }

    /// <summary>
    ///     Sets the border style. This is a shorthand for calling the <see cref="HtmlTag.Style" /> method with the 'border' key
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="border">The border. This can be any valid css value for 'border'</param>
    /// <param name="replaceExisting">A value indicating whether the existing border, if any, should be overriden or not</param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Border(this HtmlTag htmlTag, string border, bool replaceExisting = true)
    {
        ArgumentNullException.ThrowIfNull(border);

        return htmlTag.Style("border", border, replaceExisting);
    }
}
