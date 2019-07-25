using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic;

namespace Balakin.VSOutputEnhancer.Tests.IntegrationTests.TestCases
{
    [ExcludeFromCodeCoverage]
    public class PublishResult : ITestCase
    {
        public String ContentType { get; } = Logic.ContentType.BuildOrderOutput;

        public IReadOnlyList<String> SourceText { get; } = new[]
        {
            "1>------ Build started: Project: WebDemo, Configuration: Release Any CPU ------\r\n",
            "1>  WebDemo -> C:\\test\\WebDemo\\bin\\WebDemo.dll\r\n",
            "2>------ Publish started: Project: WebDemo, Configuration: Release Any CPU ------\r\n",
            "2>Connecting to C:\\test\\WebDemoSite...\r\n",
            "2>Transformed Web.config using C:\\test\\WebDemo\\Web.Release.config into obj\\Release\\TransformWebConfig\\transformed\\Web.config.\r\n",
            "2>Copying all files to temporary location below for package/publish:\r\n",
            "2>obj\\Release\\Package\\PackageTmp.\r\n",
            "2>Publishing folder /...\r\n",
            "2>Publishing folder bin...\r\n",
            "2>Publishing folder bin/roslyn...\r\n",
            "2>Web App was published successfully file:///C:/test/WebDemoSite\r\n",
            "2>\r\n",
            "========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========\r\n",
            "========== Publish: 1 succeeded, 0 failed, 0 skipped ==========\r\n"
        };

        public IReadOnlyList<ClassifiedText> ExpectedResult { get; } = new[]
        {
            new ClassifiedText(ClassificationType.BuildResultSucceeded, "========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========\r\n"),
            new ClassifiedText(ClassificationType.PublishResultSucceeded, "========== Publish: 1 succeeded, 0 failed, 0 skipped ==========\r\n"),
        };
    }
}