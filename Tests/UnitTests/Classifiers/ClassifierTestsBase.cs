using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Tests.Base.Stubs;
using Balakin.VSOutputEnhancer.Tests.Stubs;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Classifiers
{
    [ExcludeFromCodeCoverage]
    public abstract class ClassifierTestsBase
    {
        protected abstract String GetContentType();

        protected void Test(SnapshotSpan span, IReadOnlyCollection<ClassificationSpan> expectedResult)
        {
            var classifier = CreateClassifier();
            var actualResult = classifier.GetClassificationSpans(span);

            actualResult.ShouldAllBeEquivalentTo(expectedResult);
        }

        private IClassifier CreateClassifier()
        {
            var exportProvider = ExportProviderFactory.Create();
            var classifierProvider = exportProvider.GetExport<IClassifierProvider>().Value;

            var contentType = GetContentType();
            var classifier = classifierProvider.GetClassifier(new TextBufferStub(contentType));
            return classifier;
        }
    }
}