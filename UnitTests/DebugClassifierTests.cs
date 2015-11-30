using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DebugClassifierTests : ClassifierTestsBase {
        [TestMethod]
        public void TraceError() {
            const String traceErrorMessage = "VSOutputEnhancerDemo.vshost.exe Error: 0 : Trace error message\r\n";

            var span = Utils.CreateSpan(traceErrorMessage);
            var classifier = CreateClassifier();
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(new SnapshotSpan(span.Snapshot, 32, 30), classificationSpan.Span);
            Assert.AreEqual(ClassificationType.DebugTraceError, classificationSpan.ClassificationType.Classification);
        }

        [TestMethod]
        public void TraceError2() {
            const String fullText = "Some other text\r\nVSOutputEnhancerDemo.vshost.exe Error: 0 : Trace error message\r\n";
            const String traceErrorMessage = "VSOutputEnhancerDemo.vshost.exe Error: 0 : Trace error message\r\n";
            const String highlightedMessage = "Error: 0 : Trace error message";

            var span = Utils.CreateSpan(fullText);
            span = new SnapshotSpan(span.Snapshot, fullText.Length - traceErrorMessage.Length, traceErrorMessage.Length);
            Assert.AreEqual(span.GetText(), traceErrorMessage);
            var classifier = CreateClassifier();
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(highlightedMessage, classificationSpan.Span.GetText());
            Assert.AreEqual(ClassificationType.DebugTraceError, classificationSpan.ClassificationType.Classification);
        }

        [TestMethod]
        public void TraceInformation() {
            const String traceErrorMessage = "VSOutputEnhancerDemo.vshost.exe Information: 0 : Trace information message\r\n";

            var span = Utils.CreateSpan(traceErrorMessage);
            var classifier = CreateClassifier();
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(new SnapshotSpan(span.Snapshot, 32, 42), classificationSpan.Span);
            Assert.AreEqual(ClassificationType.DebugTraceInformation, classificationSpan.ClassificationType.Classification);
        }

        [TestMethod]
        public void TraceWarning() {
            const String traceErrorMessage = "VSOutputEnhancerDemo.vshost.exe Warning: 0 : Trace warning message\r\n";

            var span = Utils.CreateSpan(traceErrorMessage);
            var classifier = CreateClassifier();
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(new SnapshotSpan(span.Snapshot, 32, 34), classificationSpan.Span);
            Assert.AreEqual(ClassificationType.DebugTraceWarning, classificationSpan.ClassificationType.Classification);
        }

        [TestMethod]
        public void TraceException() {
            const String traceErrorMessage = "Exception thrown: 'System.Exception' in VSOutputEnhancerDemo.exe\r\n";

            var span = Utils.CreateSpan(traceErrorMessage);
            var classifier = CreateClassifier();
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(span, classificationSpan.Span);
            Assert.AreEqual(ClassificationType.DebugException, classificationSpan.ClassificationType.Classification);
        }

        protected override IClassifier CreateClassifier() {
            return Utils.CreateDebugClassifier();
        }
    }
}
