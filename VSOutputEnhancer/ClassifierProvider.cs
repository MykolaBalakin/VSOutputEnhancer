using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Balakin.VSOutputEnhancer.Exports;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer
{
    [Export(typeof(IClassifierProvider))]
    [ContentType(ContentType.BuildOutput)]
    [ContentType(ContentType.BuildOrderOutput)]
    [ContentType(ContentType.DebugOutput)]
    // For npm logs
    [ContentType(ContentType.Output)]
#if DEBUG
    [ContentType(ContentType.Text)]
#endif
    public class ClassifierProvider : IClassifierProvider
    {
        private readonly IClassificationTypeService classificationTypeService;
        private readonly IEnumerable<ISpanClassifier> spanClassifiers;
        private readonly IEnumerable<IEventHandler> eventHandlers;

        private readonly OldClassifierProvider oldClassifierProvider;
        private readonly HashSet<String> oldContentTypes = new HashSet<String>
        {
            ContentType.Output,
            ContentType.Text
        };

        [ImportingConstructor]
        public ClassifierProvider(
            IClassificationTypeService classificationTypeService,
            [ImportMany] IEnumerable<ISpanClassifier> spanClassifiers,
            [ImportMany] IEnumerable<IEventHandler> eventHandlers,
            OldClassifierProvider oldClassifierProvider)
        {
            this.spanClassifiers = spanClassifiers;
            this.eventHandlers = eventHandlers;
            this.oldClassifierProvider = oldClassifierProvider;
            this.classificationTypeService = classificationTypeService;
        }

        public IClassifier GetClassifier(ITextBuffer textBuffer)
        {
            var contentType = textBuffer.ContentType;
            if (oldContentTypes.Contains(contentType.TypeName))
            {
                return oldClassifierProvider.GetClassifier(textBuffer);
            }

            var classifiers = GetSpanClassifiers(contentType);
            var dispatcher = CreateDispatcher(contentType);
            var classifier = new Classifier(dispatcher, classifiers, classificationTypeService);
            return classifier;
        }

        private IReadOnlyCollection<ISpanClassifier> GetSpanClassifiers(IContentType contentType)
        {
            return FilterByContentType(spanClassifiers, c => c.ContentTypes, contentType).ToArray();
        }

        private Dispatcher CreateDispatcher(IContentType contentType)
        {
            var dispatcher = new Dispatcher();
            var handlers = FilterByContentType(eventHandlers, h => h.ContentTypes, contentType);
            foreach (var handler in handlers)
            {
                dispatcher.AddHandler(handler);
            }

            return dispatcher;
        }

        private IEnumerable<T> FilterByContentType<T>(
            IEnumerable<T> collection,
            Func<T, IEnumerable<String>> getContentTypes,
            IContentType contentType)
        {
            return collection.Where(item => getContentTypes(item).Any(t => IsApplicable(contentType, t)));
        }

        private Boolean IsApplicable(IContentType target, String contentType)
        {
            if (String.Equals(target.TypeName, contentType, StringComparison.Ordinal))
            {
                return true;
            }

            return target.BaseTypes.Any(baseType => IsApplicable(baseType, contentType));
        }
    }
}