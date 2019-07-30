using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.PublishResult;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.PublishResult
{
    [ExcludeFromCodeCoverage]
    public class NoProjectNumber : TestCaseBase
    {
        public override String Input { get; } = "========== Publish: bla bla ==========\r\n";
        public override PublishResultData ExpectedResult { get; } = null;
    }
}