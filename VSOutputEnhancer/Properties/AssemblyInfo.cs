using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Output enhancer")]
[assembly: AssemblyDescription("Extension to add colors to Visual Studio output window.")]
[assembly: AssemblyCompany("Nikolay Balakin")]
[assembly: AssemblyProduct("Output enhancer")]
[assembly: AssemblyCopyright("Copyright © Nikolay Balakin 2015. All rights reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

[assembly: AssemblyVersion("0.4.*")]
[assembly: AssemblyFileVersion("0.4")]

[assembly: InternalsVisibleTo("Balakin.VSOutputEnhancer.UnitTests")]
[assembly: InternalsVisibleTo("Balakin.VSOutputEnhancer.Fakes")]
