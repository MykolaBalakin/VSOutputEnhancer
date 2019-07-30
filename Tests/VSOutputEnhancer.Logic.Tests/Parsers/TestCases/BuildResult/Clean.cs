using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildResult;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.BuildResult
{
    [ExcludeFromCodeCoverage]
    public class Clean : TestCaseBase
    {
        public override String Input { get; } = "========== Clean: 15 succeeded, 13 failed, 1 skipped ==========\r\n";

        public override BuildResultData ExpectedResult { get; } = new BuildResultData(
            new ParsedValue<Int32>(15, new Span(18, 2)),
            new ParsedValue<Int32>(13, new Span(32, 2)),
            new ParsedValue<Int32>(),
            new ParsedValue<Int32>(1, new Span(43, 1))
        );
    }
}