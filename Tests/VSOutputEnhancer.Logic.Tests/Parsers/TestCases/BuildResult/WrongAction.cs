using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildResult;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.BuildResult
{
    [ExcludeFromCodeCoverage]
    public class WrongAction : TestCaseBase
    {
        public override String Input { get; } = "========== Publish: 10 succeeded, 3 failed, 122 skipped ==========\r\n";
        public override BuildResultData ExpectedResult { get; } = null;
    }
}