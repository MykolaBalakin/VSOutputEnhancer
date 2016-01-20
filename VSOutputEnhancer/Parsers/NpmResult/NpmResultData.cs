using System;
using System.Collections.Generic;
using System.Linq;

namespace Balakin.VSOutputEnhancer.Parsers.NpmResult {
    internal class NpmResultData : ParsedData {
        public ParsedValue<Int32> ExitCode { get; set; }
    }
}
