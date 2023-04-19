using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Rendering;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace HtmlBuilders;

/// <summary>
///     Provides convenience properties to create instances of <see cref="HtmlTag" />
/// </summary>
public static class HtmlTags
{
    public static readonly HtmlTag A = new HtmlTag("a");

    public static readonly HtmlTag Abbr = new HtmlTag("abbr");

    public static readonly HtmlTag Address = new HtmlTag("address");

    public static readonly HtmlTag Area = new HtmlTag("area").Render(TagRenderMode.SelfClosing);

    public static readonly HtmlTag Article = new HtmlTag("article");

    public static readonly HtmlTag Aside = new HtmlTag("aside");

    public static readonly HtmlTag Audio = new HtmlTag("audio");

    public static readonly HtmlTag B = new HtmlTag("b");

    public static readonly HtmlTag Base = new HtmlTag("base").Render(TagRenderMode.SelfClosing);

    public static readonly HtmlTag Bdi = new HtmlTag("bdi");

    public static readonly HtmlTag Bdo = new HtmlTag("bdo");

    public static readonly HtmlTag BlockQuote = new HtmlTag("blockquote");

    public static readonly HtmlTag Body = new HtmlTag("body");

    public static readonly HtmlTag Br = new HtmlTag("br").Render(TagRenderMode.SelfClosing);

    public static readonly HtmlTag Button = new HtmlTag("button");

    public static readonly HtmlTag Canvas = new HtmlTag("canvas");

    public static readonly HtmlTag Caption = new HtmlTag("caption");

    public static readonly HtmlTag Cite = new HtmlTag("cite");

    public static readonly HtmlTag Code = new HtmlTag("code");

    public static readonly HtmlTag Col = new HtmlTag("col").Render(TagRenderMode.SelfClosing);

    public static readonly HtmlTag ColGroup = new HtmlTag("colgroup");

    public static readonly HtmlTag Data = new HtmlTag("data");

    public static readonly HtmlTag DataList = new HtmlTag("datalist");

    public static readonly HtmlTag Dd = new HtmlTag("dd");

    public static readonly HtmlTag Del = new HtmlTag("del");

    public static readonly HtmlTag Details = new HtmlTag("details");

    public static readonly HtmlTag Dfn = new HtmlTag("dfn");

    public static readonly HtmlTag Div = new HtmlTag("div");

    public static readonly HtmlTag Dl = new HtmlTag("dl");

    public static readonly HtmlTag Dt = new HtmlTag("dt");

    public static readonly HtmlTag Em = new HtmlTag("em");

    public static readonly HtmlTag Embed = new HtmlTag("embed").Render(TagRenderMode.SelfClosing);

    public static readonly HtmlTag Fieldset = new HtmlTag("fieldset");

    public static readonly HtmlTag FigCaption = new HtmlTag("figcaption");

    public static readonly HtmlTag Figure = new HtmlTag("figure");

    public static readonly HtmlTag Footer = new HtmlTag("footer");

    public static readonly HtmlTag Form = new HtmlTag("form");

    public static readonly HtmlTag H1 = new HtmlTag("h1");

    public static readonly HtmlTag H2 = new HtmlTag("h2");

    public static readonly HtmlTag H3 = new HtmlTag("h3");

    public static readonly HtmlTag H4 = new HtmlTag("h4");

    public static readonly HtmlTag H5 = new HtmlTag("h5");

    public static readonly HtmlTag H6 = new HtmlTag("h6");

    public static readonly HtmlTag Head = new HtmlTag("head");

    public static readonly HtmlTag Header = new HtmlTag("header");

    public static readonly HtmlTag Hr = new HtmlTag("hr").Render(TagRenderMode.SelfClosing);

    public static readonly HtmlTag Html = new HtmlTag("html");

    public static readonly HtmlTag I = new HtmlTag("i");

    public static readonly HtmlTag Iframe = new HtmlTag("iframe");

    public static readonly HtmlTag Img = new HtmlTag("img").Render(TagRenderMode.SelfClosing);

    public static readonly HtmlTag Ins = new HtmlTag("ins");

    public static readonly HtmlTag Kbd = new HtmlTag("kbd");

    public static readonly HtmlTag Keygen = new HtmlTag("keygen");

    public static readonly HtmlTag Label = new HtmlTag("label");

    public static readonly HtmlTag Legend = new HtmlTag("legend");

    public static readonly HtmlTag Li = new HtmlTag("li");

    public static readonly HtmlTag Link = new HtmlTag("link").Render(TagRenderMode.SelfClosing);

    public static readonly HtmlTag Main = new HtmlTag("main");

    public static readonly HtmlTag Map = new HtmlTag("map");

    public static readonly HtmlTag Mark = new HtmlTag("mark");

    public static readonly HtmlTag Menu = new HtmlTag("menu");

    public static readonly HtmlTag MenuItem = new HtmlTag("menuitem");

