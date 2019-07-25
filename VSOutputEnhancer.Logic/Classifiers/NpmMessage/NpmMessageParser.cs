using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.NpmMessage
{
    [Export(typeof(IParser<NpmMessageData>))]
    public class NpmMessageParser : IParser<NpmMessageData>
    {
        public Boolean TryParse(SnapshotSpan span, out NpmMessageData result)
        {
            result = null;
            var text = span.GetText();

            if (!text.StartsWith("npm ", StringComparison.Ordinal))
            {
                return false;
            }

            var regex = "^npm (?<NpmMessageType>WARN|ERR!) (?<Message>.*)\r\n$";
            var match = Regex.Match(text, regex, RegexOptions.Compiled);
            if (!match.Success)
            {
                return false;
            }

            result = ParsedData.Create<NpmMessageData>(match, span.Span);
            return true;
        }
    }
}