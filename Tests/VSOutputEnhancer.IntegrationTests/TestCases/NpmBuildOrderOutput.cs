using System;
using System.Diagnostics.CodeAnalysis;

namespace Balakin.VSOutputEnhancer.IntegrationTests.TestCases
{
    [ExcludeFromCodeCoverage]
    public class NpmBuildOrderOutput : NpmOutputBase
    {
        public override String ContentType { get; } = Logic.ContentType.BuildOrderOutput;
    }
}