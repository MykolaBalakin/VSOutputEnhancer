using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.NpmMessage
{
    public class NpmMessageData : ParsedData
    {
        // TODO: Refactor ParsedData builder to get rid of this constructor
        public NpmMessageData()
        {
        }

        public NpmMessageData(ParsedValue<MessageType> type, ParsedValue<String> message)
        {
            Type = type;
            Message = message;
        }

        public ParsedValue<MessageType> Type { get; set; }
        public ParsedValue<String> Message { get; set; }

        protected override void Fill(Match match, Span originalSpan)
        {
            base.Fill(match, originalSpan);

            var npmMessageTypeGroup = match.Groups["NpmMessageType"];
            if (npmMessageTypeGroup.Success)
            {
                var span = new Span(originalSpan.Start + npmMessageTypeGroup.Index, npmMessageTypeGroup.Length);
                switch (npmMessageTypeGroup.Value)
                {
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