using System;
using System.Diagnostics.CodeAnalysis;

namespace Balakin.VSOutputEnhancer.IntegrationTests.TestCases
{
    [ExcludeFromCodeCoverage]
    public class NpmOutput : NpmOutputBase
    {
        public override String ContentType { get; } = Logic.ContentType.Output;
    }
}