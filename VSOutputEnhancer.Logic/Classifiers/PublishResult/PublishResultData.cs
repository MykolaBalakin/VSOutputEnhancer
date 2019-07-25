using System;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.PublishResult
{
    public class PublishResultData : ParsedData
    {
        // TODO: Refactor ParsedData builder to get rid of this constructor
        public PublishResultData()
        {
        }

        public PublishResultData(ParsedValue<Int32> succeeded, ParsedValue<Int32> failed, ParsedValue<Int32> skipped)
        {
            Succeeded = succeeded;
            Failed = failed;
            Skipped = skipped;
        }

        public ParsedValue<Int32> Succeeded { get; private set; }
        public ParsedValue<Int32> Failed { get; private set; }
        public ParsedValue<Int32> Skipped { get; private set; }
    }
}