using System;
using System.Collections.Generic;
using System.Linq;
using Balakin.VSOutputEnhancer.Parsers;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Classifiers
{
    internal class ParserBasedClassifier<T> : IClassifier
        where T : ParsedData
    {
        private readonly IParser<T> parser;
        private readonly IParsedDataProcessor<T> processor;
        private readonly IClassificationTypeService classificationTypeService;

        public ParserBasedClassifier(IParser<T> parser, IParsedDataProcessor<T> processor, IClassificationTypeService classificationTypeService)
        {
            this.parser = parser;
            this.processor = processor;
            this.classificationTypeService = classificationTypeService;
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            T parsedData;
            var parsed = parser.TryParse(span, out parsedData);
            if (!parsed)
            {
                return new List<ClassificationSpan>(0);
            }
            var processedData = processor.ProcessData(span, parsedData);
            var classificationSpans = processedData
                .Select(d => CreateClassificationSpan(span, d))
                .ToList();
            return classificationSpans;
        }

        private ClassificationSpan CreateClassificationSpan(SnapshotSpan originalSpan, ProcessedParsedData data)
        {
            var classificationType = classificationTypeService.GetClassificationType(data.ClassificationName);
            var span = new SnapshotSpan(originalSpan.Snapshot, data.Span);
            return new ClassificationSpan(span, classificationType);
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
    }
}