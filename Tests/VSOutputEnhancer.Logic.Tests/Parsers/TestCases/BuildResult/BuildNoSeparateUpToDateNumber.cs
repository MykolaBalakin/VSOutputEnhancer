using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildResult;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.BuildResult
{
    [ExcludeFromCodeCoverage]
    public class BuildNoSeparateUpToDateNumber : TestCaseBase
    {
        public override String Input { get; } = "========== Build: 10 succeeded or up-to-date, 3 failed, 43 skipped ==========\r\n";

        public override BuildResultData ExpectedResult { get; } = new BuildResultData(
            new ParsedValue<Int32>(10, new Span(18, 2)),
            new ParsedValue<Int32>(3, new Span(46, 1)),
            new ParsedValue<Int32>(),
            new ParsedValue<Int32>(43, new Span(56, 2))
        );
    }
}