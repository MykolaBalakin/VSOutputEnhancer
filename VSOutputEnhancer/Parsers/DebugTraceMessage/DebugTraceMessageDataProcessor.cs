using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.DebugTraceMessage {
    internal class DebugTraceMessageDataProcessor : IParsedDataProcessor<DebugTraceMessageData> {
        public IEnumerable<ProcessedParsedData> ProcessData(SnapshotSpan span, DebugTraceMessageData data) {
            if (data == null) {
                yield break;
            }

            var classificationType = GetClassificationType(data.Type);
            if (String.IsNullOrEmpty(classificationType)) {
                yield break;
            }

            var resultSpan = data.PrettyMessage.Span;
            var snapshotSpan = new SnapshotSpan(span.Snapshot, resultSpan);
            yield return new ProcessedParsedData(snapshotSpan, classificationType);
        }

        private String GetClassificationType(TraceEventType eventType) {
            switch (eventType) {
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