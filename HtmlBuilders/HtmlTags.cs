using Microsoft.AspNetCore.Mvc.Rendering;

namespace HtmlBuilders {
  /// <summary>
  ///   Provides convenience properties to create instances of <see cref="HtmlTag" />
  /// </summary>
  public static class HtmlTags {
    public static HtmlTag A => new HtmlTag("a");

    public static HtmlTag Abbr => new HtmlTag("abbr");

    public static HtmlTag Address => new HtmlTag("address");

    public static HtmlTag Area => new HtmlTag("area").Render(TagRenderMode.SelfClosing);

    public static HtmlTag Article => new HtmlTag("article");

    public static HtmlTag Aside => new HtmlTag("aside");

    public static HtmlTag Audio => new HtmlTag("audio");

    public static HtmlTag B => new HtmlTag("b");

    public static HtmlTag Base => new HtmlTag("base").Render(TagRenderMode.SelfClosing);

    public static HtmlTag Bdi => new HtmlTag("bdi");

    public static HtmlTag Bdo => new HtmlTag("bdo");

    public static HtmlTag BlockQuote => new HtmlTag("blockquote");

    public static HtmlTag Body => new HtmlTag("body");

    public static HtmlTag Br => new HtmlTag("br").Render(TagRenderMode.SelfClosing);

    public static HtmlTag Button => new HtmlTag("button");

    public static HtmlTag Canvas => new HtmlTag("canvas");

    public static HtmlTag Caption => new HtmlTag("caption");

    public static HtmlTag Cite => new HtmlTag("cite");

    public static HtmlTag Code => new HtmlTag("code");

    public static HtmlTag Col => new HtmlTag("col").Render(TagRenderMode.SelfClosing);

    public static HtmlTag ColGroup => new HtmlTag("colgroup");

    public static HtmlTag Data => new HtmlTag("data");

    public static HtmlTag DataList => new HtmlTag("datalist");

    public static HtmlTag Dd => new HtmlTag("dd");

    public static HtmlTag Del => new HtmlTag("del");

    public static HtmlTag Details => new HtmlTag("details");

    public static HtmlTag Dfn => new HtmlTag("dfn");

    public static HtmlTag Div => new HtmlTag("div");

    public static HtmlTag Dl => new HtmlTag("dl");

    public static HtmlTag Dt => new HtmlTag("dt");

    public static HtmlTag Em => new HtmlTag("em");

    public static HtmlTag Embed => new HtmlTag("embed").Render(TagRenderMode.SelfClosing);

    public static HtmlTag Fieldset => new HtmlTag("fieldset");

    public static HtmlTag FigCaption => new HtmlTag("figcaption");

    public static HtmlTag Figure => new HtmlTag("figure");

    public static HtmlTag Footer => new HtmlTag("footer");

    public static HtmlTag Form => new HtmlTag("form");

    public static HtmlTag H1 => new HtmlTag("h1");

    public static HtmlTag H2 => new HtmlTag("h2");

    public static HtmlTag H3 => new HtmlTag("h3");

    public static HtmlTag H4 => new HtmlTag("h4");

    public static HtmlTag H5 => new HtmlTag("h5");

    public static HtmlTag H6 => new HtmlTag("h6");

    public static HtmlTag Head => new HtmlTag("head");

    public static HtmlTag Header => new HtmlTag("header");

    public static HtmlTag Hr => new HtmlTag("hr").Render(TagRenderMode.SelfClosing);

    public static HtmlTag Html => new HtmlTag("html");

    public static HtmlTag I => new HtmlTag("i");

    public static HtmlTag Iframe => new HtmlTag("iframe");

    public static HtmlTag Img => new HtmlTag("img").Render(TagRenderMode.SelfClosing);

    public static HtmlTag Ins => new HtmlTag("ins");

    public static HtmlTag Kbd => new HtmlTag("kbd");

    public static HtmlTag Keygen => new HtmlTag("keygen");

    public static HtmlTag Label => new HtmlTag("label");

    public static HtmlTag Legend => new HtmlTag("legend");

    public static HtmlTag Li => new HtmlTag("li");

    public static HtmlTag Link => new HtmlTag("link").Render(TagRenderMode.SelfClosing);

    public static HtmlTag Main => new HtmlTag("main");

    public static HtmlTag Map => new HtmlTag("map");

    public static HtmlTag Mark => new HtmlTag("mark");

