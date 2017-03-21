using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Balakin.VSOutputEnhancer.Parsers;

namespace Balakin.VSOutputEnhancer.Tests.Stubs
{
    [ExcludeFromCodeCoverage]
    public class ParsedDataStub : ParsedData
    {
        // TODO: Refactor ParsedData builder to get rid of this constructor
        public ParsedDataStub()
        {
        }

        public ParsedDataStub(ParsedValue<String> message, ParsedValue<TraceEventType> type)
        {
            Message = message;
            Type = type;
        }

        public ParsedValue<String> Message { get; private set; }
        public ParsedValue<TraceEventType> Type { get; private set; }
    }
}