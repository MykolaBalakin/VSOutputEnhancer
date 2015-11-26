using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.Parsers;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer {
    internal class ParserBasedClassifier<T> : IClassifier
        where T : ParsedData {

        private readonly IParser<T> parser;
        private readonly IParsedDataProcessor<T> processor;

        public ParserBasedClassifier(IParser<T> parser, IParsedDataProcessor<T> processor) {
            this.parser = parser;
            this.processor = processor;
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span) {
            T parsedData;
            var parsed = parser.TryParse(span, out parsedData);
            if (!parsed) {
                return null;
            }
            var processedData = processor.ProcessData(span, parsedData);
            var classificationSpans = processedData
                .Select(CreateClassificationSpan)
                .ToList();
            return classificationSpans;
        }

        private ClassificationSpan CreateClassificationSpan(ProcessedParsedData data) {
            throw new NotImplementedException();
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
    }
}
