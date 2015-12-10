using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    public interface IParsedDataProcessor<in T> where T : ParsedData {
        IEnumerable<ProcessedParsedData> ProcessData(SnapshotSpan span, T parsedData);
    }
}