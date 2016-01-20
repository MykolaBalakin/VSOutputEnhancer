using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BuildOutputClassifierTests : ClassifierTestsBase {
        [TestMethod]
        public void BuildFailed() {
            const String buildCompleteMessage = "========== Build: 5 succeeded, 1 failed, 3 up-to-date, 2 skipped ==========\r\n";

            var span = Utils.CreateSpan(buildCompleteMessage);
            var classifier = CreateClassifier();
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
            var classifier = CreateClassifier();
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
            var classifier = CreateClassifier();
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
            var classifier = CreateClassifier();
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(span, classificationSpan.Span);
            Assert.AreEqual(ClassificationType.PublishResultSucceeded, classificationSpan.ClassificationType.Classification);
        }

        [TestMethod]
        public void CompilationWarning() {
            const String compilationWarningMessage = "1>C:\\Sources\\GitHub\\VSOutputEnhancer\\VSOutputEnhancer\\ClassificationType.cs(29,53,29,83): warning CS0169: The field 'ClassificationType.BuildResultSucceededDefinition' is never used\r\n";

            var span = Utils.CreateSpan(compilationWarningMessage);
            var classifier = CreateClassifier();
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(new SnapshotSpan(span.Snapshot, 90, 91), classificationSpan.Span);
            Assert.AreEqual(ClassificationType.BuildMessageWarning, classificationSpan.ClassificationType.Classification);
        }

        [TestMethod]
        public void CompilationError() {
            const String compilationErrorMessage = "1>C:\\Sources\\GitHub\\VSOutputEnhancer\\UnitTests\\BuildOutputClassifierTests.cs(91,64,91,65): error CS1026: ) expected\r\n";

            var span = Utils.CreateSpan(compilationErrorMessage);
            var classifier = CreateClassifier();
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(new SnapshotSpan(span.Snapshot, 91, 24), classificationSpan.Span);
            Assert.AreEqual(ClassificationType.BuildMessageError, classificationSpan.ClassificationType.Classification);
        }

        [TestMethod]
        public void NpmWarning() {
            const String npmWarnMessage = "npm WARN package.json ASP.NET@0.0.0 No description\r\n";

            var span = Utils.CreateSpan(npmWarnMessage);
            var classifier = CreateClassifier();
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1,result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(new SnapshotSpan(span.Snapshot, 9, 43), classificationSpan.Span);
            Assert.AreEqual(ClassificationType.NpmMessageWarning, classificationSpan.ClassificationType.Classification);
        }

        [TestMethod]
        public void NpmError() {
            const String npmWarnMessage = "npm ERR! 404 Not Found\r\n";

            var span = Utils.CreateSpan(npmWarnMessage);
            var classifier = CreateClassifier();
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(new SnapshotSpan(span.Snapshot, 9, 15), classificationSpan.Span);
            Assert.AreEqual(ClassificationType.NpmMessageError, classificationSpan.ClassificationType.Classification);
        }

        protected override IClassifier CreateClassifier() {
            return Utils.CreateBuildOutputClassifier();
        }
    }
}
