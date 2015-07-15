using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

#region Update this region before creating a new release with 'BuildAndPushPackage.cmd'

// Versioning uses Semantic Versioning 2.0.0 (http://semver.org/): MAJOR.MINOR.PATCH.<UNUSED>

[assembly: AssemblyInformationalVersion("2.0.5")]
[assembly: AssemblyVersion("2.0.5.0")]
[assembly: AssemblyFileVersion("2.0.5.0")]
[assembly: AssemblyDescription( // put releaseNotes in here:
    "2.0.5: Updated NuGet package file to reflect downgraded minimum .NET MVC version.\n" +
    "2.0.4: Downgraded required .NET MVC version from 5.2 to 4.0 to increase availability.\n" +
    "2.0.3: Updated 'Merge' to include class, fix encoding issue with quotes in attributes.\n" +
    "2.0.1: Built in release, had some errors during latest publish so this is mostly a republish just to be safe.")]

#endregion

[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Alexander Moerman")]
[assembly: AssemblyProduct("HtmlBuilders")]
[assembly: AssemblyCopyright("Copyright ©  2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("d90bb1d0-6f1d-48db-b4a8-32d32a641ec7")]
[assembly: InternalsVisibleTo("HtmlBuilders.Tests")]