    public static HtmlTag Menu => new HtmlTag("menu");

    public static HtmlTag MenuItem => new HtmlTag("menuitem");

    public static HtmlTag Meta => new HtmlTag("meta").Render(TagRenderMode.SelfClosing);

    public static HtmlTag Meter => new HtmlTag("meter");

    public static HtmlTag Nav => new HtmlTag("nav");

    public static HtmlTag NoScript => new HtmlTag("noscript");

    public static HtmlTag Object => new HtmlTag("object");

    public static HtmlTag Ol => new HtmlTag("ol");

    public static HtmlTag OptGroup => new HtmlTag("optgroup");

    public static HtmlTag Option => new HtmlTag("option");

    public static HtmlTag Output => new HtmlTag("output");

    public static HtmlTag P => new HtmlTag("p");

    public static HtmlTag Param => new HtmlTag("param").Render(TagRenderMode.SelfClosing);

    public static HtmlTag Pre => new HtmlTag("pre");

    public static HtmlTag Progress => new HtmlTag("progress");

    public static HtmlTag Q => new HtmlTag("q");

    public static HtmlTag Rp => new HtmlTag("rp");

    public static HtmlTag Rt => new HtmlTag("rt");

    public static HtmlTag Ruby => new HtmlTag("ruby");

    public static HtmlTag S => new HtmlTag("s");

    public static HtmlTag Samp => new HtmlTag("samp");

    public static HtmlTag Script => new HtmlTag("script");

    public static HtmlTag Section => new HtmlTag("section");

    public static HtmlTag Select => new HtmlTag("select");

    public static HtmlTag Small => new HtmlTag("small");

    public static HtmlTag Source => new HtmlTag("source").Render(TagRenderMode.SelfClosing);

    public static HtmlTag Span => new HtmlTag("span");

    public static HtmlTag Strong => new HtmlTag("strong");

    public static HtmlTag Style => new HtmlTag("style");

    public static HtmlTag Sub => new HtmlTag("sub");

    public static HtmlTag Summary => new HtmlTag("summary");

    public static HtmlTag Sup => new HtmlTag("sup");

    public static HtmlTag Table => new HtmlTag("table");

    public static HtmlTag Tbody => new HtmlTag("tbody");

    public static HtmlTag Td => new HtmlTag("td");

    public static HtmlTag Template => new HtmlTag("template");

    public static HtmlTag Textarea => new HtmlTag("textarea");

    public static HtmlTag Tfoot => new HtmlTag("tfoot");

    public static HtmlTag Th => new HtmlTag("th");

    public static HtmlTag Thead => new HtmlTag("thead");

    public static HtmlTag Time => new HtmlTag("time");

    public static HtmlTag Title => new HtmlTag("title");

    public static HtmlTag Tr => new HtmlTag("tr");

    public static HtmlTag Track => new HtmlTag("track").Render(TagRenderMode.SelfClosing);

    public static HtmlTag U => new HtmlTag("u");

    public static HtmlTag Ul => new HtmlTag("ul");

    public static HtmlTag Var => new HtmlTag("var");

    public static HtmlTag Video => new HtmlTag("video");

    public static HtmlTag Wbr => new HtmlTag("wbr").Render(TagRenderMode.SelfClosing);

    public static class Input {
      public static HtmlTag Button => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("button");

      public static HtmlTag CheckBox => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("checkbox");

      public static HtmlTag Color => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("color");

      public static HtmlTag Date => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("date");

      public static HtmlTag DateTime => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("datetime");

      public static HtmlTag DateTimeLocal => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("datetime-local");

      public static HtmlTag Email => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("email");

      public static HtmlTag File => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("file");

      public static HtmlTag Hidden => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("hidden");

      public static HtmlTag Image => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("image");

      public static HtmlTag Month => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("month");

      public static HtmlTag Number => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("number");

      public static HtmlTag Password => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("password");

      public static HtmlTag Radio => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("radio");

      public static HtmlTag Range => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("range");

      public static HtmlTag Reset => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("reset");

      public static HtmlTag Search => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("search");

      public static HtmlTag Submit => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("submit");

      public static HtmlTag Tel => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("tel");

      public static HtmlTag Text => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("text");

      public static HtmlTag Time => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("time");

      public static HtmlTag Url => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("url");

      public static HtmlTag Week => new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("week");
    }
  }
}