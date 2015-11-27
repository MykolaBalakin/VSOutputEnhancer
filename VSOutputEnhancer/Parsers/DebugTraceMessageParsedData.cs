using System;
using System.Diagnostics;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    internal class DebugTraceMessageParsedData : ParsedData {
        // This properties filled by reflection
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        public ParsedValue<String> Source { get; private set; }
        public ParsedValue<TraceEventType> Type { get; private set; }
        public ParsedValue<Int32> Id { get; private set; }
        public ParsedValue<String> Message { get; private set; }
        public ParsedValue<String> PrettyMessage { get; private set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}