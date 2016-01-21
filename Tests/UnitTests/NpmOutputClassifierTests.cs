using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests {
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class NpmOutputClassifierTests : ClassifierTestsBase {
        [TestMethod]
        public void NpmWarning() {
            const String npmWarnMessage = "npm WARN package.json ASP.NET@0.0.0 No description\r\n";

            var span = Utils.CreateSpan(npmWarnMessage);
            var classifier = CreateClassifier();
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(new SnapshotSpan(span.Snapshot, 9, 41), classificationSpan.Span);
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
            Assert.AreEqual(new SnapshotSpan(span.Snapshot, 9, 13), classificationSpan.Span);
            Assert.AreEqual(ClassificationType.NpmMessageError, classificationSpan.ClassificationType.Classification);
        }

        [TestMethod]
        public void NpmResultSuccess() {
            const String npmExitCode0 = "====npm command completed with exit code 0====\r\n";

            var span = Utils.CreateSpan(npmExitCode0);
            var classifier = CreateClassifier();
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(span, classificationSpan.Span);
            Assert.AreEqual(ClassificationType.NpmResultSuccessed, classificationSpan.ClassificationType.Classification);
        }

        [TestMethod]
        public void NpmResultFailed() {
            const String npmExitCode1 = "====npm command completed with exit code 1====\r\n";

            var span = Utils.CreateSpan(npmExitCode1);
            var classifier = CreateClassifier();
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(span, classificationSpan.Span);
            Assert.AreEqual(ClassificationType.NpmResultFailed, classificationSpan.ClassificationType.Classification);
        }

        protected override IClassifier CreateClassifier() {
            return Utils.CreateNpmOutputClassifier();
        }
    }
}
