using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.PublishResult
{
    [Export(typeof(ISpanClassifier))]
    public class PublishResultClassifier : ParserBasedSpanClassifier<PublishResultData>
    {
        [ImportingConstructor]
        public PublishResultClassifier(IParser<PublishResultData> parser) : base(parser)
        {
        }

        public override IEnumerable<String> ContentTypes { get; } = new[]
        {
            ContentType.BuildOutput,
            ContentType.BuildOrderOutput
        };

        protected override IEnumerable<ProcessedParsedData> Classify(SnapshotSpan span, PublishResultData parsedData, DataContainer data)
        {
            if (parsedData.Failed == 0)
            {
                yield return new ProcessedParsedData(span, ClassificationType.PublishResultSucceeded);
            }
            else
            {
                yield return new ProcessedParsedData(span, ClassificationType.PublishResultFailed);
            }
        }
    }
}