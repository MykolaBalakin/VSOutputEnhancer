using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.DebugTraceMessage
{
    [ExcludeFromCodeCoverage]
    public class UnsupportedMessageType : TestCaseBase
    {
        public override String Input { get; } = "VSOutputEnhancerDemo.vshost.exe Transfer: 0 : Trace warning message\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = null;
    }
}