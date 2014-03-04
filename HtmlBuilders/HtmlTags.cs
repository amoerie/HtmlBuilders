using System.Web.Mvc;

namespace HtmlBuilders
{
    /// <summary>
    /// Provides convenience properties to create instances of <see cref="HtmlTag"/>
    /// </summary>
    public static class HtmlTags
    {
        public static HtmlTag A                 { get { return new HtmlTag("a"); } }
        public static HtmlTag Abbr              { get { return new HtmlTag("abbr"); } }
        public static HtmlTag Address           { get { return new HtmlTag("address"); } }
        public static HtmlTag Area              { get { return new HtmlTag("area").Render(TagRenderMode.SelfClosing); } }
        public static HtmlTag Article           { get { return new HtmlTag("article"); } }
        public static HtmlTag Aside             { get { return new HtmlTag("aside"); } }
        public static HtmlTag Audio             { get { return new HtmlTag("audio"); } }

        public static HtmlTag B                 { get { return new HtmlTag("b"); } }
        public static HtmlTag Base              { get { return new HtmlTag("base").Render(TagRenderMode.SelfClosing);; } }
        public static HtmlTag Bdi               { get { return new HtmlTag("bdi"); } }
        public static HtmlTag Bdo               { get { return new HtmlTag("bdo"); } }
        public static HtmlTag BlockQuote        { get { return new HtmlTag("blockquote"); } }
        public static HtmlTag Body              { get { return new HtmlTag("body"); } }
        public static HtmlTag Br                { get { return new HtmlTag("br").Render(TagRenderMode.SelfClosing);; } }
        public static HtmlTag Button            { get { return new HtmlTag("button"); } }

        public static HtmlTag Canvas            { get { return new HtmlTag("canvas"); } }
        public static HtmlTag Caption           { get { return new HtmlTag("caption"); } }
        public static HtmlTag Cite              { get { return new HtmlTag("cite"); } }
        public static HtmlTag Code              { get { return new HtmlTag("code"); } }
        public static HtmlTag Col               { get { return new HtmlTag("col").Render(TagRenderMode.SelfClosing);; } }
        public static HtmlTag ColGroup          { get { return new HtmlTag("colgroup"); } }

        public static HtmlTag Data              { get { return new HtmlTag("data"); } }
        public static HtmlTag DataList          { get { return new HtmlTag("datalist"); } }
        public static HtmlTag Dd                { get { return new HtmlTag("dd"); } }
        public static HtmlTag Del               { get { return new HtmlTag("del"); } }
        public static HtmlTag Details           { get { return new HtmlTag("details"); } }
        public static HtmlTag Dfn               { get { return new HtmlTag("dfn"); } }
        public static HtmlTag Div               { get { return new HtmlTag("div"); } }
        public static HtmlTag Dl                { get { return new HtmlTag("dl"); } }
        public static HtmlTag Dt                { get { return new HtmlTag("dt"); } }

        public static HtmlTag Em                { get { return new HtmlTag("em"); } }
        public static HtmlTag Embed             { get { return new HtmlTag("embed").Render(TagRenderMode.SelfClosing);; } }

        public static HtmlTag Fieldset          { get { return new HtmlTag("fieldset"); } }
        public static HtmlTag FigCaption        { get { return new HtmlTag("figcaption"); } }
        public static HtmlTag Figure            { get { return new HtmlTag("figure"); } }
        public static HtmlTag Footer            { get { return new HtmlTag("footer"); } }
        public static HtmlTag Form              { get { return new HtmlTag("form"); } }

        public static HtmlTag H1                { get { return new HtmlTag("h1"); } }
        public static HtmlTag H2                { get { return new HtmlTag("h2"); } }
        public static HtmlTag H3                { get { return new HtmlTag("h3"); } }
        public static HtmlTag H4                { get { return new HtmlTag("h4"); } }
        public static HtmlTag H5                { get { return new HtmlTag("h5"); } }
        public static HtmlTag H6                { get { return new HtmlTag("h6"); } }
        public static HtmlTag Head              { get { return new HtmlTag("head"); } }
        public static HtmlTag Header            { get { return new HtmlTag("header"); } }
        public static HtmlTag Hr                { get { return new HtmlTag("hr").Render(TagRenderMode.SelfClosing);; } }
        public static HtmlTag Html              { get { return new HtmlTag("html"); } }

