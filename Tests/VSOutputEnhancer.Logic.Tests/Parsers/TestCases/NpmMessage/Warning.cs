using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.NpmMessage;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.NpmMessage
{
    [ExcludeFromCodeCoverage]
    public class Warning : TestCaseBase
    {
        public override String Input { get; } = "npm WARN package.json ASP.NET@0.0.0 No README data\r\n";

        public override NpmMessageData ExpectedResult { get; } = new NpmMessageData(
            new ParsedValue<MessageType>(MessageType.Warning, new Span(4, 4)),
            new ParsedValue<String>("package.json ASP.NET@0.0.0 No README data", new Span(9, 41))
        );
    }
}