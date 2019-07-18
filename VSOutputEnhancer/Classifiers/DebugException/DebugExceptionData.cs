using System;

namespace Balakin.VSOutputEnhancer.Parsers.DebugException
{
    // TODO: Review accessibility level
    public class DebugExceptionData : ParsedData
    {
        // TODO: Refactor ParsedData builder to get rid of this constructor
        public DebugExceptionData()
        {
        }

        public DebugExceptionData(ParsedValue<String> exception, ParsedValue<String> assembly)
        {
            Exception = exception;
            Assembly = assembly;
        }

        public ParsedValue<String> Exception { get; private set; }
        public ParsedValue<String> Assembly { get; private set; }
    }
}