using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Tests.Base;
using Xunit;

namespace Balakin.VSOutputEnhancer.Logic.Tests
{
    [ExcludeFromCodeCoverage]
    public class ExcludeFromCodeCoverageTests : ExcludeFromCodeCoverageTestsBase
    {
        [Fact]
        public void AllTestsCodeExcludedFromCoverage()
        {
            CheckAllTestsCodeExcludedFromCoverage();
        }
    }
}