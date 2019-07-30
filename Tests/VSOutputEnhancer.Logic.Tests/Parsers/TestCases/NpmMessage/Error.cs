using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.NpmMessage;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.NpmMessage
{
    [ExcludeFromCodeCoverage]
    public class Error : TestCaseBase
    {
        public override String Input { get; } = "npm ERR! code E404\r\n";

        public override NpmMessageData ExpectedResult { get; } = new NpmMessageData(
            new ParsedValue<MessageType>(MessageType.Error, new Span(4, 4)),
            new ParsedValue<String>("code E404", new Span(9, 9))
        );
    }
}