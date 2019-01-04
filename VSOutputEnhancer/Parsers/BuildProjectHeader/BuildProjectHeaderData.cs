using System;

namespace Balakin.VSOutputEnhancer.Parsers.BuildProjectHeader
{
    // TODO: Review accessibility level
    public class BuildProjectHeaderData : ParsedData
    {
        // TODO: Refactor ParsedData builder to get rid of this constructor
        public BuildProjectHeaderData()
        {
        }

        public BuildProjectHeaderData(
            ParsedValue<Int32> buildTaskId,
            ParsedValue<String> fullMessage)
        {
            BuildTaskId = buildTaskId;
            FullMessage = fullMessage;
        }

        // This properties filled using reflection
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        public ParsedValue<Int32> BuildTaskId { get; private set; }
        public ParsedValue<String> FullMessage { get; private set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}