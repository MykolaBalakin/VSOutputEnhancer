using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.NpmResult
{
    [ExcludeFromCodeCoverage]
    public class Succeeded : TestCaseBase
    {
        public override String Input { get; } = "====npm command completed with exit code 0====\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(0, 48), ClassificationType.NpmResultSucceeded);
    }
}