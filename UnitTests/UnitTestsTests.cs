using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestsTests {
        [TestMethod]
        public void AllTestsCodeExcludedFromCoverage() {
            var assembly = Assembly.GetExecutingAssembly();

            var notExcluded = assembly.GetTypes().Where(t => !IsExcludedFromCodeCoverage(t)).ToList();
            var notExcludedString = String.Join(", ", notExcluded.Select(t => t.Name));
            Assert.AreEqual(0, notExcluded.Count, $"Those types are not excluded from code coverage analysis: {notExcludedString}");
        }

        private Boolean IsExcludedFromCodeCoverage(Type t) {
            var attribute = t.GetCustomAttribute<ExcludeFromCodeCoverageAttribute>();
            if (attribute != null) {
                return true;
            }
            if (t.DeclaringType != null) {
                return IsExcludedFromCodeCoverage(t.DeclaringType);
            }
            return false;
        }
    }
}
