using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildResult;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.BuildResult
{
    [ExcludeFromCodeCoverage]
    public class Build : TestCaseBase
    {
        public override String Input { get; } = "========== Build: 302 succeeded, 41 failed, 16 up-to-date, 5 skipped ==========\r\n";

        public override BuildResultData ExpectedResult { get; } = new BuildResultData(
            new ParsedValue<Int32>(302, new Span(18, 3)),
            new ParsedValue<Int32>(41, new Span(33, 2)),
            new ParsedValue<Int32>(16, new Span(44, 2)),
            new ParsedValue<Int32>(5, new Span(59, 1))
        );
    }
}