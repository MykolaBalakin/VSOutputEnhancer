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
            var regex = $"^(?<Source>.*) (?<Type>{allTraceEventTypes}): (?<Id>\\d+) : (?<Message>.*)\r\n$";
            var match = Regex.Match(text, regex);
            if (!match.Success) {
                return false;
            }

            var source = match.Groups["Source"].Value;
            var type = match.Groups["Type"].Value;
            var id = match.Groups["Id"].Value;
            var message = match.Groups["Message"].Value;
            var sourceSpan = new Span(span.Start.Position + match.Groups["Source"].Index, source.Length);
            var typeSpan = new Span(span.Start.Position + match.Groups["Type"].Index, type.Length);
            var idSpan = new Span(span.Start.Position + match.Groups["Id"].Index, id.Length);
            var messageSpan = new Span(span.Start.Position + match.Groups["Message"].Index, message.Length);

            result = new DebugTraceMessageParsedData(
                source,
                (TraceEventType)Enum.Parse(typeof(TraceEventType), type),
                Convert.ToInt32(id),
                message,
                sourceSpan,
                typeSpan,
                idSpan,
                messageSpan);
            return true;
        }
    }
}
