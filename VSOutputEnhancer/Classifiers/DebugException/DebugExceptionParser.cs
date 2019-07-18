using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.DebugException
{
    [Export(typeof(IParser<DebugExceptionData>))]
    internal class DebugExceptionParser : IParser<DebugExceptionData>
    {
        public Boolean TryParse(SnapshotSpan span, out DebugExceptionData result)
        {
            result = null;
            var text = span.GetText();
            if (text.StartsWith("Exception thrown: '", StringComparison.Ordinal))
            {
                var regex = "^Exception thrown: '(?<Exception>.*)' in (?<Assembly>.*)\r\n$";
                var match = Regex.Match(text, regex, RegexOptions.Compiled);
                if (!match.Success)
                {
                    return false;
                }

                result = ParsedData.Create<DebugExceptionData>(match, span.Span);
                return true;
            }

            // VS 2013 message
            if (text.StartsWith("A first chance exception of type '", StringComparison.Ordinal))
            {
                var regex = "^A first chance exception of type '(?<Exception>.*)' occurred in (?<Assembly>.*)\r\n$";
                var match = Regex.Match(text, regex, RegexOptions.Compiled);
                if (!match.Success)
                {
                    return false;
                }

                result = ParsedData.Create<DebugExceptionData>(match, span.Span);
                return true;
            }

            return false;
        }
    }
}