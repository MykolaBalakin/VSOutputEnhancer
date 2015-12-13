using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.BuildResult {
    internal class BuildResultDataProcessor : IParsedDataProcessor<BuildResultData> {
        public IEnumerable<ProcessedParsedData> ProcessData(SnapshotSpan span, BuildResultData parsedData) {
            if (parsedData == null) {
                yield break;
            }

            if (parsedData.Failed == 0) {
                yield return new ProcessedParsedData(span, ClassificationType.BuildResultSucceeded);
            } else {
                yield return new ProcessedParsedData(span, ClassificationType.BuildResultFailed);
            }
        }
    }
}