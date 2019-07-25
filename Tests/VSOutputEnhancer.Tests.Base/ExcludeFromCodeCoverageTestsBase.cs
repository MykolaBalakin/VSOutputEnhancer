using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;

namespace Balakin.VSOutputEnhancer.Tests.Base
{
    [ExcludeFromCodeCoverage]
    public class ExcludeFromCodeCoverageTestsBase
    {
        protected void CheckAllTestsCodeExcludedFromCoverage()
        {
            var assembly = GetAssembly();

            var notExcluded = assembly.GetTypes()
                .Where(t => !t.IsInterface)
                .Where(t => !IsExcludedFromCodeCoverage(t))
                .ToList();
            notExcluded.Should().BeEmpty("All test code should be excluded from code coverage");
        }

        protected virtual Assembly GetAssembly()
        {
            return GetType().Assembly;
        }

        private Boolean IsExcludedFromCodeCoverage(Type t)
        {
            var attribute = t.GetCustomAttribute<ExcludeFromCodeCoverageAttribute>();
            if (attribute != null)
            {
                return true;
            }

            if (t.DeclaringType != null)
            {
                return IsExcludedFromCodeCoverage(t.DeclaringType);
            }

            return false;
        }
    }
}