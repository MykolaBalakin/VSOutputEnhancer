using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PublishResultParserTests {
        [TestMethod]
        public void Publish() {
            const String publishCompleteMessage = "========== Publish: 1 succeeded, 3 failed, 2 skipped ==========\r\n";

            var span = Utils.CreateSpan(publishCompleteMessage);
            PublishResultParser publishResult;
            var parsed = PublishResultParser.TryParse(span, out publishResult);
            Assert.IsTrue(parsed, "Not parsed");
            Assert.IsNotNull(publishResult, "Not parsed");
            Assert.AreEqual(1, publishResult.Succeeded, "Succeeded projects");
            Assert.AreEqual(3, publishResult.Failed, "Failed projects");
            Assert.AreEqual(2, publishResult.Skipped, "Skipped projects");
        }
    }
}
