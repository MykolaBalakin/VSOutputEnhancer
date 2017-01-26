using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.NpmResult
{
    internal class NpmResultDataProcessor : IParsedDataProcessor<NpmResultData>
    {
        public IEnumerable<ProcessedParsedData> ProcessData(SnapshotSpan span, NpmResultData parsedData)
        {
            if (parsedData == null)
            {
                yield break;
            }

            if (parsedData.ExitCode == 0)
            {
                yield return new ProcessedParsedData(span, ClassificationType.NpmResultSucceeded);
            }
            else
            {
                yield return new ProcessedParsedData(span, ClassificationType.NpmResultFailed);
            }
        }
    }
}