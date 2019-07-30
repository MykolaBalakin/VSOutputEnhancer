using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.DebugException;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.DebugException
{
    [ExcludeFromCodeCoverage]
    public class FirstChanceException : TestCaseBase
    {
        public override String Input { get; } = "A first chance exception of type 'System.Exception' occurred in ConsoleDemo.exe\r\n";

        public override DebugExceptionData ExpectedResult { get; } = new DebugExceptionData(
            new ParsedValue<String>("System.Exception", new Span(34, 16)),
            new ParsedValue<String>("ConsoleDemo.exe", new Span(64, 15))
        );
    }
}