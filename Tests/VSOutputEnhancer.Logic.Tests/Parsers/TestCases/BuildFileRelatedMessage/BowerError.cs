using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildFileRelatedMessage;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.BuildFileRelatedMessage
{
    [ExcludeFromCodeCoverage]
    public class BowerError : TestCaseBase
    {
        public override String Input { get; } = "C:\\Program Files (x86)\\MSBuild\\Microsoft\\VisualStudio\\v14.0\\Web\\Microsoft.DNX.Publishing.targets(152,5): Error : bower bootstrap1#3.3.5       ENOTFOUND Package bootstrap1 not found\r\n";
        
        public override BuildFileRelatedMessageData ExpectedResult { get; } = new BuildFileRelatedMessageData(
            new ParsedValue<Int32>(),
            new ParsedValue<MessageType>(MessageType.Error, new Span(105, 5)),
            new ParsedValue<String>(),
            new ParsedValue<String>("bower bootstrap1#3.3.5       ENOTFOUND Package bootstrap1 not found", new Span(113, 67)),
            new ParsedValue<String>("Error : bower bootstrap1#3.3.5       ENOTFOUND Package bootstrap1 not found", new Span(105, 75)),
            new ParsedValue<String>("C:\\Program Files (x86)\\MSBuild\\Microsoft\\VisualStudio\\v14.0\\Web\\Microsoft.DNX.Publishing.targets", new Span(0, 96))
        );
    }
}