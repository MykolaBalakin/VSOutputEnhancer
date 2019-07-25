using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic;

namespace Balakin.VSOutputEnhancer.Tests.IntegrationTests.TestCases
{
    [ExcludeFromCodeCoverage]
    public class BowerOutput : ITestCase
    {
        public String ContentType { get; } = Logic.ContentType.Output;

        public IReadOnlyList<String> SourceText { get; } = new[]
        {
            "bower bootstrap1#3.3.5       ENOTFOUND Package bootstrap1 not found\r\n"
        };

        public IReadOnlyList<ClassifiedText> ExpectedResult { get; } = new[]
        {
            new ClassifiedText(ClassificationType.BowerMessageError, "Package bootstrap1 not found")
        };
    }
}