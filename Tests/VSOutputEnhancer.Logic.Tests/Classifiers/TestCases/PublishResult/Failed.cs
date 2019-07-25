using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.PublishResult
{
    [ExcludeFromCodeCoverage]
    public class Failed : TestCaseBase
    {
        public override String Input { get; } = "========== Publish: 0 succeeded, 1 failed, 0 skipped ==========\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(0, 65), ClassificationType.PublishResultFailed);
    }
}