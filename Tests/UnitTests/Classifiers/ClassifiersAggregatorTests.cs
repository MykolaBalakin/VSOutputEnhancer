using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Balakin.VSOutputEnhancer.Classifiers;
using Balakin.VSOutputEnhancer.Tests.Stubs;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using NSubstitute;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Classifiers
{
    [ExcludeFromCodeCoverage]
    public class ClassifiersAggregatorTests
    {
        [Fact]
        public void GetClassificationSpans()
        {
            var fullSpan = Utils.CreateSpan("Some text");
            var span1 = new SnapshotSpan(fullSpan.Snapshot, new Span(0, 4));
            var span2 = new SnapshotSpan(fullSpan.Snapshot, new Span(5, 4));

            var expectedResult = new[]
            {
                new ClassificationSpan(span1, new ClassificationTypeStub("ClassificationType1")),
                new ClassificationSpan(span2, new ClassificationTypeStub("ClassificationType2"))
            };

            var classifier1 = Substitute.For<IClassifier>();
            classifier1.GetClassificationSpans(fullSpan)
                .Returns(new List<ClassificationSpan>
                {
                    new ClassificationSpan(
                        new SnapshotSpan(fullSpan.Snapshot, new Span(0, 4)),
                        new ClassificationTypeStub("ClassificationType1"))
                });

            var classifier2 = Substitute.For<IClassifier>();
            classifier2.GetClassificationSpans(fullSpan)
                .Returns(new List<ClassificationSpan>
                {
                    new ClassificationSpan(
                        new SnapshotSpan(fullSpan.Snapshot, new Span(5, 4)),
                        new ClassificationTypeStub("ClassificationType2"))
                });

            var aggregator = new ClassifiersAggregator(classifier1, classifier2);
            var actualResult = aggregator.GetClassificationSpans(fullSpan);

            actualResult.ShouldAllBeEquivalentTo(expectedResult);
        }

        [Fact]
        public void ClassificationChanged()
        {
            var fullSpan = Utils.CreateSpan("Some text");
            var span1 = new SnapshotSpan(fullSpan.Snapshot, new Span(0, 4));
            var span2 = new SnapshotSpan(fullSpan.Snapshot, new Span(5, 4));

            var classifier1 = new ClassifierStub();
            var classifier2 = new ClassifierStub();

            var invokes = new List<Tuple<Object, ClassificationChangedEventArgs>>();
            var aggregator = new ClassifiersAggregator(classifier1, classifier2);

            aggregator.ClassificationChanged += (sender, e) => invokes.Add(Tuple.Create(sender, e));

            classifier1.InvokeClassificationChanged(new ClassificationChangedEventArgs(span1));

            // TODO: Refactor this code to use ShouldAllBeEquivalent
            invokes.Should().HaveCount(1);
            invokes.Single().Item1.Should().Be(classifier1);
            invokes.Single().Item2.ChangeSpan.Should().Be(span1);

            invokes.Clear();
            classifier2.InvokeClassificationChanged(new ClassificationChangedEventArgs(span2));

            // TODO: Refactor this code to use ShouldAllBeEquivalent
            invokes.Should().HaveCount(1);
            invokes.Single().Item1.Should().Be(classifier2);
            invokes.Single().Item2.ChangeSpan.Should().Be(span2);
        }
    }
}