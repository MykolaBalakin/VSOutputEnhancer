using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic;

namespace Balakin.VSOutputEnhancer.IntegrationTests.TestCases
{
    [ExcludeFromCodeCoverage]
    public class DebugOutput : ITestCase
    {
        public String ContentType { get; } = Logic.ContentType.DebugOutput;

        public IReadOnlyList<String> SourceText { get; } = new[]
        {
            "'ConsoleDemo.exe' (CLR v4.0.30319: DefaultDomain): Loaded 'C:\\GAC_32\\mscorlib\\v4.0_4.0.0.0__b77a5c561934e089\\mscorlib.dll'. Skipped loading symbols.\r\n",
            "'ConsoleDemo.exe' (CLR v4.0.30319: DefaultDomain): Loaded 'C:\\test\\ConsoleDemo\\bin\\Debug\\ConsoleDemo.exe'. Symbols loaded.\r\n",
            "'ConsoleDemo.exe' (CLR v4.0.30319: ConsoleDemo.exe): Loaded 'C:\\GAC_MSIL\\System\\v4.0_4.0.0.0__b77a5c561934e089\\System.dll'. Skipped loading symbols.\r\n",
            "'ConsoleDemo.exe' (CLR v4.0.30319: ConsoleDemo.exe): Loaded 'C:\\GAC_MSIL\\System.Configuration\\v4.0_4.0.0.0__b03f5f7f11d50a3a\\System.Configuration.dll'. Skipped loading symbols.\r\n",
            "'ConsoleDemo.exe' (CLR v4.0.30319: ConsoleDemo.exe): Loaded 'C:\\GAC_MSIL\\System.Core\\v4.0_4.0.0.0__b77a5c561934e089\\System.Core.dll'. Skipped loading symbols.\r\n",
            "'ConsoleDemo.exe' (CLR v4.0.30319: ConsoleDemo.exe): Loaded 'C:\\GAC_MSIL\\System.Xml\\v4.0_4.0.0.0__b77a5c561934e089\\System.Xml.dll'. Skipped loading symbols.\r\n",
            "ConsoleDemo.exe Error: 0 : Trace error message\r\n",
            "ConsoleDemo.exe Warning: 0 : Trace warning message\r\n",
            "ConsoleDemo.exe Information: 0 : Trace information message\r\n",
            "Exception thrown: 'System.Exception' in ConsoleDemo.exe\r\n",
            "The program '[9756] ConsoleDemo.exe' has exited with code 0 (0x0).\r\n"
        };

        public IReadOnlyList<ClassifiedText> ExpectedResult { get; } = new[]
        {
            new ClassifiedText(ClassificationType.DebugTraceError, "Error: 0 : Trace error message"),
            new ClassifiedText(ClassificationType.DebugTraceWarning, "Warning: 0 : Trace warning message"),
            new ClassifiedText(ClassificationType.DebugTraceInformation, "Information: 0 : Trace information message"),
            new ClassifiedText(ClassificationType.DebugException, "Exception thrown: 'System.Exception' in ConsoleDemo.exe\r\n"),
        };
    }
}