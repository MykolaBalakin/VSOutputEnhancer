using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    internal class DebugExceptionParser : IParser<DebugExceptionData> {
        public Boolean TryParse(SnapshotSpan span, out DebugExceptionData result) {
            result = null;
            var text = span.GetText();
            if (!text.StartsWith("Exception thrown: '", StringComparison.Ordinal)) {
                return false;
            }

            var regex = "^Exception thrown: '(?<Type>.*)' in (?<Assembly>.*)\r\n$";
            var match = Regex.Match(text, regex);
            if (!match.Success) {
                return false;
            }

            var exception = match.Groups["Type"].Value;
            var assembly = match.Groups["Assembly"].Value;
            var exceptionSpan = new Span(match.Groups["Type"].Index, exception.Length);
            var assemblySpan = new Span(match.Groups["Assembly"].Index, assembly.Length);

            result = new DebugExceptionData(exception, assembly, exceptionSpan, assemblySpan);
            return true;
        }
    }
}
