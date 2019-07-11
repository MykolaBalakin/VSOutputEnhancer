using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.BowerMessage
{
    [Export(typeof(IParser<BowerMessageData>))]
    [UseForClassification(ContentType.Output)]
    internal class BowerMessageParser : IParser<BowerMessageData>
    {
        public Boolean TryParse(SnapshotSpan span, out BowerMessageData result)
        {
            var text = span.GetText();

            result = null;
            if (!text.StartsWith("bower ", StringComparison.Ordinal))
            {
                return false;
            }

            var regex = "^bower (?<PackageName>.+)#(?<PackageVersion>\\d+\\.\\d+\\.\\d+) +(?<ErrorCode>ENOTFOUND) (?<Message>.*)\r\n$";
            var match = Regex.Match(text, regex, RegexOptions.Compiled);
            if (!match.Success)
            {
                return false;
            }

            result = ParsedData.Create<BowerMessageData>(match, span.Span);
            return true;
        }
    }
}