using System;

namespace Balakin.VSOutputEnhancer.Parsers.BuildFileRelatedMessage
{
    internal class BuildFileRelatedMessageData : ParsedData
    {
        // This properties filled using reflection
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        public ParsedValue<Int32> BuildTaskId { get; private set; }
        public ParsedValue<String> Message { get; private set; }
        public ParsedValue<String> FullMessage { get; private set; }
        public ParsedValue<String> Code { get; private set; }
        public ParsedValue<MessageType> Type { get; private set; }
        public ParsedValue<String> FilePath { get; private set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}