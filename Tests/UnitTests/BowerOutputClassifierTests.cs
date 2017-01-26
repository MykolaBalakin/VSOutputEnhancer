using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BowerOutputClassifierTests : ClassifierTestsBase
    {
        protected override IClassifier CreateClassifier()
        {
            return Utils.CreateBowerOutputClassifier();
        }

        [TestMethod]
        public void PackageNotFound()
        {
            const String notFoundError = "bower bootstrap1#3.3.5       ENOTFOUND Package bootstrap1 not found\r\n";

            var span = Utils.CreateSpan(notFoundError);
            var classifier = CreateClassifier();
            var result = classifier.GetClassificationSpans(span);

            Assert.AreEqual(1, result.Count);
            var classificationSpan = result.Single();
            Assert.AreEqual(new SnapshotSpan(span.Snapshot, 39, 28), classificationSpan.Span);
            Assert.AreEqual(ClassificationType.BowerMessageError, classificationSpan.ClassificationType.Classification);
        }
    }
}