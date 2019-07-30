using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.DebugTraceMessage;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.DebugTraceMessage
{
    [ExcludeFromCodeCoverage]
    public class NotParsed : TestCaseBase
    {
        public override String Input { get; } = "Some message\r\n";
        public override DebugTraceMessageData ExpectedResult { get; } = null;
    }
}