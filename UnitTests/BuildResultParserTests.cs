using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.UnitTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BuildResultParserTests {
        [TestMethod]
        public void Build() {
            const String buildCompleteMessage = "========== Build: 3 succeeded, 1 failed, 16 up-to-date, 5 skipped ==========\r\n";

            var span = Utils.CreateSpan(buildCompleteMessage);
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
            const String rebuildCompleteMessage = "========== Rebuild All: 2 succeeded, 5 failed, 1 skipped ==========\r\n";

            var span = Utils.CreateSpan(rebuildCompleteMessage);
            BuildResult buildResult;
            var parsed = BuildResult.TryParse(span, out buildResult);
            Assert.IsTrue(parsed, "Not parsed");
            Assert.IsNotNull(buildResult, "Not parsed");
            Assert.AreEqual(2, buildResult.Succeeded, "Succeeded projects");
            Assert.AreEqual(5, buildResult.Failed, "Failed projects");
            Assert.AreEqual(0, buildResult.UpToDate, "Up-to-date projects");
            Assert.AreEqual(1, buildResult.Skipped, "Skipped projects");
        }
    }
}
