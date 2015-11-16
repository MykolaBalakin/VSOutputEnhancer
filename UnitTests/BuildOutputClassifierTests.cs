using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.UnitTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BuildOutputClassifierTests {
        [TestMethod]
        public void BuildFailed() {
            const String buildCompleteMessage = "========== Build: 5 succeeded, 1 failed, 3 up-to-date, 2 skipped ==========\r\n";

            var span = Utils.CreateSpan(buildCompleteMessage);
            var registry = CreateClassificationTypeRegistryService();
            var classifier = new BuildOutputClassifier(registry);
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(span, classificationSpan.Span);
            Assert.AreEqual(ClassificationType.BuildResultFailed, classificationSpan.ClassificationType.Classification);
        }

        private IClassificationTypeRegistryService CreateClassificationTypeRegistryService() {
            return new ClassificationTypeRegistryServiceStub();
        }
    }
}
