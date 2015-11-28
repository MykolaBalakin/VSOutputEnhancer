using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Parsers.PublishResult;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PublishResultParserTests {
        [TestMethod]
        public void NotParsed() {
            const String randomMessage = "Message\r\n";
            const String buildCompleteMessage = "========== Build: 5 succeeded, 1 failed, 3 up-to-date, 2 skipped ==========\r\n";
            const String almostPublishMessage = "========== Publish: bla bla\r\n";
            const String almostPublishMessage2 = "========== Publish: bla bla ==========\r\n";

            TestNotParsed(randomMessage);
            TestNotParsed(buildCompleteMessage);
            TestNotParsed(almostPublishMessage);
            TestNotParsed(almostPublishMessage2);
        }

        private void TestNotParsed(String message) {
            var span = Utils.CreateSpan(message);
            var parser = new PublishResultParser();
            PublishResultData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsFalse(parsed);
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Publish() {
            const String publishCompleteMessage = "========== Publish: 10 succeeded, 3 failed, 122 skipped ==========\r\n";

            var span = Utils.CreateSpan(publishCompleteMessage);
            PublishResultData data;
            var parser = new PublishResultParser();
            var parsed = parser.TryParse(span, out data);
            Assert.IsTrue(parsed);
            Assert.IsNotNull(data);

            Assert.IsTrue(data.Succeeded.HasValue);
            Assert.IsTrue(data.Failed.HasValue);
            Assert.IsTrue(data.Skipped.HasValue);

            Assert.AreEqual(10, data.Succeeded);
            Assert.AreEqual(3, data.Failed);
            Assert.AreEqual(122, data.Skipped);

            Assert.AreEqual(new Span(20, 2), data.Succeeded.Span);
            Assert.AreEqual(new Span(34, 1), data.Failed.Span);
            Assert.AreEqual(new Span(44, 3), data.Skipped.Span);
        }
    }
}
