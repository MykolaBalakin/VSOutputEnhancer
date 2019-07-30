using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.PublishResult;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.PublishResult
{
    [ExcludeFromCodeCoverage]
    public class NotParsed : TestCaseBase
    {
        public override String Input { get; } = "Message\r\n";
        public override PublishResultData ExpectedResult { get; } = null;
    }
}