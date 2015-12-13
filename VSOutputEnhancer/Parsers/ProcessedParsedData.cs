using System;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    public class ProcessedParsedData {
        public ProcessedParsedData(Span span, String classificationName) {
            Span = span;
            ClassificationName = classificationName;
        }

        public Span Span { get; }
        public String ClassificationName { get; }
    }
}