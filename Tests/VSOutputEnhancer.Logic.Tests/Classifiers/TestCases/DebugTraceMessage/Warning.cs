using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.DebugTraceMessage
{
    [ExcludeFromCodeCoverage]
    public class Warning : TestCaseBase
    {
        public override String Input { get; } = "VSOutputEnhancerDemo.vshost.exe Warning: 0 : Trace warning message\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(32, 34), ClassificationType.DebugTraceWarning);
    }
}