    public static readonly HtmlTag Meta = new HtmlTag("meta").Render(TagRenderMode.SelfClosing);

    public static readonly HtmlTag Meter = new HtmlTag("meter");

    public static readonly HtmlTag Nav = new HtmlTag("nav");

    public static readonly HtmlTag NoScript = new HtmlTag("noscript");

    [SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Wasn't my decision to name this HTML tag")]
    public static readonly HtmlTag Object = new HtmlTag("object");

    public static readonly HtmlTag Ol = new HtmlTag("ol");

    public static readonly HtmlTag OptGroup = new HtmlTag("optgroup");

    public static readonly HtmlTag Option = new HtmlTag("option");

    public static readonly HtmlTag Output = new HtmlTag("output");

    public static readonly HtmlTag P = new HtmlTag("p");

    public static readonly HtmlTag Param = new HtmlTag("param").Render(TagRenderMode.SelfClosing);

    public static readonly HtmlTag Pre = new HtmlTag("pre");

    public static readonly HtmlTag Progress = new HtmlTag("progress");

    public static readonly HtmlTag Q = new HtmlTag("q");

    public static readonly HtmlTag Rp = new HtmlTag("rp");

    public static readonly HtmlTag Rt = new HtmlTag("rt");

    public static readonly HtmlTag Ruby = new HtmlTag("ruby");

    public static readonly HtmlTag S = new HtmlTag("s");

    public static readonly HtmlTag Samp = new HtmlTag("samp");

    public static readonly HtmlTag Script = new HtmlTag("script");

    public static readonly HtmlTag Section = new HtmlTag("section");

    public static readonly HtmlTag Select = new HtmlTag("select");

    public static readonly HtmlTag Small = new HtmlTag("small");

    public static readonly HtmlTag Source = new HtmlTag("source").Render(TagRenderMode.SelfClosing);

    public static readonly HtmlTag Span = new HtmlTag("span");

    public static readonly HtmlTag Strong = new HtmlTag("strong");

    public static readonly HtmlTag Style = new HtmlTag("style");

    public static readonly HtmlTag Sub = new HtmlTag("sub");

    public static readonly HtmlTag Summary = new HtmlTag("summary");

    public static readonly HtmlTag Sup = new HtmlTag("sup");

    public static readonly HtmlTag Table = new HtmlTag("table");

    public static readonly HtmlTag Tbody = new HtmlTag("tbody");

    public static readonly HtmlTag Td = new HtmlTag("td");

    public static readonly HtmlTag Template = new HtmlTag("template");

    public static readonly HtmlTag Textarea = new HtmlTag("textarea");

    public static readonly HtmlTag Tfoot = new HtmlTag("tfoot");

    public static readonly HtmlTag Th = new HtmlTag("th");

    public static readonly HtmlTag Thead = new HtmlTag("thead");

    public static readonly HtmlTag Time = new HtmlTag("time");

    public static readonly HtmlTag Title = new HtmlTag("title");

    public static readonly HtmlTag Tr = new HtmlTag("tr");

    public static readonly HtmlTag Track = new HtmlTag("track").Render(TagRenderMode.SelfClosing);

    public static readonly HtmlTag U = new HtmlTag("u");

    public static readonly HtmlTag Ul = new HtmlTag("ul");

    public static readonly HtmlTag Var = new HtmlTag("var");

    public static readonly HtmlTag Video = new HtmlTag("video");

    public static readonly HtmlTag Wbr = new HtmlTag("wbr").Render(TagRenderMode.SelfClosing);

    public static class Input
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass", Justification = "This is a different kind of button")]
        public static readonly HtmlTag Button = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("button");

        public static readonly HtmlTag CheckBox = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("checkbox");

        public static readonly HtmlTag Color = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("color");

        public static readonly HtmlTag Date = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("date");

        public static readonly HtmlTag DateTime = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("datetime");

        public static readonly HtmlTag DateTimeLocal = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("datetime-local");

        public static readonly HtmlTag Email = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("email");

        public static readonly HtmlTag File = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("file");

        public static readonly HtmlTag Hidden = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("hidden");

        public static readonly HtmlTag Image = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("image");

        public static readonly HtmlTag Month = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("month");

        public static readonly HtmlTag Number = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("number");

        public static readonly HtmlTag Password = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("password");

        public static readonly HtmlTag Radio = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("radio");

        public static readonly HtmlTag Range = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("range");

        public static readonly HtmlTag Reset = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("reset");

        public static readonly HtmlTag Search = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("search");

        public static readonly HtmlTag Submit = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("submit");

        public static readonly HtmlTag Tel = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("tel");

        public static readonly HtmlTag Text = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("text");

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass", Justification = "This is a different kind of time")]
        public static readonly HtmlTag Time = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("time");

        public static readonly HtmlTag Url = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("url");

        public static readonly HtmlTag Week = new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("week");
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
