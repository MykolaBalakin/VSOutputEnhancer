using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    internal class DebugTraceMessageParser : IParser<DebugTraceMessageParsedData> {
        public Boolean TryParse(SnapshotSpan span, out DebugTraceMessageParsedData result) {
            result = null;
            var text = span.GetText();
            var allTraceEventTypes = String.Join("|", Enum.GetNames(typeof(TraceEventType)));
            var regex = $"^(?<Source>.*) (?<PrettyMessage>(?<Type>{allTraceEventTypes}): (?<Id>\\d+) : (?<Message>.*))\r\n$";
            var match = Regex.Match(text, regex);
            if (!match.Success) {
                return false;
            }

            result = ParsedData.Create<DebugTraceMessageParsedData>(match, span.Span);
            return true;
        }
    }
}
