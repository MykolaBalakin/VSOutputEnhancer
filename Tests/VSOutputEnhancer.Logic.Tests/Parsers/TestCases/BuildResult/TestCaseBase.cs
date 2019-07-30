using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildResult;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.BuildResult
{
    [ExcludeFromCodeCoverage]
    public abstract class TestCaseBase : ITestCase<BuildResultData>
    {
        public IParser<BuildResultData> CreateParser() => new BuildResultParser();

        public abstract String Input { get; }
        public abstract BuildResultData ExpectedResult { get; }
    }
}