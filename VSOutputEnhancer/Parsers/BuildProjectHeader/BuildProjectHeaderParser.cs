using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.BuildProjectHeader
{
    [UseForClassification(ContentType.BuildOutput)]
    [UseForClassification(ContentType.BuildOrderOutput)]
    internal class BuildProjectHeaderParser : IParser<BuildProjectHeaderData>
    {
        public Boolean TryParse(SnapshotSpan span, out BuildProjectHeaderData result)
        {
            var text = span.GetText();

            result = null;
            if (!text.EndsWith(" ------\r\n", StringComparison.Ordinal))
            {
                return false;
            }

            var regex = $"^(?:(?<BuildTaskId>\\d+)>)------ (?<FullMessage>.*) ------\r\n$";

            var match = Regex.Match(text, regex, RegexOptions.Compiled);
            if (!match.Success)
            {
                return false;
            }

            result = ParsedData.Create<BuildProjectHeaderData>(match, span.Span);
            return true;
        }
    }
}