using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic;

namespace Balakin.VSOutputEnhancer.Tests.IntegrationTests.TestCases
{
    [ExcludeFromCodeCoverage]
    public abstract class NpmOutputBase : ITestCase
    {
        public abstract String ContentType { get; }

        public IReadOnlyList<String> SourceText { get; } = new[]
        {
            "npm WARN package.json ASP.NET@0.0.0 No description\r\n",
            "====npm command completed with exit code 0====\r\n",
            "npm ERR! 404 Not Found\r\n",
            "====npm command completed with exit code 1====\r\n"
        };

        public IReadOnlyList<ClassifiedText> ExpectedResult { get; } = new[]
        {
            new ClassifiedText(ClassificationType.NpmMessageWarning, "package.json ASP.NET@0.0.0 No description"),
            new ClassifiedText(ClassificationType.NpmResultSucceeded, "====npm command completed with exit code 0====\r\n"),
            new ClassifiedText(ClassificationType.NpmMessageError, "404 Not Found"),
            new ClassifiedText(ClassificationType.NpmResultFailed, "====npm command completed with exit code 1====\r\n")
        };
    }
}