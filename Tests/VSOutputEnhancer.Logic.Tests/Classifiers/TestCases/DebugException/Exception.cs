using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.DebugException
{
    [ExcludeFromCodeCoverage]
    public class Exception : TestCaseBase
    {
        public override String Input { get; } = "Exception thrown: 'System.Exception' in VSOutputEnhancerDemo.exe\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(0, 66), ClassificationType.DebugException);
    }
}