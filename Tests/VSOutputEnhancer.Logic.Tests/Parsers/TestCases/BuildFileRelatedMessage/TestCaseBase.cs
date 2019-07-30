using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildFileRelatedMessage;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.BuildFileRelatedMessage
{
    [ExcludeFromCodeCoverage]
    public abstract class TestCaseBase : ITestCase<BuildFileRelatedMessageData>
    {
        public IParser<BuildFileRelatedMessageData> CreateParser() => new BuildFileRelatedMessageParser();

        public abstract String Input { get; }
        public abstract BuildFileRelatedMessageData ExpectedResult { get; }
    }
}