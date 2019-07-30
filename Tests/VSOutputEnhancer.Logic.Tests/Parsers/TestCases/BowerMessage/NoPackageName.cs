using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BowerMessage;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.BowerMessage
{
    [ExcludeFromCodeCoverage]
    public class NoPackageName : TestCaseBase
    {
        public override String Input { get; } = "bower \r\n";
        public override BowerMessageData ExpectedResult { get; } = null;
    }
}