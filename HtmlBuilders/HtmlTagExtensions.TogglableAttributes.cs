namespace HtmlBuilders;

public static partial class HtmlTagExtensions
{
    /// <summary>
    ///     Sets the 'checked' attribute to 'checked' if <paramref name="checked" /> is true or removes the attribute if
    ///     <paramref name="checked" /> is false
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="checked">
    ///     A value indicating whether this tag should have the attribute 'checked' with value 'checked' or
    ///     not.
    /// </param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Checked(this HtmlTag htmlTag, bool @checked) => htmlTag.ToggleAttribute("checked", @checked);

    /// <summary>
    ///     Sets the 'disabled' attribute to 'disabled' if <paramref name="disabled" /> is true or removes the attribute if
    ///     <paramref name="disabled" /> is false
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="disabled">
    ///     A value indicating whether this tag should have the attribute 'disabled' with value 'disabled'
    ///     or not.
    /// </param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Disabled(this HtmlTag htmlTag, bool disabled) => htmlTag.ToggleAttribute("disabled", disabled);

    /// <summary>
    ///     Sets the 'selected' attribute to 'selected' if <paramref name="selected" /> is true or removes the attribute if
    ///     <paramref name="selected" /> is false
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="selected">
    ///     A value indicating whether this tag should have the attribute 'selected' with value 'selected'
    ///     or not.
    /// </param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Selected(this HtmlTag htmlTag, bool selected) => htmlTag.ToggleAttribute("selected", selected);

    /// <summary>
    ///     Sets the 'readonly' attribute to 'readonly' if <paramref name="readonly" /> is true or removes the attribute if
    ///     <paramref name="readonly" /> is false
    /// </summary>
    /// <param name="htmlTag">This <see cref="HtmlTag" /></param>
    /// <param name="readonly">
    ///     A value indicating whether this tag should have the attribute 'readonly' with value 'readonly'
    ///     or not.
    /// </param>
    /// <returns>This <see cref="HtmlTag" /></returns>
    public static HtmlTag Readonly(this HtmlTag htmlTag, bool @readonly) => htmlTag.ToggleAttribute("readonly", @readonly);
}
