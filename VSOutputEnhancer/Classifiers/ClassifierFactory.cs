using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Classifiers
{
    [Export(typeof(IClassifierFactory))]
    internal class ClassifierFactory : IClassifierFactory
    {
        private readonly IClassificationTypeService classificationTypeService;
        private readonly IParsersConfigurationService parsersConfigurationService;
        private readonly ConcurrentDictionary<String, IClassifier> classifiers;

        [ImportingConstructor]
        public ClassifierFactory(IClassificationTypeService classificationTypeService, IParsersConfigurationService parsersConfigurationService)
        {
            this.classificationTypeService = classificationTypeService;
            this.parsersConfigurationService = parsersConfigurationService;
            classifiers = new ConcurrentDictionary<String, IClassifier>();
        }

        public IClassifier GetClassifierForContentType(IContentType contentType)
        {
            var key = GetClassifierKey(contentType);
            var classifier = classifiers.GetOrAdd(key, k => CreateClassifierForContentType(contentType));
            return classifier;
        }

        private IClassifier CreateClassifierForContentType(IContentType contentType)
        {
            var configuration = parsersConfigurationService.GetParsers(contentType);
            if (configuration.Count == 0)
            {
                Trace.TraceWarning($"Can not create classifier for content type {contentType.TypeName} (base types: {String.Join(", ", contentType.BaseTypes.Select(t => t.TypeName))})");
                return null;
            }
            if (configuration.Count == 1)
            {
                return CreateClassifierFromConfiguration(configuration.Single());
            }
            return new ClassifiersAggregator(configuration.Select(CreateClassifierFromConfiguration));
        }

        private IClassifier CreateClassifierFromConfiguration(ParserConfiguration configuration)
        {
            var parser = Activator.CreateInstance(configuration.Parser);
            var dataProcessor = Activator.CreateInstance(configuration.DataProcessor);

            var classifierType = typeof(ParserBasedClassifier<>).MakeGenericType(configuration.Data);
            return (IClassifier) Activator.CreateInstance(classifierType, parser, dataProcessor, classificationTypeService);
        }

        private String GetClassifierKey(IContentType contentType)
        {
            return contentType.TypeName;
        }
    }
}