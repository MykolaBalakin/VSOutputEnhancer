using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.NpmResult
{
    [UseForClassification(ContentType.Output)]
    internal class NpmResultParser : IParser<NpmResultData>
    {
        public Boolean TryParse(SnapshotSpan span, out NpmResultData result)
        {
            result = null;
            var text = span.GetText();

            if (!text.StartsWith("====npm command completed with exit code ", StringComparison.Ordinal))
            {
                return false;
            }
            if (!text.EndsWith("====\r\n", StringComparison.Ordinal))
            {
                return false;
            }

            var regex = "^====npm command completed with exit code (?<ExitCode>-?\\d+)====\r\n$";
            var match = Regex.Match(text, regex, RegexOptions.Compiled);
            if (!match.Success)
            {
                return false;
            }

            result = ParsedData.Create<NpmResultData>(match, span.Span);
            return true;
        }
    }
}