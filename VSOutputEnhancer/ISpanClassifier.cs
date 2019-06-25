using System;
using System.Collections.Generic;
using Balakin.VSOutputEnhancer.Parsers;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer
{
    public interface ISpanClassifier
    {
        IEnumerable<String> ContentTypes { get; }
        IEnumerable<ProcessedParsedData> Classify(SnapshotSpan span, IDispatcher dispatcher);
    }
}