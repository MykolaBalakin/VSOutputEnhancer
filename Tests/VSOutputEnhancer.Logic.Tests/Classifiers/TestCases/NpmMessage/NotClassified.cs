using System;
using System.Diagnostics.CodeAnalysis;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.NpmMessage
{
    [ExcludeFromCodeCoverage]
    public class NotClassified : TestCaseBase
    {
        public override String Input { get; } = "Some message\r\n";
        public override ProcessedParsedData ExpectedResult { get; } = null;
    }
}