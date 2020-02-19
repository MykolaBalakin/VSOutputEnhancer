using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.BuildActionStart
{
    [Export(typeof(IParser<BuildActionStartData>))]
    public class BuildActionStartParser : IParser<BuildActionStartData>
    {
        public Boolean TryParse(SnapshotSpan span, out BuildActionStartData result)
        {
            var text = span.GetText();

            result = null;
            if (!text.EndsWith(" ------\r\n", StringComparison.Ordinal))
            {
                return false;
            }

            var regex = "^(?:(?<BuildTaskId>\\d+)>)?(?<FullMessage>------ (?<Action>.+) started: Project: (?<ProjectName>.*), Configuration: (?<Configuration>.*) ------)\r\n$";
            var match = Regex.Match(text, regex, RegexOptions.Compiled);
            if (!match.Success)
            {
                return false;
            }

            result = ParsedData.Create<BuildActionStartData>(match, span.Span);
            return true;
        }
    }
}