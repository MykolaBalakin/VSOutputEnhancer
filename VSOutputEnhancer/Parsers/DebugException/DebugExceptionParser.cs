using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.DebugException {
    [UseForClassification(ContentType = ContentType.DebugOutput, DataProcessor = typeof(DebugExceptionDataProcessor))]
    internal class DebugExceptionParser : IParser<DebugExceptionData> {
        public Boolean TryParse(SnapshotSpan span, out DebugExceptionData result) {
            result = null;
            var text = span.GetText();
            if (!text.StartsWith("Exception thrown: '", StringComparison.Ordinal)) {
                return false;
            }

            var regex = "^Exception thrown: '(?<Exception>.*)' in (?<Assembly>.*)\r\n$";
            var match = Regex.Match(text, regex);
            if (!match.Success) {
                return false;
            }

            result = ParsedData.Create<DebugExceptionData>(match, span.Span);
            return true;
        }
    }
}
