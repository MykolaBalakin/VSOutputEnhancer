using System;
using System.Diagnostics;

namespace Balakin.VSOutputEnhancer.Parsers.DebugTraceMessage
{
    internal class DebugTraceMessageData : ParsedData
    {
        // TODO: Refactor ParsedData builder to get rid of this constructor
        public DebugTraceMessageData()
        {
        }

        public DebugTraceMessageData(ParsedValue<String> source, ParsedValue<TraceEventType> type, ParsedValue<Int32> id, ParsedValue<String> message, ParsedValue<String> prettyMessage)
        {
            Source = source;
            Type = type;
            Id = id;
            Message = message;
            PrettyMessage = prettyMessage;
        }

        // This properties filled using reflection
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        public ParsedValue<String> Source { get; private set; }
        public ParsedValue<TraceEventType> Type { get; private set; }
        public ParsedValue<Int32> Id { get; private set; }
        public ParsedValue<String> Message { get; private set; }
        public ParsedValue<String> PrettyMessage { get; private set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}