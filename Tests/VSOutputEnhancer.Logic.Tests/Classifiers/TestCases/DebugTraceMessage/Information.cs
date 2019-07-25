using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.DebugTraceMessage
{
    [ExcludeFromCodeCoverage]
    public class Information : TestCaseBase
    {
        public override String Input { get; } = "VSOutputEnhancerDemo.vshost.exe Information: 0 : Trace information message\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(32, 42), ClassificationType.DebugTraceInformation);
    }
}