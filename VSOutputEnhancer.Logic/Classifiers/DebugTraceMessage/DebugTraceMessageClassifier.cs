using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers.DebugTraceMessage
{
    [Export(typeof(ISpanClassifier))]
    public class DebugTraceMessageClassifier : ParserBasedSpanClassifier<DebugTraceMessageData>
    {
        [ImportingConstructor]
        public DebugTraceMessageClassifier(IParser<DebugTraceMessageData> parser) : base(parser)
        {
        }

        public override IEnumerable<String> ContentTypes { get; } = new[]
        {
            ContentType.DebugOutput,
        };

        protected override IEnumerable<ProcessedParsedData> Classify(SnapshotSpan span, DebugTraceMessageData parsedData)
        {
            var classificationType = GetClassificationType(parsedData.Type);
            if (String.IsNullOrEmpty(classificationType))
            {
                yield break;
            }

            var resultSpan = parsedData.PrettyMessage.Span;
            var snapshotSpan = new SnapshotSpan(span.Snapshot, resultSpan);
            yield return new ProcessedParsedData(snapshotSpan, classificationType);
        }

        private String GetClassificationType(TraceEventType eventType)
        {
            switch (eventType)
            {
                case TraceEventType.Critical:
                case TraceEventType.Error:
                    return ClassificationType.DebugTraceError;
                case TraceEventType.Warning:
                    return ClassificationType.DebugTraceWarning;
                case TraceEventType.Information:
                    return ClassificationType.DebugTraceInformation;
                default:
                    return null;
            }
        }
    }
}