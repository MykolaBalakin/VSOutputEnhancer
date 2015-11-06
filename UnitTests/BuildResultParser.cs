using System;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.UnitTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    public class BuildResultParser {
        const String BuildResultMessage = "========== Build: 3 succeeded, 1 failed, 16 up-to-date, 5 skipped ==========\r\n";

        [TestMethod]
        public void Build() {
            var span = CreateSpan(BuildResultMessage);
            BuildResult buildResult;
            var parsed = BuildResult.TryParse(span, out buildResult);
            Assert.IsTrue(parsed);
            Assert.IsNotNull(buildResult);
            Assert.AreEqual(3,buildResult.Succeeded);
            Assert.AreEqual(1, buildResult.Failed);
            Assert.AreEqual(16, buildResult.UpToDate);
            Assert.AreEqual(5, buildResult.Skipped);
        }

        private SnapshotSpan CreateSpan(String text) {
            var snapshot = new TextSnapshotStub(text);
            return new SnapshotSpan(snapshot, new Span(0, snapshot.Length));
        }
    }
}
