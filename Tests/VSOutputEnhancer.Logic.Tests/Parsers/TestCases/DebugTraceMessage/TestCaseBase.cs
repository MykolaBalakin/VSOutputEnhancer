using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.DebugTraceMessage;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.DebugTraceMessage
{
    [ExcludeFromCodeCoverage]
    public abstract class TestCaseBase : ITestCase<DebugTraceMessageData>
    {
        public IParser<DebugTraceMessageData> CreateParser() => new DebugTraceMessageParser();

        public abstract String Input { get; }
        public abstract DebugTraceMessageData ExpectedResult { get; }
    }
}