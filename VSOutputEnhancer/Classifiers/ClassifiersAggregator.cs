using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Classifiers {
    internal class ClassifiersAggregator : IClassifier {
        private readonly IList<IClassifier> classifiers;

        public ClassifiersAggregator(params IClassifier[] classifiers)
            : this((IEnumerable<IClassifier>)classifiers) {
        }

        public ClassifiersAggregator(IEnumerable<IClassifier> classifiers) {
            this.classifiers = classifiers.ToList();
            foreach (var classifier in this.classifiers) {
                classifier.ClassificationChanged += Classifier_ClassificationChanged;
            }
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span) {
            return classifiers.SelectMany(c => c.GetClassificationSpans(span)).ToList();
        }

        private void Classifier_ClassificationChanged(Object sender, ClassificationChangedEventArgs e) {
            ClassificationChanged?.Invoke(sender, e);
        }
    }
}
