using System;
using System.Diagnostics;

namespace Balakin.VSOutputEnhancer.Parsers.DebugTraceMessage
{
    internal class DebugTraceMessageData : ParsedData
    {
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