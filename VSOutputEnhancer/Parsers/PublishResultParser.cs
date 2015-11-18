using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    internal class PublishResultParser {
        public static Boolean TryParse(SnapshotSpan span, out PublishResultParser result) {
            result = null;

            var text = span.GetText();

            if (!text.StartsWith("========== Publish: ", StringComparison.Ordinal)) {
                return false;
            }
            if (!text.EndsWith(" ==========\r\n", StringComparison.Ordinal)) {
                return false;
            }

            var regex = "^========== (?:Publish): (?<Succeeded>\\d+) succeeded, (?<Failed>\\d+) failed, (?<Skipped>\\d+) skipped ==========\r\n$";
            var match = Regex.Match(text, regex);
            if (!match.Success) {
                return false;
            }

            var localResult = new PublishResultParser();
            localResult.Succeeded = Convert.ToInt32(match.Groups["Succeeded"].Value);
            localResult.Failed = Convert.ToInt32(match.Groups["Failed"].Value);
            localResult.Skipped = Convert.ToInt32(match.Groups["Skipped"].Value);
            result = localResult;
            return true;
        }

        public Int32 Succeeded { get; private set; }
        public Int32 Failed { get; private set; }
        public Int32 Skipped { get; private set; }
    }
}
