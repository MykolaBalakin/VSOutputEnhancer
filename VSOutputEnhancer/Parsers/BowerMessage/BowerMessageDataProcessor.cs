using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.BowerMessage {
    internal class BowerMessageDataProcessor : IParsedDataProcessor<BowerMessageData> {
        public IEnumerable<ProcessedParsedData> ProcessData(SnapshotSpan span, BowerMessageData parsedData) {
            throw new NotImplementedException();
        }
    }
}