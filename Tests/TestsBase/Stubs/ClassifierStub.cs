using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Tests.Stubs
{
    [ExcludeFromCodeCoverage]
    public class ClassifierStub : IClassifier
    {
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            throw new NotImplementedException();
        }

        public void InvokeClassificationChanged(ClassificationChangedEventArgs eventArgs)
        {
            ClassificationChanged?.Invoke(this, eventArgs);
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
    }
}
