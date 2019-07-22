using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic;

namespace Balakin.VSOutputEnhancer.Tests.IntegrationTests.TestCases
{
    [ExcludeFromCodeCoverage]
    public class BuildOutput : ITestCase
    {
        public String ContentType { get; } = Logic.ContentType.BuildOutput;

        public IReadOnlyList<String> SourceText { get; } = new[]
        {
            "------ Build started: Project: ConsoleDemo, Configuration: Debug Any CPU ------\r\n",
            "C:\\test\\ConsoleDemo\\Program.cs: warning CS0168: The variable 'ex' is declared but never used\r\n",
            "C:\\test\\ConsoleDemo\\Program.cs(1,14,1,15): error CS1022: Type or namespace definition, or end-of-file expected\r\n",
            "------ Build started: Project: WebDemo, Configuration: Debug Any CPU ------\r\n",
            "  WebDemo -> C:\\test\\WebDemo\\bin\\WebDemo.dll\r\n",
            "========== Build: 1 succeeded, 1 failed, 0 up-to-date, 0 skipped ==========\r\n"
        };

        public IReadOnlyList<ClassifiedText> ExpectedResult { get; } = new[]
        {
            new ClassifiedText(ClassificationType.BuildMessageWarning, "warning CS0168: The variable 'ex' is declared but never used"),
            new ClassifiedText(ClassificationType.BuildActionStartedWarning, "------ Build started: Project: ConsoleDemo, Configuration: Debug Any CPU ------"),
            new ClassifiedText(ClassificationType.BuildMessageError, "error CS1022: Type or namespace definition, or end-of-file expected"),
            new ClassifiedText(ClassificationType.BuildActionStartedError, "------ Build started: Project: ConsoleDemo, Configuration: Debug Any CPU ------"),
            new ClassifiedText(ClassificationType.BuildResultFailed, "========== Build: 1 succeeded, 1 failed, 0 up-to-date, 0 skipped ==========\r\n"),
            new ClassifiedText(ClassificationType.BuildActionStartedSuccess, "------ Build started: Project: WebDemo, Configuration: Debug Any CPU ------")
        };
    }
}