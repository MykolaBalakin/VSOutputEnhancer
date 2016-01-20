using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.NpmMessage {
    internal class NpmMessageDataProcessor : IParsedDataProcessor<NpmMessageData> {
        public IEnumerable<ProcessedParsedData> ProcessData(SnapshotSpan span, NpmMessageData parsedData) {
            throw new NotImplementedException();
        }
    }
}