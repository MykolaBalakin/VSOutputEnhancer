using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.NpmResult
{
    [ExcludeFromCodeCoverage]
    public class Failed : TestCaseBase
    {
        public override String Input { get; } = "====npm command completed with exit code 1====\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(0, 48), ClassificationType.NpmResultFailed);
    }
}