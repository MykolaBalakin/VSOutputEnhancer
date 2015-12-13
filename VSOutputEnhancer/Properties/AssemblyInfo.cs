using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Output enhancer")]
[assembly: AssemblyDescription("Extension to add colors to Visual Studio output window.")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

[assembly: InternalsVisibleTo("Balakin.VSOutputEnhancer.Tests.UnitTests")]
[assembly: InternalsVisibleTo("Balakin.VSOutputEnhancer.TestsBase")]
[assembly: InternalsVisibleTo("Balakin.VSOutputEnhancer.Fakes")]
