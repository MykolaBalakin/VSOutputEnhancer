using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.BuildResult {
    internal class BuildResultParser:IParser<BuildResultData> {
        public Boolean TryParse(SnapshotSpan span, out BuildResultData result) {
            var text = span.GetText();

            result = null;
            if (!text.StartsWith("========== ", StringComparison.Ordinal)) {
                return false;
            }
            if (!text.EndsWith(" ==========\r\n", StringComparison.Ordinal)) {
                return false;
            }

            var regex = "^========== (?:Build|Rebuild All): (?<Succeeded>\\d+) succeeded, (?<Failed>\\d+) failed, (?:(?<UpToDate>\\d+) up-to-date, )?(?<Skipped>\\d+) skipped ==========\r\n$";
            var match = Regex.Match(text, regex);
            if (!match.Success) {
                return false;
            }

            result = ParsedData.Create<BuildResultData>(match, span.Span);
            return true;
        }
    }
}
