using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.NpmMessage {
    internal class NpmMessageData : ParsedData {
        public ParsedValue<String> Message { get; set; }
        public ParsedValue<MessageType> Type { get; set; }

        protected override void Fill(Match match, Span originalSpan) {
            base.Fill(match, originalSpan);

            var npmMessageTypeGroup = match.Groups["NpmMessageType"];
            if (npmMessageTypeGroup.Success) {
                var span = new Span(originalSpan.Start + npmMessageTypeGroup.Index, npmMessageTypeGroup.Length);
                switch (npmMessageTypeGroup.Value) {
                        case "WARN":
                        Type = new ParsedValue<MessageType>(MessageType.Warning, span);
                        break;
                    case "ERR!":
                        Type = new ParsedValue<MessageType>(MessageType.Error, span);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
