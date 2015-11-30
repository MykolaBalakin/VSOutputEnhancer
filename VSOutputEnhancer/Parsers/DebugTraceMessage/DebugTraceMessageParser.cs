using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.DebugTraceMessage {
    [UseForClassification(ContentType = ContentType.DebugOutput, DataProcessor = typeof(DebugTraceMessageDataProcessor))]
    internal class DebugTraceMessageParser : IParser<DebugTraceMessageData> {
        public Boolean TryParse(SnapshotSpan span, out DebugTraceMessageData result) {
            result = null;
            var text = span.GetText();
            var allTraceEventTypes = String.Join("|", Enum.GetNames(typeof(TraceEventType)));
            var regex = $"^(?<Source>.*) (?<PrettyMessage>(?<Type>{allTraceEventTypes}): (?<Id>\\d+) : (?<Message>.*))\r\n$";
            var match = Regex.Match(text, regex);
            if (!match.Success) {
                return false;
            }

            result = ParsedData.Create<DebugTraceMessageData>(match, span.Span);
            return true;
        }
    }
}
