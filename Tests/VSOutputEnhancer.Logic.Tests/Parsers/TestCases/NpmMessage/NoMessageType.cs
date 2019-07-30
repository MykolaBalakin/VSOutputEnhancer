using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.NpmMessage;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.NpmMessage
{
    [ExcludeFromCodeCoverage]
    public class NoMessageType : TestCaseBase
    {
        public override String Input { get; } = "npm \r\n";
        public override NpmMessageData ExpectedResult { get; } = null;
    }
}