using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Classifiers
{
    [ExcludeFromCodeCoverage]
    public class BowerOutputClassifierTests : ClassifierTestsBase
    {
        protected override IClassifier CreateClassifier()
        {
            return Utils.CreateBowerOutputClassifier();
        }

        [Fact]
        public void PackageNotFound()
        {
            const String notFoundError = "bower bootstrap1#3.3.5       ENOTFOUND Package bootstrap1 not found\r\n";

            var span = Utils.CreateSpan(notFoundError);
            var classifier = CreateClassifier();
            var result = classifier.GetClassificationSpans(span);

            // TODO: Use ShouldAllBeEquivalentTo here
            result.Should().HaveCount(1);
            var classificationSpan = result.Single();
            classificationSpan.Span.ShouldBeEquivalentTo(new SnapshotSpan(span.Snapshot, 39, 28));
            classificationSpan.ClassificationType.Classification.Should().Be(ClassificationType.BowerMessageError);
        }
    }
}