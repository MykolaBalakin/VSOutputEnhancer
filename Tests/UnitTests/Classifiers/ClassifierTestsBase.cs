using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Classifiers
{
    [ExcludeFromCodeCoverage]
    public abstract class ClassifierTestsBase
    {
        protected abstract IClassifier CreateClassifier();

        protected void Test(SnapshotSpan span, IReadOnlyCollection<ClassificationSpan> expectedResult)
        {
            var classifier = CreateClassifier();
            var actualResult = classifier.GetClassificationSpans(span);

            actualResult.ShouldAllBeEquivalentTo(expectedResult);
        }
    }
}