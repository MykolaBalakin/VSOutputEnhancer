using System;

namespace Balakin.VSOutputEnhancer.Parsers.BuildFileRelatedMessage
{
    // TODO: Review accessibility level
    public class BuildFileRelatedMessageData : ParsedData
    {
        // TODO: Refactor ParsedData builder to get rid of this constructor
        public BuildFileRelatedMessageData()
        {
        }

        public BuildFileRelatedMessageData(
            ParsedValue<Int32> buildTaskId,
            ParsedValue<MessageType> type,
            ParsedValue<String> code,
            ParsedValue<String> message,
            ParsedValue<String> fullMessage,
            ParsedValue<String> filePath)
        {
            BuildTaskId = buildTaskId;
            Type = type;
            Code = code;
            Message = message;
            FullMessage = fullMessage;
            FilePath = filePath;
        }

        // This properties filled using reflection
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        public ParsedValue<Int32> BuildTaskId { get; private set; }
        public ParsedValue<MessageType> Type { get; private set; }
        public ParsedValue<String> Code { get; private set; }
        public ParsedValue<String> Message { get; private set; }
        public ParsedValue<String> FullMessage { get; private set; }
        public ParsedValue<String> FilePath { get; private set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}