using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.BuildResult
{
    [ExcludeFromCodeCoverage]
    public class Failed : TestCaseBase
    {
        public override String Input { get; } = "========== Build: 5 succeeded, 1 failed, 3 up-to-date, 2 skipped ==========\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(0, 77), ClassificationType.BuildResultFailed);
    }
}