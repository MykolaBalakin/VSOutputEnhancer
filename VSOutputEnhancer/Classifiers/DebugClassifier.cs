using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Classifiers {
    [Obsolete]
    internal class DebugClassifier : IClassifier {
        internal DebugClassifier(IClassificationTypeRegistryService registry) {
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span) {
            throw new NotImplementedException();
        }
    }
}