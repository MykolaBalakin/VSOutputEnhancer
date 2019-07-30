using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.PublishResult;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.PublishResult
{
    [ExcludeFromCodeCoverage]
    public abstract class TestCaseBase : ITestCase<PublishResultData>
    {
        public IParser<PublishResultData> CreateParser() => new PublishResultParser();

        public abstract String Input { get; }
        public abstract PublishResultData ExpectedResult { get; }
    }
}