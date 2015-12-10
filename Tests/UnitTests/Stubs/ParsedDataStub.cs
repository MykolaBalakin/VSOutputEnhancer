using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Balakin.VSOutputEnhancer.Parsers;

namespace Balakin.VSOutputEnhancer.UnitTests.Stubs {
    [ExcludeFromCodeCoverage]
    internal class ParsedDataStub : ParsedData {
        // This properties filled using reflection
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        public ParsedValue<String> Message { get; private set; }
        public ParsedValue<TraceEventType> Type { get; private set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}
