using System;
using Balakin.VSOutputEnhancer.Parsers;

namespace Balakin.VSOutputEnhancer.Classifiers.BuildActionStart
{
    public class BuildActionStartData : ParsedData
    {
        // TODO: Refactor ParsedData builder to get rid of this constructor
        public BuildActionStartData()
        {
        }

        public BuildActionStartData(
            ParsedValue<Int32> buildTaskId,
            ParsedValue<String> projectName,
            ParsedValue<String> action,
            ParsedValue<String> fullMessage)
        {
            BuildTaskId = buildTaskId;
            ProjectName = projectName;
            Action = action;
            FullMessage = fullMessage;
        }

        public ParsedValue<Int32> BuildTaskId { get; private set; }
        public ParsedValue<String> ProjectName { get; private set; }
        public ParsedValue<String> Action { get; private set; }
        public ParsedValue<String> FullMessage { get; private set; }
    }
}