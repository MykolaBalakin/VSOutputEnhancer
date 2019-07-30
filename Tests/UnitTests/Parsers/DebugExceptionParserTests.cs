using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.DebugException;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Parsers
{
    [ExcludeFromCodeCoverage]
    public class DebugExceptionParserTests
    {
        [Theory]
        [InlineData("Some message\r\n")]
        [InlineData("Exception thrown: 'blablabla\r\n")]
        [InlineData("A first chance exception of type 'blablabla\r\n")]
        public void NotParsed(String message)
        {
            var span = Utils.CreateSpan(message);
            var parser = new DebugExceptionParser();

            DebugExceptionData actualResult;
            var parsed = parser.TryParse(span, out actualResult);

            parsed.Should().BeFalse();
            actualResult.Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void Exception(String message, DebugExceptionData expectedResult)
        {
            var span = Utils.CreateSpan(message);
            var parser = new DebugExceptionParser();

            DebugExceptionData actualResult;
            var parsed = parser.TryParse(span, out actualResult);

            parsed.Should().BeTrue();
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        public static IEnumerable<Object[]> CreateTestData()
        {
            yield return new Object[]
            {
                "Exception thrown: 'System.Exception' in VSOutputEnhancerDemo.exe\r\n",
                new DebugExceptionData(
                    new ParsedValue<String>("System.Exception", new Span(19, 16)),
                    new ParsedValue<String>("VSOutputEnhancerDemo.exe", new Span(40, 24))
                )
            };

            yield return new Object[]
            {
                "A first chance exception of type 'System.Exception' occurred in ConsoleDemo.exe\r\n",
                new DebugExceptionData(
                    new ParsedValue<String>("System.Exception", new Span(34, 16)),
                    new ParsedValue<String>("ConsoleDemo.exe", new Span(64, 15))
                )
            };
        }
    }
}