        public static HtmlTag I                 { get { return new HtmlTag("i"); } }
        public static HtmlTag Iframe            { get { return new HtmlTag("iframe"); } }
        public static HtmlTag Img               { get { return new HtmlTag("img").Render(TagRenderMode.SelfClosing);; } }
        public static class Input
        {
            public static HtmlTag Button        { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("button"); } }
            public static HtmlTag CheckBox      { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("checkbox"); } }
            public static HtmlTag Color         { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("color"); } }
            public static HtmlTag Date          { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("date"); } }
            public static HtmlTag DateTime      { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("datetime"); } }
            public static HtmlTag DateTimeLocal { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("datetime-local"); } }
            public static HtmlTag Email         { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("email"); } }
            public static HtmlTag File          { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("file"); } }
            public static HtmlTag Hidden        { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("hidden"); } }
            public static HtmlTag Image         { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("image"); } }
            public static HtmlTag Month         { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("month"); } }
            public static HtmlTag Number        { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("number"); } }
            public static HtmlTag Password      { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("password"); } }
            public static HtmlTag Radio         { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("radio"); } }
            public static HtmlTag Range         { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("range"); } }
            public static HtmlTag Reset         { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("reset"); } }
            public static HtmlTag Search        { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("search"); } }
            public static HtmlTag Submit        { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("submit"); } }
            public static HtmlTag Tel           { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("tel"); } }
            public static HtmlTag Text          { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("text"); } }
            public static HtmlTag Time          { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("time"); } }
            public static HtmlTag Url           { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("url"); } }
            public static HtmlTag Week          { get { return new HtmlTag("input").Render(TagRenderMode.SelfClosing).Type("week"); } }
        }
        public static HtmlTag Ins               { get { return new HtmlTag("ins"); } }

        public static HtmlTag Kbd               { get { return new HtmlTag("kbd"); } }
        public static HtmlTag Keygen            { get { return new HtmlTag("keygen"); } }

        public static HtmlTag Label             { get { return new HtmlTag("label"); } }
        public static HtmlTag Legend            { get { return new HtmlTag("legend"); } }
        public static HtmlTag Li                { get { return new HtmlTag("li"); } }
        public static HtmlTag Link              { get { return new HtmlTag("link").Render(TagRenderMode.SelfClosing); } }

        public static HtmlTag Main              { get { return new HtmlTag("main"); } }
        public static HtmlTag Map               { get { return new HtmlTag("map"); } }
        public static HtmlTag Mark              { get { return new HtmlTag("mark"); } }
        public static HtmlTag Menu              { get { return new HtmlTag("menu"); } }
        public static HtmlTag MenuItem          { get { return new HtmlTag("menuitem"); } }
        public static HtmlTag Meta              { get { return new HtmlTag("meta").Render(TagRenderMode.SelfClosing); } }
        public static HtmlTag Meter             { get { return new HtmlTag("meter"); } }

        public static HtmlTag Nav               { get { return new HtmlTag("nav"); } }
        public static HtmlTag NoScript          { get { return new HtmlTag("noscript"); } }

        public static HtmlTag Object            { get { return new HtmlTag("object"); } }
        public static HtmlTag Ol                { get { return new HtmlTag("ol"); } }
        public static HtmlTag OptGroup          { get { return new HtmlTag("optgroup"); } }
        public static HtmlTag Option            { get { return new HtmlTag("option"); } }
        public static HtmlTag Output            { get { return new HtmlTag("output"); } }

        public static HtmlTag P                 { get { return new HtmlTag("p"); } }
        public static HtmlTag Param             { get { return new HtmlTag("param").Render(TagRenderMode.SelfClosing); } }
        public static HtmlTag Pre               { get { return new HtmlTag("pre"); } }
        public static HtmlTag Progress          { get { return new HtmlTag("progress"); } }

        public static HtmlTag Q                 { get { return new HtmlTag("q"); } }

        public static HtmlTag Rp                { get { return new HtmlTag("rp"); } }
        public static HtmlTag Rt                { get { return new HtmlTag("rt"); } }
        public static HtmlTag Ruby              { get { return new HtmlTag("ruby"); } }

        public static HtmlTag S                 { get { return new HtmlTag("s"); } }
        public static HtmlTag Samp              { get { return new HtmlTag("samp"); } }
        public static HtmlTag Script            { get { return new HtmlTag("script"); } }
        public static HtmlTag Section           { get { return new HtmlTag("section"); } }
        public static HtmlTag Select            { get { return new HtmlTag("select"); } }
        public static HtmlTag Small             { get { return new HtmlTag("small"); } }
        public static HtmlTag Source            { get { return new HtmlTag("source").Render(TagRenderMode.SelfClosing); } }
        public static HtmlTag Span              { get { return new HtmlTag("span"); } }
        public static HtmlTag Strong            { get { return new HtmlTag("strong"); } }
        public static HtmlTag Style             { get { return new HtmlTag("style"); } }
        public static HtmlTag Sub               { get { return new HtmlTag("sub"); } }
        public static HtmlTag Summary           { get { return new HtmlTag("summary"); } }
        public static HtmlTag Sup               { get { return new HtmlTag("sup"); } }

        public static HtmlTag Table             { get { return new HtmlTag("table"); } }
        public static HtmlTag Tbody             { get { return new HtmlTag("tbody"); } }
        public static HtmlTag Td                { get { return new HtmlTag("td"); } }
        public static HtmlTag Template          { get { return new HtmlTag("template"); } }
        public static HtmlTag Textarea          { get { return new HtmlTag("textarea"); } }
        public static HtmlTag Tfoot             { get { return new HtmlTag("tfoot"); } }
        public static HtmlTag Th                { get { return new HtmlTag("th"); } }
        public static HtmlTag Thead             { get { return new HtmlTag("thead"); } }
        public static HtmlTag Time              { get { return new HtmlTag("time"); } }
        public static HtmlTag Title             { get { return new HtmlTag("title"); } }
        public static HtmlTag Tr                { get { return new HtmlTag("tr"); } }
        public static HtmlTag Track             { get { return new HtmlTag("track").Render(TagRenderMode.SelfClosing); } }

        public static HtmlTag U                 { get { return new HtmlTag("u"); } }
        public static HtmlTag Ul                { get { return new HtmlTag("ul"); } }

        public static HtmlTag Var               { get { return new HtmlTag("var"); } }
        public static HtmlTag Video             { get { return new HtmlTag("video"); } }

        public static HtmlTag Wbr               { get { return new HtmlTag("wbr").Render(TagRenderMode.SelfClosing); } }
    }
}
