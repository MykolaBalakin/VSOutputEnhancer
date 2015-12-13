using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.Tests {
    [ExcludeFromCodeCoverage]
    public class ExcludeFromCodeCoverageTestsBase {
        protected void CheckAllTestsCodeExcludedFromCoverage() {
            var assembly = GetAssembly();

            var notExcluded = assembly.GetTypes().Where(t => !IsExcludedFromCodeCoverage(t)).ToList();
            var notExcludedString = String.Join(", ", notExcluded.Select(t => t.Name));
            Assert.AreEqual(0, notExcluded.Count, $"Those types are not excluded from code coverage analysis: {notExcludedString}");
        }

        protected virtual Assembly GetAssembly() {
            return Assembly.GetExecutingAssembly();
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