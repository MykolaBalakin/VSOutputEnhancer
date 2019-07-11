using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.PublishResult
{
    [Obsolete]
    internal class PublishResultDataProcessor : IParsedDataProcessor<PublishResultData>
    {
        public IEnumerable<ProcessedParsedData> ProcessData(SnapshotSpan span, PublishResultData parsedData)
        {
            if (parsedData == null)
            {
                yield break;
            }

            if (parsedData.Failed == 0)
            {
                yield return new ProcessedParsedData(span.Span, ClassificationType.PublishResultSucceeded);
            }
            else
            {
                yield return new ProcessedParsedData(span.Span, ClassificationType.PublishResultFailed);
            }
        }
    }
}