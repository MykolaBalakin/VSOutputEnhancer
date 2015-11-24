using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer {
    [Export(typeof(IClassifierProvider))]
    [ContentType("BuildOutput")]
    [ContentType("Debug")]
    public class ClassifierProvider : IClassifierProvider {
        private readonly IClassificationTypeRegistryService classificationRegistry;

        [ImportingConstructor]
        public ClassifierProvider(IClassificationTypeRegistryService classificationRegistry) {
            this.classificationRegistry = classificationRegistry;
        }

        public IClassifier GetClassifier(ITextBuffer buffer) {
            if (buffer.ContentType.TypeName.Equals("BuildOutput", StringComparison.OrdinalIgnoreCase)) {
                return buffer.Properties.GetOrCreateSingletonProperty(creator: () => new BuildOutputClassifier(classificationRegistry));
            } else if (buffer.ContentType.TypeName.Equals("Debug", StringComparison.OrdinalIgnoreCase)) {
                return buffer.Properties.GetOrCreateSingletonProperty(creator: () => new DebugClassifier(classificationRegistry));
            }
            return null;
        }
    }
}
