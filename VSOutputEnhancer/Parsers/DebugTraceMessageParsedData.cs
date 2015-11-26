using System;
using System.Diagnostics;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    internal class DebugTraceMessageParsedData : ParsedData {
        public DebugTraceMessageParsedData(String source, TraceEventType type, Int32 id, String message, Span sourceSpan, Span typeSpan, Span idSpan, Span messageSpan) {
            Source = source;
            Type = type;
            Id = id;
            Message = message;
            SourceSpan = sourceSpan;
            TypeSpan = typeSpan;
            IdSpan = idSpan;
            MessageSpan = messageSpan;
        }

        public String Source { get; }
        public TraceEventType Type { get; }
        public Int32 Id { get; }
        public String Message { get; }

        public Span SourceSpan { get; }
        public Span TypeSpan { get; }
        public Span IdSpan { get; }
        public Span MessageSpan { get; }
    }
}