using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.Tests.PerfomanceTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ClassificationPerfomance {
        [TestMethod]
        public void EntityFramework() {
            // ~ 570 000 lines of log
            // Small count of classified text

            var content = Utils.ReadLogFile("Resources\\EntityFrameworkBuild.log");
            var spans = content.Select(Tests.Utils.CreateSpan).ToList();
            var classifier = Tests.Utils.CreateBuildOutputClassifier();
            var totalCount = 0;
            var sw = Stopwatch.StartNew();
            foreach (var span in spans) {
                totalCount += classifier.GetClassificationSpans(span).Count;
            }
            sw.Stop();
            Trace.TraceInformation("Elapsed: " + sw.Elapsed);
            Assert.IsTrue(sw.Elapsed < TimeSpan.FromSeconds(5), "Elapsed: " + sw.Elapsed);
        }

        [TestMethod]
        public void LotOfClassifiedMessages() {
            // 100 000 warning/error messages

            var content = Utils.ReadLogFile("Resources\\RandomBuildOutput.log");
            var spans = content.Select(Tests.Utils.CreateSpan).ToList();
            var classifier = Tests.Utils.CreateBuildOutputClassifier();
            var totalCount = 0;
            var sw = Stopwatch.StartNew();
            foreach (var span in spans) {
                totalCount += classifier.GetClassificationSpans(span).Count;
            }
            sw.Stop();
            Trace.TraceInformation("Elapsed: " + sw.Elapsed);
            Assert.IsTrue(sw.Elapsed < TimeSpan.FromSeconds(5), "Elapsed: " + sw.Elapsed);
        }
    }
}
