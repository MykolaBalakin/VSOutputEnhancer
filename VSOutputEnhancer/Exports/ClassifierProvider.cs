using System;
using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Classifiers;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports {
    [Export(typeof(IClassifierProvider))]
    [ContentType(ContentType.BuildOutput)]
    [ContentType(ContentType.BuildOrderOutput)]
    [ContentType(ContentType.DebugOutput)]
#if DEBUG
    [ContentType(ContentType.Output)]
#endif
    internal class ClassifierProvider : IClassifierProvider {
        private readonly IClassifierFactory classifierFactory;

        [ImportingConstructor]
        public ClassifierProvider(IClassifierFactory classifierFactory) {
            this.classifierFactory = classifierFactory;
        }

        public IClassifier GetClassifier(ITextBuffer buffer) {
            return classifierFactory.GetClassifierForContentType(buffer.ContentType);
        }
    }
}
