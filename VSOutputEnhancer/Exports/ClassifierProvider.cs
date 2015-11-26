using System;
using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Classifiers;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports {
    [Export(typeof(IClassifierProvider))]
    [ContentType("BuildOutput")]
    [ContentType("Debug")]
    internal class ClassifierProvider : IClassifierProvider {
        private readonly ClassifierFactory classifierFactory;

        [ImportingConstructor]
        public ClassifierProvider(ClassifierFactory classifierFactory) {
            this.classifierFactory = classifierFactory;
        }

        public IClassifier GetClassifier(ITextBuffer buffer) {
            return classifierFactory.GetClassifierForContentType(buffer.ContentType);
        }
    }
}
