using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.BuildResult
{
    [Export(typeof(IParser<BuildResultData>))]
    internal class BuildResultParser : IParser<BuildResultData>
    {
        public Boolean TryParse(SnapshotSpan span, out BuildResultData result)
        {
            var text = span.GetText();

            result = null;
            if (!text.StartsWith("========== ", StringComparison.Ordinal))
            {
                return false;
            }
            if (!text.EndsWith(" ==========\r\n", StringComparison.Ordinal))
            {
                return false;
            }

            var regex = "^========== (?:Build|Rebuild All|Clean): (?<Succeeded>\\d+) succeeded(?: or up-to-date)?, (?<Failed>\\d+) failed, (?:(?<UpToDate>\\d+) up-to-date, )?(?<Skipped>\\d+) skipped ==========\r\n$";
            var match = Regex.Match(text, regex, RegexOptions.Compiled);
            if (!match.Success)
            {
                return false;
            }

            result = ParsedData.Create<BuildResultData>(match, span.Span);
            return true;
        }
    }
}