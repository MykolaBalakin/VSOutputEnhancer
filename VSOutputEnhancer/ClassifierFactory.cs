using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Resources;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer {
    [Export]
    public class ClassifierFactory {
        private readonly IClassificationTypeRegistryService classificationTypeRegistryService;
        private readonly ConcurrentDictionary<String, IClassifier> classifiers;

        [ImportingConstructor]
        public ClassifierFactory(IClassificationTypeRegistryService classificationTypeRegistryService) {
            this.classificationTypeRegistryService = classificationTypeRegistryService;
            classifiers = new ConcurrentDictionary<String, IClassifier>();
        }

        public IClassifier GetClassifierForContentType(IContentType contentType) {
            var key = GetClassifierKey(contentType);
            var classifier = classifiers.GetOrAdd(key, k => CreateClassifierForContentType(contentType));
            return classifier;
        }

        private IClassifier CreateClassifierForContentType(IContentType contentType) {
            if (contentType.TypeName.Equals("BuildOutput", StringComparison.OrdinalIgnoreCase)) {
                return new BuildOutputClassifier(classificationTypeRegistryService);
            }
            if (contentType.TypeName.Equals("Debug", StringComparison.OrdinalIgnoreCase)) {
                return new DebugClassifier(classificationTypeRegistryService);
            }
            return null;
        }

        private String GetClassifierKey(IContentType contentType) {
            return contentType.TypeName;
        }
    }
}