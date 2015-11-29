using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.BuildFileRelatedMessage {
    internal class BuildFileRelatedMessageParser : IParser<BuildFileRelatedMessageData> {
        public Boolean TryParse(SnapshotSpan span, out BuildFileRelatedMessageData result) {
            result = null;
            var text = span.GetText();

            var locationVariants = new[] {
                "\\(\\d+,\\d+,\\d+,\\d+\\)",
                "\\(\\d+,\\d+\\)"
            };
            var regex = $"^\\d+>(?<FilePath>.*?)(?<Location>{String.Join("|", locationVariants)})?: (?<FullMessage>(?<Type>warning|error) (?<Code>\\w+): (?<Message>.*))\r\n$";
            var match = Regex.Match(text, regex);
            if (!match.Success) {
                return false;
            }

            result = ParsedData.Create<BuildFileRelatedMessageData>(match, span.Span);
            return true;
        }
    }
}
