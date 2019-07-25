using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.BuildFileRelatedMessage
{
    [ExcludeFromCodeCoverage]
    public class Warning : TestCaseBase
    {
        public override String Input { get; } = "1>C:\\Sources\\GitHub\\VSOutputEnhancer\\VSOutputEnhancer\\ClassificationType.cs(29,53,29,83): warning CS0169: The field 'ClassificationType.BuildResultSucceededDefinition' is never used\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(90, 91), ClassificationType.BuildMessageWarning);
    }
}