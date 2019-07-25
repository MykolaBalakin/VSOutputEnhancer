using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.BuildFileRelatedMessage
{
    [ExcludeFromCodeCoverage]
    public class Error : TestCaseBase
    {
        public override String Input { get; } = "1>C:\\Sources\\GitHub\\VSOutputEnhancer\\UnitTests\\BuildOutputClassifierTests.cs(91,64,91,65): error CS1026: ) expected\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = new ProcessedParsedData(new Span(91, 24), ClassificationType.BuildMessageError);
    }
}