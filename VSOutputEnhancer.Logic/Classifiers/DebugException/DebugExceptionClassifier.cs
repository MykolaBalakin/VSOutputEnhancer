using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.DebugException
{
    [Export(typeof(ISpanClassifier))]
    public class DebugExceptionClassifier : ParserBasedSpanClassifier<DebugExceptionData>
    {
        [ImportingConstructor]
        public DebugExceptionClassifier(IParser<DebugExceptionData> parser) : base(parser)
        {
        }

        public override IEnumerable<String> ContentTypes { get; } = new[]
        {
            ContentType.DebugOutput,
        };

        protected override IEnumerable<ProcessedParsedData> Classify(SnapshotSpan span, DebugExceptionData parsedData)
        {
            yield return new ProcessedParsedData(span, ClassificationType.DebugException);
        }
    }
}