using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Parsers.BuildFileRelatedMessage;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Parsers
{
    [ExcludeFromCodeCoverage]
    public class BuildFileRelatedMessageParserTests
    {
        [Theory]
        [InlineData("Some message\r\n")]
        [InlineData("Some message: error \r\n")]
        public void NotParsed(String message)
        {
            var span = Utils.CreateSpan(message);
            var parser = new BuildFileRelatedMessageParser();

            BuildFileRelatedMessageData actualResult;
            var parsed = parser.TryParse(span, out actualResult);

            parsed.Should().BeFalse();
            actualResult.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(CreateWarningTestData))]
        public void Warning(String message, BuildFileRelatedMessageData expectedResult)
        {
            Test(message, expectedResult);
        }

        [Theory]
        [MemberData(nameof(CreateSingleBuildTaskTestData))]
        public void SingleBuildTask(String message, BuildFileRelatedMessageData expectedResult)
        {
            Test(message, expectedResult);
        }

        [Theory]
        [MemberData(nameof(CreatePostSharpNotReferencedTestData))]
        public void PostSharpNotReferenced(String message, BuildFileRelatedMessageData expectedResult)
        {
            Test(message, expectedResult);
        }

        [Theory]
        [MemberData(nameof(CreateBowerErrorTestData))]
        public void BowerError(String message, BuildFileRelatedMessageData expectedResult)
        {
            Test(message, expectedResult);
        }

        private void Test(String message, BuildFileRelatedMessageData expectedResult)
        {
            var span = Utils.CreateSpan(message);
            var parser = new BuildFileRelatedMessageParser();

            BuildFileRelatedMessageData actualResult;
            var parsed = parser.TryParse(span, out actualResult);

            parsed.Should().BeTrue();
            actualResult.ShouldBeEquivalentTo(expectedResult);
        }

        public static IEnumerable<object[]> CreateWarningTestData()
        {
            yield return new Object[]
            {
                "1>C:\\Sources\\GitHub\\VSOutputEnhancer\\VSOutputEnhancer\\ClassificationType.cs(29,53,29,83): warning CS0169: The field 'ClassificationType.BuildResultSucceededDefinition' is never used\r\n",
                new BuildFileRelatedMessageData(
                    new ParsedValue<Int32>(1, new Span(0, 1)),
                    new ParsedValue<MessageType>(MessageType.Warning, new Span(90, 7)),
                    new ParsedValue<String>("CS0169", new Span(98, 6)),
                    new ParsedValue<String>("The field 'ClassificationType.BuildResultSucceededDefinition' is never used", new Span(106, 75)),
                    new ParsedValue<String>("warning CS0169: The field 'ClassificationType.BuildResultSucceededDefinition' is never used", new Span(90, 91)),
                    new ParsedValue<String>("C:\\Sources\\GitHub\\VSOutputEnhancer\\VSOutputEnhancer\\ClassificationType.cs", new Span(2, 73))
                )
            };

            yield return new Object[]
            {
                "9>C:\\Sources\\Some project path\\AppPoolDlg.wxs(9,0): warning CNDL1000: The Binary/@Id attribute's value, 'GetUserCredentialsCA', is 20 characters long.  It will be too long if modularized.  The identifier shouldn't be longer than 18 characters long to allow for modularization (appending a guid for merge modules).\r\n",
                new BuildFileRelatedMessageData(
                    new ParsedValue<Int32>(9, new Span(0, 1)),
                    new ParsedValue<MessageType>(MessageType.Warning, new Span(52, 7)),
                    new ParsedValue<String>("CNDL1000", new Span(60, 8)),
                    new ParsedValue<String>("The Binary/@Id attribute's value, 'GetUserCredentialsCA', is 20 characters long.  It will be too long if modularized.  The identifier shouldn't be longer than 18 characters long to allow for modularization (appending a guid for merge modules).", new Span(70, 243)),
                    new ParsedValue<String>("warning CNDL1000: The Binary/@Id attribute's value, 'GetUserCredentialsCA', is 20 characters long.  It will be too long if modularized.  The identifier shouldn't be longer than 18 characters long to allow for modularization (appending a guid for merge modules).", new Span(52, 261)),
                    new ParsedValue<String>("C:\\Sources\\Some project path\\AppPoolDlg.wxs", new Span(2, 43))
                )
            };
        }

        public static IEnumerable<object[]> CreateSingleBuildTaskTestData()
        {
            yield return new Object[]
            {
                "C:\\Sources\\Local\\AppConfigWix\\AppConfigWix\\Product.wxs(83,0): warning CNDL1138: The RegistryKey/@Action attribute has been deprecated.  In most cases, you can simply omit @Action.  If you need to force Windows Installer to create an empty key or recursively delete the key, use the ForceCreateOnInstall or ForceDeleteOnUninstall attributes instead.\r\n",
                new BuildFileRelatedMessageData(
                    new ParsedValue<Int32>(),
                    new ParsedValue<MessageType>(MessageType.Warning, new Span(62, 7)),
                    new ParsedValue<String>("CNDL1138", new Span(70, 8)),
                    new ParsedValue<String>("The RegistryKey/@Action attribute has been deprecated.  In most cases, you can simply omit @Action.  If you need to force Windows Installer to create an empty key or recursively delete the key, use the ForceCreateOnInstall or ForceDeleteOnUninstall attributes instead.", new Span(80, 268)),
                    new ParsedValue<String>("warning CNDL1138: The RegistryKey/@Action attribute has been deprecated.  In most cases, you can simply omit @Action.  If you need to force Windows Installer to create an empty key or recursively delete the key, use the ForceCreateOnInstall or ForceDeleteOnUninstall attributes instead.", new Span(62, 286)),
                    new ParsedValue<String>("C:\\Sources\\Local\\AppConfigWix\\AppConfigWix\\Product.wxs", new Span(0, 54))
                )
            };
        }

        public static IEnumerable<object[]> CreatePostSharpNotReferencedTestData()
        {
            yield return new Object[]
            {
                "1>C:\\Sources\\Some project\\SomeProject.csproj(163,5): error : This project references NuGet package(s) that are missing on this computer. Enable NuGet Package Restore to download them.  For more information, see http://www.postsharp.net/links/nuget-restore.\r\n",
                new BuildFileRelatedMessageData(
                    new ParsedValue<Int32>(1, new Span(0, 1)),
                    new ParsedValue<MessageType>(MessageType.Error, new Span(53, 5)),
                    new ParsedValue<String>(),
                    new ParsedValue<String>("This project references NuGet package(s) that are missing on this computer. Enable NuGet Package Restore to download them.  For more information, see http://www.postsharp.net/links/nuget-restore.", new Span(61, 195)),
                    new ParsedValue<String>("error : This project references NuGet package(s) that are missing on this computer. Enable NuGet Package Restore to download them.  For more information, see http://www.postsharp.net/links/nuget-restore.", new Span(53, 203)),
                    new ParsedValue<String>("C:\\Sources\\Some project\\SomeProject.csproj", new Span(2, 42))
                )
            };
        }

        public static IEnumerable<object[]> CreateBowerErrorTestData()
        {
            yield return new Object[]
            {
                "C:\\Program Files (x86)\\MSBuild\\Microsoft\\VisualStudio\\v14.0\\Web\\Microsoft.DNX.Publishing.targets(152,5): Error : bower bootstrap1#3.3.5       ENOTFOUND Package bootstrap1 not found\r\n",
                new BuildFileRelatedMessageData(
                    new ParsedValue<Int32>(),
                    new ParsedValue<MessageType>(MessageType.Error, new Span(105, 5)),
                    new ParsedValue<String>(),
                    new ParsedValue<String>("bower bootstrap1#3.3.5       ENOTFOUND Package bootstrap1 not found", new Span(113, 67)),
                    new ParsedValue<String>("Error : bower bootstrap1#3.3.5       ENOTFOUND Package bootstrap1 not found", new Span(105, 75)),
                    new ParsedValue<String>("C:\\Program Files (x86)\\MSBuild\\Microsoft\\VisualStudio\\v14.0\\Web\\Microsoft.DNX.Publishing.targets", new Span(0, 96))
                )
            };
        }
    }
}