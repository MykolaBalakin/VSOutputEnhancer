using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildFileRelatedMessage;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.BuildFileRelatedMessage
{
    [ExcludeFromCodeCoverage]
    public class Warning1 : TestCaseBase
    {
        public override String Input { get; } = "1>C:\\VSOutputEnhancer\\ClassificationType.cs(29,53,29,83): warning CS0169: The field 'ClassificationType.BuildResultSucceededDefinition' is never used\r\n";

        public override BuildFileRelatedMessageData ExpectedResult { get; } = new BuildFileRelatedMessageData(
            new ParsedValue<Int32>(1, new Span(0, 1)),
            new ParsedValue<MessageType>(MessageType.Warning, new Span(58, 7)),
            new ParsedValue<String>("CS0169", new Span(66, 6)),
            new ParsedValue<String>("The field 'ClassificationType.BuildResultSucceededDefinition' is never used", new Span(74, 75)),
            new ParsedValue<String>("warning CS0169: The field 'ClassificationType.BuildResultSucceededDefinition' is never used", new Span(58, 91)),
            new ParsedValue<String>("C:\\VSOutputEnhancer\\ClassificationType.cs", new Span(2, 41))
        );
    }
}