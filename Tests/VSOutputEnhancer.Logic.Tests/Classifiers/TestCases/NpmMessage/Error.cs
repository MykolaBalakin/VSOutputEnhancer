using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.NpmMessage
{
    [ExcludeFromCodeCoverage]
    public class Error : TestCaseBase
    {
        public override String Input { get; } = "npm ERR! 404 Not Found\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(9, 13), ClassificationType.NpmMessageError);
    }
}