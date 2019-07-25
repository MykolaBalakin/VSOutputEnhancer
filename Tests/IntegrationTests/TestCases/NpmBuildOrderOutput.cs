using System;
using System.Diagnostics.CodeAnalysis;

namespace Balakin.VSOutputEnhancer.Tests.IntegrationTests.TestCases
{
    [ExcludeFromCodeCoverage]
    public class NpmBuildOrderOutput : NpmOutputBase
    {
        public override String ContentType { get; } = Logic.ContentType.BuildOrderOutput;
    }
}