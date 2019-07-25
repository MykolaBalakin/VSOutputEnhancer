using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.NpmResult
{
    [Export(typeof(ISpanClassifier))]
    public class NpmResultClassifier : ParserBasedSpanClassifier<NpmResultData>
    {
        [ImportingConstructor]
        public NpmResultClassifier(IParser<NpmResultData> parser) : base(parser)
        {
        }

        public override IEnumerable<String> ContentTypes { get; } = new[]
        {
            ContentType.Output,
            ContentType.BuildOutput,
            ContentType.BuildOrderOutput
        };

        protected override IEnumerable<ProcessedParsedData> Classify(SnapshotSpan span, NpmResultData parsedData)
        {
            if (parsedData.ExitCode == 0)
            {
                yield return new ProcessedParsedData(span, ClassificationType.NpmResultSucceeded);
            }
            else
            {
                yield return new ProcessedParsedData(span, ClassificationType.NpmResultFailed);
            }
        }
    }
}