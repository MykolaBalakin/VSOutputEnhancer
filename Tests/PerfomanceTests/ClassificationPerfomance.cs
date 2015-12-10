using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.Tests.PerfomanceTests {
    [TestClass]
    public class ClassificationPerfomance {
        [TestMethod]
        public void EntityFramework() {
            // ~ 570 000 lines of log
            // Small count of classified text

            var content = Utils.ReadLogFile("Resources\\EntityFrameworkBuild.log").ToList();
            var classifier = Tests.Utils.CreateBuildOutputClassifier();
            var totalCount = 0;
            var sw = Stopwatch.StartNew();
            foreach (var line in content) {
                totalCount += classifier.GetClassificationSpans(Tests.Utils.CreateSpan(line + "\r\n")).Count;
            }
            sw.Stop();
            Assert.IsTrue(sw.Elapsed < TimeSpan.FromSeconds(5), "Elapsed: " + sw.Elapsed);
        }
    }
}
