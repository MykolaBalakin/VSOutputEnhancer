using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.Parsers;

namespace Balakin.VSOutputEnhancer.UnitTests.Stubs {
    [ExcludeFromCodeCoverage]
    internal class ParsedDataStub : ParsedData {
        public ParsedValue<String> Message { get; private set; }

        public ParsedValue<TraceEventType> Type { get; private set; }
    }
}
