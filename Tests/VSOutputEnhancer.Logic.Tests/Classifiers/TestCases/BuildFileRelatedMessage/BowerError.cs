using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.BuildFileRelatedMessage
{
    [ExcludeFromCodeCoverage]
    public class BowerError : TestCaseBase
    {
        public override String Input { get; } = "C:\\Program Files (x86)\\MSBuild\\Microsoft\\VisualStudio\\v14.0\\Web\\Microsoft.DNX.Publishing.targets(152,5): Error : bower bootstrap1#3.3.5       ENOTFOUND Package bootstrap1 not found\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(105, 75), ClassificationType.BuildMessageError);
    }
}