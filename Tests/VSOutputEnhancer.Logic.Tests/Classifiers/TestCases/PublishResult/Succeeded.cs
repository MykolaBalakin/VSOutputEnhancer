using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.PublishResult
{
    [ExcludeFromCodeCoverage]
    public class Succeeded : TestCaseBase
    {
        public override String Input { get; } = "========== Publish: 1 succeeded, 0 failed, 0 skipped ==========\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(0, 65), ClassificationType.PublishResultSucceeded);
    }
}