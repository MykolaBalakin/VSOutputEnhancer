using System;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.BuildResult
{
    public class BuildResultData : ParsedData
    {
        // TODO: Refactor ParsedData builder to get rid of this constructor
        public BuildResultData()
        {
        }

        public BuildResultData(
            ParsedValue<Int32> succeeded,
            ParsedValue<Int32> failed,
            ParsedValue<Int32> upToDate,
            ParsedValue<Int32> skipped)
        {
            Succeeded = succeeded;
            Failed = failed;
            UpToDate = upToDate;
            Skipped = skipped;
        }

        public ParsedValue<Int32> Succeeded { get; private set; }
        public ParsedValue<Int32> Failed { get; private set; }
        public ParsedValue<Int32> UpToDate { get; private set; }
        public ParsedValue<Int32> Skipped { get; private set; }
    }
}