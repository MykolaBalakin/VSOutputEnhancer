using System;
using System.Collections.Generic;
using System.Linq;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Events;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Logic
{
    public class Classifier : IClassifier, IEventHandler<ClassificationChangedEvent>
    {
        private readonly IDispatcher dispatcher;
        private readonly IReadOnlyCollection<ISpanClassifier> spanClassifiers;
        private readonly IClassificationTypeService classificationTypeService;

        public Classifier(
            IDispatcher dispatcher,
            IReadOnlyCollection<ISpanClassifier> spanClassifiers,
            IClassificationTypeService classificationTypeService)
        {
            this.dispatcher = dispatcher;
            this.spanClassifiers = spanClassifiers;
            this.classificationTypeService = classificationTypeService;
        }

        public IEnumerable<String> ContentTypes => throw new NotSupportedException();

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var result = new List<ClassificationSpan>();
            foreach (var classifier in spanClassifiers)
            {
                var classifierResult = classifier.Classify(span, dispatcher);
                var classificationSpans = classifierResult.Select(r => CreateClassificationSpan(span, r));
                result.AddRange(classificationSpans);
            }

            return result;
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        private ClassificationSpan CreateClassificationSpan(SnapshotSpan originalSpan, ProcessedParsedData data)
        {
            var classificationType = classificationTypeService.GetClassificationType(data.ClassificationName);
            var span = new SnapshotSpan(originalSpan.Snapshot, data.Span);
            return new ClassificationSpan(span, classificationType);
        }

        public void Handle(IDispatcher dispatcher, ClassificationChangedEvent @event)
        {
            var eventArgs = new ClassificationChangedEventArgs(@event.Span);
            ClassificationChanged?.Invoke(this, eventArgs);
        }
    }
}