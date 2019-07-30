using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildResult;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.BuildResult
{
    [ExcludeFromCodeCoverage]
    public class Rebuild : TestCaseBase
    {
        public override String Input { get; } = "========== Rebuild All: 2 succeeded, 135 failed, 86 skipped ==========\r\n";

        public override BuildResultData ExpectedResult { get; } = new BuildResultData(
            new ParsedValue<Int32>(2, new Span(24, 1)),
            new ParsedValue<Int32>(135, new Span(37, 3)),
            new ParsedValue<Int32>(),
            new ParsedValue<Int32>(86, new Span(49, 2))
        );
    }
}