using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Parsers.BowerMessage;
using Balakin.VSOutputEnhancer.Parsers.NpmMessage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BowerMessageParserTests {
        [TestMethod]
        public void NotParsed() {
            const String messageString = "Some message\r\n";
            const String messageString2 = "bower \r\n";

            var span = Utils.CreateSpan(messageString);
            var parser = new BowerMessageParser();
            BowerMessageData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsFalse(parsed);
            Assert.IsNull(data);

            span = Utils.CreateSpan(messageString2);
            parser = new BowerMessageParser();
            parsed = parser.TryParse(span, out data);
            Assert.IsFalse(parsed);
            Assert.IsNull(data);
        }

        [TestMethod]
        public void NotFound() {
            const String notFoundError = "bower bootstrap1#3.3.5       ENOTFOUND Package bootstrap1 not found\r\n";

            var span = Utils.CreateSpan(notFoundError);
            var parser = new BowerMessageParser();
            BowerMessageData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsTrue(parsed);
            Assert.IsNotNull(data);

            Assert.IsTrue(data.PackageName.HasValue);
            Assert.IsTrue(data.PackageVersion.HasValue);
            Assert.IsTrue(data.Type.HasValue);
            Assert.IsTrue(data.ErrorCode.HasValue);
            Assert.IsTrue(data.Message.HasValue);

            Assert.AreEqual("bootstrap1", data.PackageName);
            Assert.AreEqual("3.3.5", data.PackageVersion);
            Assert.AreEqual(MessageType.Error, data.Type);
            Assert.AreEqual("ENOTFOUND", data.ErrorCode);
            Assert.AreEqual("Package bootstrap1 not found", data.Message);

            Assert.AreEqual(new SnapshotSpan(span.Snapshot, new Span(6, 10)), data.PackageName.Span);
            Assert.AreEqual(new SnapshotSpan(span.Snapshot, new Span(17, 5)), data.PackageVersion.Span);
            Assert.AreEqual(new SnapshotSpan(span.Snapshot, new Span(29, 9)), data.Type.Span);
            Assert.AreEqual(new SnapshotSpan(span.Snapshot, new Span(29, 9)), data.ErrorCode.Span);
            Assert.AreEqual(new SnapshotSpan(span.Snapshot, new Span(39, 28)), data.Message.Span);
        }
    }
}
