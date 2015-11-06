using System;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.UnitTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    public class BuildResultParser {
        const String BuildResultMessage = "========== Build: 3 succeeded, 1 failed, 16 up-to-date, 5 skipped ==========\r\n";
        const String RebuildResultMessage = "========== Rebuild All: 2 succeeded, 5 failed, 1 skipped ==========\r\n";

        [TestMethod]
        public void Build() {
            var span = CreateSpan(BuildResultMessage);
            BuildResult buildResult;
            var parsed = BuildResult.TryParse(span, out buildResult);
            Assert.IsTrue(parsed, "Not parsed");
            Assert.IsNotNull(buildResult, "Not parsed");
            Assert.AreEqual(3, buildResult.Succeeded, "Succeeded projects");
            Assert.AreEqual(1, buildResult.Failed, "Failed projects");
            Assert.AreEqual(16, buildResult.UpToDate, "Up-to-date projects");
            Assert.AreEqual(5, buildResult.Skipped, "Skipped projects");
        }

        [TestMethod]
        public void Rebuild() {
            var span = CreateSpan(RebuildResultMessage);
            BuildResult buildResult;
            var parsed = BuildResult.TryParse(span, out buildResult);
            Assert.IsTrue(parsed, "Not parsed");
            Assert.IsNotNull(buildResult, "Not parsed");
            Assert.AreEqual(2, buildResult.Succeeded, "Succeeded projects");
            Assert.AreEqual(5, buildResult.Failed, "Failed projects");
            Assert.AreEqual(0, buildResult.UpToDate, "Up-to-date projects");
            Assert.AreEqual(1, buildResult.Skipped, "Skipped projects");
        }

        private SnapshotSpan CreateSpan(String text) {
            var snapshot = new TextSnapshotStub(text);
            return new SnapshotSpan(snapshot, new Span(0, snapshot.Length));
        }
    }
}
