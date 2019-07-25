using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.BowerMessage
{
    public class BowerMessageData : ParsedData
    {
        public ParsedValue<String> PackageName { get; set; }
        public ParsedValue<String> PackageVersion { get; set; }
        public ParsedValue<MessageType> Type { get; set; }
        public ParsedValue<String> ErrorCode { get; set; }
        public ParsedValue<String> Message { get; set; }

        protected override void Fill(Match match, Span originalSpan)
        {
            base.Fill(match, originalSpan);

            if (ErrorCode.HasValue)
            {
                Type = new ParsedValue<MessageType>(MessageType.Error, ErrorCode.Span);
            }
        }
    }
}