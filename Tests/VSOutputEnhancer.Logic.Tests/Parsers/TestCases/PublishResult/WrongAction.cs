using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.PublishResult;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.PublishResult
{
    [ExcludeFromCodeCoverage]
    public class WrongAction : TestCaseBase
    {
        public override String Input { get; } = "========== Build: 5 succeeded, 1 failed, 3 up-to-date, 2 skipped ==========\r\n";
        public override PublishResultData ExpectedResult { get; } = null;
    }
}