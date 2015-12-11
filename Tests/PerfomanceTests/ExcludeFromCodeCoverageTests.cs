using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.Tests.PerfomanceTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ExcludeFromCodeCoverageTests : ExcludeFromCodeCoverageTestsBase {
        [TestMethod]
        public void AllTestsCodeExcludedFromCoverage() {
            CheckAllTestsCodeExcludedFromCoverage();
        }

        protected override Assembly GetAssembly() {
            return Assembly.GetExecutingAssembly();
        }
    }
}