using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.NpmResult {
    internal class NpmResultDataProcessor : IParsedDataProcessor<NpmResultData> {
        public IEnumerable<ProcessedParsedData> ProcessData(SnapshotSpan span, NpmResultData parsedData) {
            throw new NotImplementedException();
        }
    }
}