using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.BuildProjectHeader
{
    internal class BuildProjectHeaderProcessor : IParsedDataProcessor<BuildProjectHeaderData>
    {
        public IEnumerable<ProcessedParsedData> ProcessData(SnapshotSpan span, BuildProjectHeaderData parsedData)
        {
            if (parsedData == null)
            {
                yield break;
            }

            yield return new ProcessedParsedData(span, ClassificationType.BuildProjectHeader);
        }
    }
}