using System;

namespace Balakin.VSOutputEnhancer.Parsers.NpmResult
{
    // TODO: Review accessibility level
    public class NpmResultData : ParsedData
    {
        // TODO: Refactor ParsedData builder to get rid of this constructor
        public NpmResultData()
        {
        }

        public NpmResultData(ParsedValue<Int32> exitCode)
        {
            ExitCode = exitCode;
        }

        public ParsedValue<Int32> ExitCode { get; set; }
    }
}