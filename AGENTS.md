# HtmlBuilders — Agent Guide

## Project overview

**HtmlBuilders** is a C# library that provides `HtmlTag`, a fluent, immutable drop-in replacement for ASP.NET Core's `TagBuilder`. It simplifies creating, parsing, and manipulating HTML in server-side C# code.

NuGet package: `HtmlBuilders` — targets .NET 8.0, .NET 9.0, and .NET 10.0.

## Repository layout

| Path | Purpose |
|---|---|
| `HtmlBuilders/` | Library source (the published NuGet package) |
| `HtmlBuilders.Tests/` | xUnit v3 test suite |
| `CHANGELOG.md` | Version history |
| `publish.ps1` | NuGet publish script |
| `Directory.Build.props` / `Directory.Packages.props` | Shared MSBuild and package version configuration |

## Build and test

```bash
# Restore tools (CSharpier formatter)
dotnet tool restore

# Check formatting
dotnet csharpier check .

# Build
dotnet build --configuration Release

# Run tests
dotnet test --configuration Release --verbosity normal
```

CI runs on `ubuntu-latest` and executes all four steps above. PRs must pass formatting, build, and tests before merging.

## Code style

- Formatting is enforced by **CSharpier** — run `dotnet csharpier .` to auto-format before committing.
- Nullable reference types are enabled.
- The SDK is `Microsoft.NET.Sdk.Web` (required for `IHtmlContent` integration).

## Key source files

| File | Role |
|---|---|
| `HtmlBuilders/HtmlTag.cs` | Core type — immutable HTML element with fluent builder API |
| `HtmlBuilders/HtmlTags.cs` | Static helpers for all standard HTML elements (e.g. `HtmlTags.Div`, `HtmlTags.Input.Checkbox`) |
| `HtmlBuilders/IHtmlElement.cs` | Shared interface implemented by `HtmlTag` and `HtmlText` |
| `HtmlBuilders/HtmlText.cs` | Represents a text node |
| `HtmlBuilders/HtmlTagExtensions.*.cs` | Extension methods grouped by concern (attribute shorthands, styles, text, togglable attributes) |
| `HtmlBuilders/IHtmlContentExtensions.ToHtmlTag.cs` | Converts `IHtmlContent` to `HtmlTag` via HtmlAgilityPack parsing |
| `HtmlBuilders/AttributesComparer.cs` | Equality comparer for HTML attribute dictionaries |

## Key design points

- `HtmlTag` is **immutable** — every method returns a new instance. Do not look for in-place mutation methods.
- `HtmlTag` implements `IHtmlContent`, so it integrates directly with ASP.NET Core Razor views.
- HTML parsing (e.g. `HtmlTag.Parse(string html)`) is backed by **HtmlAgilityPack**.
- Attribute and CSS class manipulation is handled through the fluent API: `.Attribute()`, `.Class()`, `.Style()`, `.Data()`, etc.

## Making changes

1. Create a feature branch from `main`.
2. Edit source under `HtmlBuilders/`.
3. Add or update tests in `HtmlBuilders.Tests/` for every behavioural change.
4. Run `dotnet csharpier .` then `dotnet build --configuration Release` then `dotnet test --configuration Release`.
5. Update `CHANGELOG.md` with the change and today's date.
6. Open a PR against `main`.

## Testing notes

- Tests are written with **xUnit v3**.
- Test files mirror the source file they cover (e.g. `HtmlTagTests.cs` tests `HtmlTag.cs`).
- Code coverage is collected via Coverlet and reported to Codecov on pushes to `main`.
- To run a single test class: `dotnet test --filter "FullyQualifiedName~HtmlTagTests"`.
