using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.DebugException;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.DebugException
{
    [ExcludeFromCodeCoverage]
    public class Exception : TestCaseBase
    {
        public override String Input { get; } = "Exception thrown: 'System.Exception' in VSOutputEnhancerDemo.exe\r\n";

        public override DebugExceptionData ExpectedResult { get; } = new DebugExceptionData(
            new ParsedValue<String>("System.Exception", new Span(19, 16)),
            new ParsedValue<String>("VSOutputEnhancerDemo.exe", new Span(40, 24))
        );
    }
}