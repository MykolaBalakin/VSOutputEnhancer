using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.NpmResult;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Parsers
{
    [ExcludeFromCodeCoverage]
    public class NpmResultParserTests
    {
        [Theory]
        [InlineData("Some message\r\n")]
        [InlineData("====npm command completed with exit code asdf====\r\n")]
        public void NotParsed(String message)
        {
            var span = Utils.CreateSpan(message);
            var parser = new NpmResultParser();

            NpmResultData actualResult;
            var parsed = parser.TryParse(span, out actualResult);

            parsed.Should().BeFalse();
            actualResult.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(CreateExitCodeTestData))]
        public void ExitCode(String message, NpmResultData expectedResult)
        {
            var span = Utils.CreateSpan(message);
            var parser = new NpmResultParser();

            NpmResultData actualResult;
            var parsed = parser.TryParse(span, out actualResult);

            parsed.Should().BeTrue();
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        public static IEnumerable<Object[]> CreateExitCodeTestData()
        {
            yield return new Object[]
            {
                "====npm command completed with exit code 1823====\r\n",
                new NpmResultData(new ParsedValue<Int32>(1823, new Span(41, 4)))
            };
            yield return new Object[]
            {
                "====npm command completed with exit code -8====\r\n",
                new NpmResultData(new ParsedValue<Int32>(-8, new Span(41, 2)))
            };
        }
    }
}