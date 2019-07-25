using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.DebugTraceMessage;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.DebugTraceMessage
{
    [ExcludeFromCodeCoverage]
    public abstract class TestCaseBase : ITestCase
    {
        public ISpanClassifier CreateClassifier()
        {
            var parser = new DebugTraceMessageParser();
            var classifier = new DebugTraceMessageClassifier(parser);
            return classifier;
        }

        public abstract String Input { get; }
        public abstract ProcessedParsedData ExpectedResult { get; }
    }
}