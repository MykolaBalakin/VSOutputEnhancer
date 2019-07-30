using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.NpmMessage;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.NpmMessage
{
    [ExcludeFromCodeCoverage]
    public class NotParsed : TestCaseBase
    {
        public override String Input { get; } = "Some message\r\n";
        public override NpmMessageData ExpectedResult { get; } = null;
    }
}