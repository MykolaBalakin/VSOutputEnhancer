using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.NpmMessage {
    [UseForClassification(ContentType.Output)]
    [UseForClassification(ContentType.BuildOutput)]
    [UseForClassification(ContentType.BuildOrderOutput)]
    internal class NpmMessageParser : IParser<NpmMessageData> {
        public Boolean TryParse(SnapshotSpan span, out NpmMessageData result) {
            result = null;
            var text = span.GetText();

            if (!text.StartsWith("npm ", StringComparison.Ordinal)) {
                return false;
            }

            var regex = "^npm (?<NpmMessageType>WARN|ERR!) (?<Message>.*)\r\n$";
            var match = Regex.Match(text, regex, RegexOptions.Compiled);
            if (!match.Success) {
                return false;
            }

            result = ParsedData.Create<NpmMessageData>(match, span.Span);
            return true;
        }
    }
}