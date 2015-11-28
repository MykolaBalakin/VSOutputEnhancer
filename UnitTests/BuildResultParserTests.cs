using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Parsers.BuildResult;
using Balakin.VSOutputEnhancer.UnitTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BuildResultParserTests {
        [TestMethod]
        public void NotParsed() {
            const String randomMessage = "Message\r\n";
            const String publishCompleteMessage = "========== Publish: 10 succeeded, 3 failed, 122 skipped ==========\r\n";
            const String almostBuildMessage = "========== Build: bla bla\r\n";
            const String almostBuildMessage2 = "========== Build: bla bla ==========\r\n";

            TestNotParsed(randomMessage);
            TestNotParsed(publishCompleteMessage);
            TestNotParsed(almostBuildMessage);
            TestNotParsed(almostBuildMessage2);
        }

        private void TestNotParsed(String message) {
            var span = Utils.CreateSpan(message);
            var parser = new BuildResultParser();
            BuildResultData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsFalse(parsed);
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Build() {
            const String publishCompleteMessage = "========== Build: 302 succeeded, 41 failed, 16 up-to-date, 5 skipped ==========\r\n";

            var span = Utils.CreateSpan(publishCompleteMessage);
            BuildResultData data;
            var parser = new BuildResultParser();
            var parsed = parser.TryParse(span, out data);
            Assert.IsTrue(parsed);
            Assert.IsNotNull(data);

            Assert.IsTrue(data.Succeeded.HasValue);
            Assert.IsTrue(data.Failed.HasValue);
            Assert.IsTrue(data.UpToDate.HasValue);
            Assert.IsTrue(data.Skipped.HasValue);

            Assert.AreEqual(302, data.Succeeded);
            Assert.AreEqual(41, data.Failed);
            Assert.AreEqual(16, data.UpToDate);
            Assert.AreEqual(5, data.Skipped);

            Assert.AreEqual(new Span(18, 3), data.Succeeded.Span);
            Assert.AreEqual(new Span(33, 2), data.Failed.Span);
            Assert.AreEqual(new Span(44, 2), data.UpToDate.Span);
            Assert.AreEqual(new Span(59, 1), data.Skipped.Span);
        }

        [TestMethod]
        public void Rebuild() {
            const String publishCompleteMessage = "========== Rebuild All: 2 succeeded, 135 failed, 86 skipped ==========\r\n";

            var span = Utils.CreateSpan(publishCompleteMessage);
            BuildResultData data;
            var parser = new BuildResultParser();
            var parsed = parser.TryParse(span, out data);
            Assert.IsTrue(parsed);
            Assert.IsNotNull(data);

            Assert.IsTrue(data.Succeeded.HasValue);
            Assert.IsTrue(data.Failed.HasValue);
            Assert.IsTrue(data.UpToDate.HasValue);
            Assert.IsTrue(data.Skipped.HasValue);

            Assert.AreEqual(2, data.Succeeded);
            Assert.AreEqual(135, data.Failed);
            Assert.AreEqual(0, data.UpToDate);
            Assert.AreEqual(86, data.Skipped);

            Assert.AreEqual(new Span(24, 1), data.Succeeded.Span);
            Assert.AreEqual(new Span(37, 3), data.Failed.Span);
            Assert.AreEqual(0, data.UpToDate.Span.Length);
            Assert.AreEqual(new Span(49, 2), data.Skipped.Span);
        }
    }
}
