using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.BowerMessage
{
    [ExcludeFromCodeCoverage]
    public class PackageNotFound : TestCaseBase
    {
        public override String Input { get; } = "bower bootstrap1#3.3.5       ENOTFOUND Package bootstrap1 not found\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(39, 28), ClassificationType.BowerMessageError);
    }
}