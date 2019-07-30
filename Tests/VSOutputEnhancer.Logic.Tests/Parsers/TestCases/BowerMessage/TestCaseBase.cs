using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BowerMessage;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.BowerMessage
{
    [ExcludeFromCodeCoverage]
    public abstract class TestCaseBase : ITestCase<BowerMessageData>
    {
        public IParser<BowerMessageData> CreateParser() => new BowerMessageParser();

        public abstract String Input { get; }
        public abstract BowerMessageData ExpectedResult { get; }
    }
}