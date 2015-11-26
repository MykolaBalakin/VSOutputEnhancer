using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    internal class DebugExceptionDataProcessor : IParsedDataProcessor<DebugExceptionData> {
        public IEnumerable<ProcessedParsedData> ProcessData(SnapshotSpan span, DebugExceptionData parsedData) {
            if (parsedData == null) {
                yield break;
            }
            yield return new ProcessedParsedData(span, ClassificationType.DebugException);
        }
    }
}