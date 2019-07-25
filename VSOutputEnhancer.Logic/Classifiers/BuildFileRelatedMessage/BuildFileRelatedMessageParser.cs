using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.BuildFileRelatedMessage
{
    [Export(typeof(IParser<BuildFileRelatedMessageData>))]
    public class BuildFileRelatedMessageParser : IParser<BuildFileRelatedMessageData>
    {
        public Boolean TryParse(SnapshotSpan span, out BuildFileRelatedMessageData result)
        {
            result = null;
            var text = span.GetText();

            var lowerText = text.ToLowerInvariant();
            // TODO: Should load possible enum values by reflection
            if (!lowerText.Contains(": warning ")
                && !lowerText.Contains(": error ")
                && !lowerText.Contains(": fatal error "))
            {
                return false;
            }

            var locationVariants = new[]
            {
                "\\(\\d+,\\d+,\\d+,\\d+\\)",
                "\\(\\d+,\\d+\\)",
                "\\(\\d+\\)"
            };
            var regex = $"^(?:(?<BuildTaskId>\\d+)>)?(?<FilePath>.*?)(?<Location>{String.Join("|", locationVariants)})?: (?<FullMessage>(?<Type>(fatal |Fatal )?(warning|error|Warning|Error)) (?<Code>\\w+)?: (?<Message>.*))\r\n$";
            var match = Regex.Match(text, regex, RegexOptions.Compiled);
            if (!match.Success)
            {
                return false;
            }

            result = ParsedData.Create<BuildFileRelatedMessageData>(match, span.Span);
            return true;
        }
    }
}