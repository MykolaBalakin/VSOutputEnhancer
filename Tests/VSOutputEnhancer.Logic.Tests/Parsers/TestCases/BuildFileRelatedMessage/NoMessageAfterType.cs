using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildFileRelatedMessage;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.BuildFileRelatedMessage
{
    [ExcludeFromCodeCoverage]
    public class NoMessageAfterType : TestCaseBase
    {
        public override String Input { get; } = "Some message: error \r\n";
        public override BuildFileRelatedMessageData ExpectedResult { get; } = null;
    }
}