using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.BuildResult
{
    [ExcludeFromCodeCoverage]
    public class Succeeded : TestCaseBase
    {
        public override String Input { get; } = "========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(0, 77), ClassificationType.BuildResultSucceeded);
    }
}