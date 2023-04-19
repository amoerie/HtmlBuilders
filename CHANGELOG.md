# Changelog

## 7.0.0
- Target .NET 7
- Add support for nullable annotations

## 6.0.0
 
- Migrate to .NET Core 3.1

## 5.0.0

- Make HtmlTag implement IHtmlContent. This introduces a lot of usability and performance improvements, at a pretty low cost.

## 3.0.1

- Expose HtmlTag.WithAttributes and HtmlTag.WithContents publically to enable full control on the consumer side. This might be necessary to keep supporting all use cases since the move to immutable HtmlTags.

## 3.0.0

- Migrate to .NET Standard 2.0. Includes a big breaking change: HtmlTag is now immutable! Also add methods to integrate with IHtmlContent.

## 2.0.6

- Added reference to System.Web to NuGet-Package to allow successful installation in empty-project.

## 2.0.5

- Updated NuGet package file to reflect downgraded minimum .NET MVC version.

## 2.0.4

- Downgraded required .NET MVC version from 5.2 to 4.0 to increase availability.

## 2.0.3

- Updated 'Merge' to include class, fix encoding issue with quotes in attributes.

## 2.0.1

- Built in release, had some errors during latest publish so this is mostly a republish just to be safe.
