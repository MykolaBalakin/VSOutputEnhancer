using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.PublishResult;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.PublishResult
{
    [ExcludeFromCodeCoverage]
    public class Publish : TestCaseBase
    {
        public override String Input { get; } = "========== Publish: 10 succeeded, 3 failed, 122 skipped ==========\r\n";

        public override PublishResultData ExpectedResult { get; } = new PublishResultData(
            new ParsedValue<Int32>(10, new Span(20, 2)),
            new ParsedValue<Int32>(3, new Span(34, 1)),
            new ParsedValue<Int32>(122, new Span(44, 3))
        );
    }
}