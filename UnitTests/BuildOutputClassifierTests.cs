using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.UnitTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BuildOutputClassifierTests : ClassifierTestsBase {
        [TestMethod]
        public void BuildFailed() {
            const String buildCompleteMessage = "========== Build: 5 succeeded, 1 failed, 3 up-to-date, 2 skipped ==========\r\n";

            var span = Utils.CreateSpan(buildCompleteMessage);
            var registry = Utils.CreateClassificationTypeRegistryService();
            var classifier = new BuildOutputClassifier(registry);
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(span, classificationSpan.Span);
            Assert.AreEqual(ClassificationType.BuildResultFailed, classificationSpan.ClassificationType.Classification);
        }

        [TestMethod]
        public void BuildSucceeded() {
            const String buildCompleteMessage = "========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========\r\n";

            var span = Utils.CreateSpan(buildCompleteMessage);
            var registry = Utils.CreateClassificationTypeRegistryService();
            var classifier = new BuildOutputClassifier(registry);
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(span, classificationSpan.Span);
            Assert.AreEqual(ClassificationType.BuildResultSucceeded, classificationSpan.ClassificationType.Classification);
        }

        [TestMethod]
        public void PublishFailed() {
            const String publishCompleteMessage = "========== Publish: 0 succeeded, 1 failed, 0 skipped ==========\r\n";

            var span = Utils.CreateSpan(publishCompleteMessage);
            var registry = Utils.CreateClassificationTypeRegistryService();
            var classifier = new BuildOutputClassifier(registry);
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(span, classificationSpan.Span);
            Assert.AreEqual(ClassificationType.PublishResultFailed, classificationSpan.ClassificationType.Classification);
        }

        [TestMethod]
        public void PublishSucceeded() {
            const String publishCompleteMessage = "========== Publish: 1 succeeded, 0 failed, 0 skipped ==========\r\n";

            var span = Utils.CreateSpan(publishCompleteMessage);
            var registry = Utils.CreateClassificationTypeRegistryService();
            var classifier = new BuildOutputClassifier(registry);
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(span, classificationSpan.Span);
            Assert.AreEqual(ClassificationType.PublishResultSucceeded, classificationSpan.ClassificationType.Classification);
        }

        [TestMethod]
        public void CompilationWarning() {
            const String publishCompleteMessage = "1>C:\\Sources\\GitHub\\VSOutputEnhancer\\VSOutputEnhancer\\ClassificationType.cs(29,53,29,83): warning CS0169: The field 'ClassificationType.buildResultSucceededDefinition' is never used\r\n";

            var span = Utils.CreateSpan(publishCompleteMessage);
            var registry = Utils.CreateClassificationTypeRegistryService();
            var classifier = new BuildOutputClassifier(registry);
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(new SnapshotSpan(span.Snapshot, 90, 91), classificationSpan.Span);
            Assert.AreEqual(ClassificationType.BuildMessageWarning, classificationSpan.ClassificationType.Classification);
        }
    }
}
