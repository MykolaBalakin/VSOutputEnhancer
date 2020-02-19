using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.BuildActionStart
{
    [Export(typeof(ISpanClassifier))]
    public class BuildActionStartClassifier : ParserBasedSpanClassifier<BuildActionStartData>
    {
        [ImportingConstructor]
        public BuildActionStartClassifier(IParser<BuildActionStartData> parser) : base(parser)
        {
        }

        public override IEnumerable<String> ContentTypes { get; } = new[]
        {
            ContentType.BuildOutput,
            ContentType.BuildOrderOutput
        };

        protected override IEnumerable<ProcessedParsedData> Classify(SnapshotSpan span, BuildActionStartData parsedData)
        {
            yield return new ProcessedParsedData(parsedData.FullMessage.Span, ClassificationType.BuildActionStarted);
        }
    }
}