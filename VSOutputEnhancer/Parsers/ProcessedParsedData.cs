using System;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    internal class ProcessedParsedData {
        public ProcessedParsedData(Span span, String classification) {
            Span = span;
            Classification = classification;
        }

        public Span Span { get; }
        public String Classification { get; }
    }
}