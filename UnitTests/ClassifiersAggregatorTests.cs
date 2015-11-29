using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.Classifiers;
using Balakin.VSOutputEnhancer.UnitTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Classification.Fakes;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ClassifiersAggregatorTests {
        [TestMethod]
        public void GetClassificationSpans() {
            var fullSpan = Utils.CreateSpan("Some text");
            var span1 = new SnapshotSpan(fullSpan.Snapshot, new Span(0, 4));
            var span2 = new SnapshotSpan(fullSpan.Snapshot, new Span(5, 4));

            var classifier1 = new StubIClassifier();
            classifier1.GetClassificationSpansSnapshotSpan = s => new List<ClassificationSpan> {
                new ClassificationSpan(
                    new SnapshotSpan(s.Snapshot, new Span(0, 4)),
                    new ClassificationTypeStub("ClassificationType1"))
            };
            var classifier2 = new StubIClassifier();
            classifier2.GetClassificationSpansSnapshotSpan = s => new List<ClassificationSpan> {
                new ClassificationSpan(
                    new SnapshotSpan(s.Snapshot, new Span(5, 4)),
                    new ClassificationTypeStub("ClassificationType2"))
            };

            var aggregator = new ClassifiersAggregator(classifier1, classifier2);
            var result = aggregator.GetClassificationSpans(fullSpan);
            Assert.AreEqual(2, result.Count);

            var classificationSpan1 = result.SingleOrDefault(s => s.ClassificationType.Classification == "ClassificationType1");
            var classificationSpan2 = result.SingleOrDefault(s => s.ClassificationType.Classification == "ClassificationType2");
            Assert.IsNotNull(classificationSpan1);
            Assert.AreEqual(span1, classificationSpan1.Span);
            Assert.IsNotNull(classificationSpan2);
            Assert.AreEqual(span2, classificationSpan2.Span);
        }

        [TestMethod]
        public void ClassificationChanged() {
            var fullSpan = Utils.CreateSpan("Some text");
            var span1 = new SnapshotSpan(fullSpan.Snapshot, new Span(0, 4));
            var span2 = new SnapshotSpan(fullSpan.Snapshot, new Span(5, 4));

            var classifier1 = new StubIClassifier();
            var classifier2 = new StubIClassifier();

            var invokes = new List<Tuple<Object, ClassificationChangedEventArgs>>();
            var aggregator = new ClassifiersAggregator(classifier1, classifier2);

            // Check for no exception
            classifier1.ClassificationChangedEvent?.Invoke(classifier1, new ClassificationChangedEventArgs(span1));

            aggregator.ClassificationChanged += (sender, e) => invokes.Add(Tuple.Create(sender, e));

            classifier1.ClassificationChangedEvent?.Invoke(classifier1, new ClassificationChangedEventArgs(span1));
            Assert.AreEqual(1, invokes.Count);
            Assert.AreEqual(classifier1, invokes[0].Item1);
            Assert.AreEqual(span1, invokes[0].Item2.ChangeSpan);

            invokes.Clear();
            classifier2.ClassificationChangedEvent?.Invoke(classifier2, new ClassificationChangedEventArgs(span2));
            Assert.AreEqual(1, invokes.Count);
            Assert.AreEqual(classifier2, invokes[0].Item1);
            Assert.AreEqual(span2, invokes[0].Item2.ChangeSpan);
        }
    }
}
