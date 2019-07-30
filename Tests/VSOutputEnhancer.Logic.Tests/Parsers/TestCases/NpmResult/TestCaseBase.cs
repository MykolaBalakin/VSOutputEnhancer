using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.NpmResult;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.NpmResult
{
    [ExcludeFromCodeCoverage]
    public abstract class TestCaseBase : ITestCase<NpmResultData>
    {
        public IParser<NpmResultData> CreateParser() => new NpmResultParser();

        public abstract String Input { get; }
        public abstract NpmResultData ExpectedResult { get; }
    }
}