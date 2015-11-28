using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Parsers.BuildResult;
using Balakin.VSOutputEnhancer.Parsers.DebugException;
using Balakin.VSOutputEnhancer.Parsers.DebugTraceMessage;
using Balakin.VSOutputEnhancer.Parsers.PublishResult;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Classifiers {
    [Export]
    internal class ClassifierFactory {
        private readonly IClassificationTypeRegistryService classificationTypeRegistryService;
        private readonly IClassificationTypeService classificationTypeService;
        private readonly ConcurrentDictionary<String, IClassifier> classifiers;

        [ImportingConstructor]
        public ClassifierFactory(IClassificationTypeRegistryService classificationTypeRegistryService, IClassificationTypeService classificationTypeService) {
            this.classificationTypeRegistryService = classificationTypeRegistryService;
            this.classificationTypeService = classificationTypeService;
            classifiers = new ConcurrentDictionary<String, IClassifier>();
        }

        public IClassifier GetClassifierForContentType(IContentType contentType) {
            var key = GetClassifierKey(contentType);
            var classifier = classifiers.GetOrAdd(key, k => CreateClassifierForContentType(contentType));
            return classifier;
        }

        // TODO: Refactor this
        // Should somewhow automaticaly find all needed parsers and processors for each content type
        private IClassifier CreateClassifierForContentType(IContentType contentType) {
            if (contentType.TypeName.Equals(ContentType.BuildOutput, StringComparison.OrdinalIgnoreCase)) {
                var oldClassifier = new BuildOutputClassifier(classificationTypeRegistryService);
                var publishResultClassifier = new ParserBasedClassifier<PublishResultData>(new PublishResultParser(), new PublishResultDataProcessor(), classificationTypeService);
                var buildResultClassifier = new ParserBasedClassifier<BuildResultData>(new BuildResultParser(), new BuildResultDataProcessor(), classificationTypeService);

                var buildClassifier = new ClassifiersAggregator(buildResultClassifier, publishResultClassifier, oldClassifier);
                return buildClassifier;
            }
            if (contentType.TypeName.Equals(ContentType.DebugOutput, StringComparison.OrdinalIgnoreCase)) {
                var exceptionClassifier = new ParserBasedClassifier<DebugExceptionData>(new DebugExceptionParser(), new DebugExceptionDataProcessor(), classificationTypeService);
                var traceMessageClassifier = new ParserBasedClassifier<DebugTraceMessageData>(new DebugTraceMessageParser(), new DebugTraceMessageDataProcessor(), classificationTypeService);

                var debugClassifier = new ClassifiersAggregator(exceptionClassifier, traceMessageClassifier);
                return debugClassifier;
            }
            return null;
        }

        private String GetClassifierKey(IContentType contentType) {
            return contentType.TypeName;
        }
    }
}