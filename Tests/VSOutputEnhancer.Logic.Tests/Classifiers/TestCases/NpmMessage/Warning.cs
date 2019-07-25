using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.NpmMessage
{
    [ExcludeFromCodeCoverage]
    public class Warning : TestCaseBase
    {
        public override String Input { get; } = "npm WARN package.json ASP.NET@0.0.0 No description\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(9, 41), ClassificationType.NpmMessageWarning);
    }
}