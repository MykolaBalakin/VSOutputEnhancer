using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.PublishResult {
    [UseForClassification(ContentType = ContentType.BuildOutput, DataProcessor = typeof(PublishResultDataProcessor))]
    internal class PublishResultParser : IParser<PublishResultData> {
        public Boolean TryParse(SnapshotSpan span, out PublishResultData result) {
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

            result = ParsedData.Create<PublishResultData>(match, span.Span);
            return true;
        }
    }
}
