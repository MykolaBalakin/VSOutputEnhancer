using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.BuildResult
{
    [ExcludeFromCodeCoverage]
    public class SucceededOrUpToDate : TestCaseBase
    {
        public override String Input { get; } = "========== Build: 3 succeeded or up-to-date, 0 failed, 0 skipped ==========\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(0, 77), ClassificationType.BuildResultSucceeded);
    }
}