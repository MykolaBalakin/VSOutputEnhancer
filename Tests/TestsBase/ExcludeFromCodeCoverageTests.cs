using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.Tests {
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ExcludeFromCodeCoverageTests : ExcludeFromCodeCoverageTestsBase {
        [TestMethod]
        public void AllTestsCodeExcludedFromCoverage() {
            CheckAllTestsCodeExcludedFromCoverage();
        }
    }
}
