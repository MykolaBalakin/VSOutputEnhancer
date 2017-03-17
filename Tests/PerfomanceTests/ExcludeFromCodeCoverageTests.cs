using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.PerfomanceTests
{
    [ExcludeFromCodeCoverage]
    public class ExcludeFromCodeCoverageTests : ExcludeFromCodeCoverageTestsBase
    {
        [Fact]
        public void AllTestsCodeExcludedFromCoverage()
        {
            CheckAllTestsCodeExcludedFromCoverage();
        }

        protected override Assembly GetAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}