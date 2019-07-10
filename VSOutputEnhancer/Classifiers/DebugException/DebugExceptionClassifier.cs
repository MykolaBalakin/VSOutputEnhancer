using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Parsers.DebugException;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Classifiers.DebugException
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