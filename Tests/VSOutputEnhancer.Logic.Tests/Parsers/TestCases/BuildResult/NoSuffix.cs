using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildResult;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.BuildResult
{
    [ExcludeFromCodeCoverage]
    public class NoSuffix : TestCaseBase
    {
        public override String Input { get; } = "========== Build: bla bla\r\n";
        public override BuildResultData ExpectedResult { get; } = null;
    }
}