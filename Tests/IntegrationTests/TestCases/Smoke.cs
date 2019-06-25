using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Balakin.VSOutputEnhancer.Tests.IntegrationTests.TestCases
{
    [ExcludeFromCodeCoverage]
    public class Smoke : ITestCase
    {
        public String ContentType { get; } = VSOutputEnhancer.ContentType.BuildOrderOutput;

        public IReadOnlyList<String> SourceText { get; } = new[]
        {
            "========== Rebuild All: 1 succeeded, 0 failed, 0 skipped ==========\r\n"
        };

        public IReadOnlyList<ClassifiedText> ExpectedResult { get; } = new[]
        {
            new ClassifiedText(ClassificationType.BuildResultSucceeded, "========== Rebuild All: 1 succeeded, 0 failed, 0 skipped ==========\r\n")
        };
    }
}