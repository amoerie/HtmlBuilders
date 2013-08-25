HtmlBuilders
============

C#'s TagBuilder on steroids


TagBuilder was definitely a good effort on Microsoft's part, but there are a few things that always irked me about it:
- No support for DOM traversal. You have a tag and its InnerHtml, that's it.
- No builder-style construction. Making a tag with a few attributes and a few children quickly resulted into tens of lines of code.
- You can render TagBuilders to a string, but you can't load a TagBuilder *from* a string. 

So, in a weekend or so, I whipped up the 'HtmlTag' class.

A quick glance at what it can do:

```c#
            // parsing from a string!
            var div = HtmlTag.Parse("<div><a href='testhref'></a><img src='testsrc'/></div>");
            Assert.That(div.TagName, Is.EqualTo("div"));
            // Children is an IEnumerable<HtmlTag>
            Assert.That(div.Children.Count(), Is.EqualTo(2));
            
            var a = div.Children.First();
            Assert.That(a.TagName, Is.EqualTo("a"));
            Assert.That(a.HasAttribute("href"), Is.True);
            Assert.That(a["href"], Is.EqualTo("testhref"));

            var img = div.Children.Last();
            Assert.That(img.TagName, Is.EqualTo("img"));
            Assert.That(img.HasAttribute("src"), Is.True);
            Assert.That(img["src"], Is.EqualTo("testsrc"));
```

The API is also quite a bit richer than what you're used to from the TagBuilder, how about direct access to the style attribute:

```c#
            var div = new HtmlTag("div").Style("width", "10px").Style("height", "15px");
            Assert.That(div.HasAttribute("style"), Is.True);
            // Styles is an IReadonlyDictionary<string, string> with a getter and setter
            Assert.That(div.Styles.Count, Is.EqualTo(2));
            Assert.That(div.Styles.ContainsKey("width"), Is.True);
            Assert.That(div.Styles["width"], Is.EqualTo("10px"));
            Assert.That(div.Styles.ContainsKey("height"), Is.True);
            Assert.That(div.Styles["height"], Is.EqualTo("15px"));
```

There's also some extra support for data attributes, which I find myself using a lot recently

```c#
            // results in <div data-test="datatest"></div>
            new HtmlTag("div").Data("test", "datatest");
            
            // support for anonymous objects too, like what you're used to from the MVC html helpers
            new HtmlTag("div").Data(new { data_test = "data test", data_test2 = "data test 2", data_test3 = "data test 3" });
```

Have I mentioned that HtmlTag implements IDictionary<string,string>? Thats right, every HtmlTag can be used as a dictionary to manipulate
its attributes. It also allows for some language sugar:

```c#
            var div = new HtmlTag("div") {{"name", "div-name"}, {"id", "div-id"}};
            Assert.That(div.HasAttribute("name"));
            Assert.That(div.HasAttribute("id"));
            Assert.That(div["name"], Is.EqualTo("div-name"));
            Assert.That(div["id"], Is.EqualTo("div-id"));
```

One more thing: it's tested! There's about 88 unit tests, which provide some 85% code coverage, and I intend to add more in the foreseeable future.

![Unit tests](http://i.imgur.com/ZrD8A92.png)



