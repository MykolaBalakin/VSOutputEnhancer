using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    internal class BuildResult {
        public static Boolean TryParse(SnapshotSpan span, out BuildResult result) {
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

            var localResult = new BuildResult();
            localResult.Succeeded = Convert.ToInt32(match.Groups["Succeeded"].Value);
            localResult.Failed = Convert.ToInt32(match.Groups["Failed"].Value);
            if (match.Groups["UpToDate"].Success) {
                localResult.UpToDate = Convert.ToInt32(match.Groups["UpToDate"].Value);
            }
            localResult.Skipped = Convert.ToInt32(match.Groups["Skipped"].Value);
            result = localResult;
            return true;
        }

        public Int32 Succeeded { get; private set; }
        public Int32 Failed { get; private set; }
        public Int32 UpToDate { get; private set; }
        public Int32 Skipped { get; private set; }
    }
}
