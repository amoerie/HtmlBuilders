HtmlBuilders
============

_C#'s TagBuilder on steroids_

***

Now available as a [Nuget Package](https://www.nuget.org/packages/HtmlBuilders/)!

***

# Summary

HtmlBuilders is a mini project that aims to simplify HTML creation, parsing and manipulating. The end result is shorter, better readable and more flexible code!
You could see it as a more advanced version of C#'s built-in TagBuilder.

## Crash course with code snippets

### Part 1: Creating HTML

What if we wanted to make this HTML in C#:

```html
	<div class="control-group">
		<div class="controls">
			<label class="checkbox">
				<input type="checkbox"> Remember me
			</label>
			<button type="submit" class="btn">Sign in</button>
		</div>
	</div>
```

Remember how you used to do this back in the days with the TagBuilder?

```c#
	var controlGroup = new TagBuilder("div");
	controlGroup.AddCssClass("control-group");
	var controls = new TagBuilder("div");
	controls.AddCssClass("controls");
	var label = new TagBuilder("label");
	label.AddCssClass("checkbox");
	var input = new TagBuilder("input");
	input.MergeAttribute("input", "checkbox");
	label.InnerHtml = input + " Remember me";
	var button = new TagBuilder("button");
	button.MergeAttribute("type", "submit");
	button.AddCssClass("btn");
	button.InnerHtml = "Sign in";
	controls.InnerHtml = label.ToString() + button;
	controlGroup.InnerHtml = controls.ToString();
```

Would you like some fluent syntax with that?

```c#
	var fluent = new HtmlTag("div").Class("control-group")
		.Append(new HtmlTag("div").Class("controls")
			.Append(new HtmlTag("label").Class("checkbox")
				.Append(new HtmlTag("input").Type("checkbox"))
				.Append(" Remember me")))
		.Append(new HtmlTag("button").Type("submit").Class("btn").Append("Sign in"));
```

Or you can just write the HTML

```c#
	var parsed =
		HtmlTag.Parse("<div class='control-group'>" +
					  "<div class='controls'>" +
					  "<label class='checkbox'><input type='checkbox'> Remember me</label>" +
					  "<button type='submit' class='btn'>Sign in</button>" +
					  "</div>" +
					  "</div>");
```

### Part 2: Manipulating the Html

What if you have this:
               
```html
	<label class="checkbox">
		<input type="checkbox" style="width:5px"> Remember me
	</label>
```

And you want to make this:
   
```html
	<label class="checkbox" for="remember">
		<input id="remember" style="width:5px;height:10px" type="checkbox"> Remember me
	</label>
```

_But here's the catch: you get the object as an incoming parameter in a method!_

Before: TagBuilder madness

```c#
	public TagBuilder AddSomeAttributes(TagBuilder label)
	{
		label.MergeAttribute("for", "remember");
		// is there even a better way?
		label.InnerHtml = label.InnerHtml.Replace("input", "input id='remember'");

		// if there is already a style attribute
		if (label.InnerHtml.IndexOf("style", StringComparison.Ordinal) != -1)
		{
			// meh, I give up
		}
		else
		{
			label.InnerHtml = label.InnerHtml.Replace("input", "input style='height:10px'");
		}
		return label;
	}
```

Now: HtmlTag glory

```c#
	public HtmlTag AddSomeAttributes(HtmlTag label)
	{
		label.Attribute("for", "remember");
		// LINQ search
		var input = label.Children.Single(c => c.TagName.Equals("input") && c.HasAttribute("type") && c["type"].Equals("checkbox"));
		// Immediate access to styles without having to worry about existing styles, correct formatting, etc.
		input.Id("remember").Style("height", "10px");
		// We don't need to update label, any changes made to the input will automatically affect the HTML rendered by label
		return label;
	}
```

# It's tested! 

There's an extensive suite of about 100 unit tests making sure I haven't forgotten anything. 
For those of you who get a kick out of juicy syntax and unit tests, here are some snippets from the tests:

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
	
	// support for anonymous objects too, like what you're used to from the MVC html helpers. Attributes will be automatically prefixed with data-
	new HtmlTag("div").Data(new { test = "data test", test2 = "data test 2", test3 = "data test 3" });
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



