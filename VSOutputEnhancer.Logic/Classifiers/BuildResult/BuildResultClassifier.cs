using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.BuildResult
{
    [Export(typeof(ISpanClassifier))]
    public class BuildResultClassifier : ParserBasedSpanClassifier<BuildResultData>
    {
        [ImportingConstructor]
        public BuildResultClassifier(IParser<BuildResultData> parser) : base(parser)
        {
        }

        public override IEnumerable<String> ContentTypes { get; } = new[]
        {
            ContentType.BuildOutput,
            ContentType.BuildOrderOutput
        };

        protected override IEnumerable<ProcessedParsedData> Classify(SnapshotSpan span, BuildResultData parsedData)
        {
            if (parsedData.Failed == 0)
            {
                yield return new ProcessedParsedData(span, ClassificationType.BuildResultSucceeded);
            }
            else
            {
                yield return new ProcessedParsedData(span, ClassificationType.BuildResultFailed);
            }
        }
    }
}