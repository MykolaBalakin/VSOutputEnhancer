using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Tests.Stubs;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class ParsedDataTests
    {
        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void Create(String message, String regex, ParsedDataStub expectedResult)
        {
            var span = Utils.CreateSpan(message);

            var match = Regex.Match(span.GetText(), regex);
            var actualResult = ParsedData.Create<ParsedDataStub>(match, span.Span);
            actualResult.ShouldBeEquivalentTo(expectedResult);
        }

        public static IEnumerable<Object[]> CreateTestData()
        {
            yield return new Object[]
            {
                "Text",
                "(?<Message>.*)",
                new ParsedDataStub(
                    new ParsedValue<String>("Text", new Span(0, 4)),
                    new ParsedValue<TraceEventType>()
                )
            };

            yield return new Object[]
            {
                "Text Error",
                "(?<Message>.*) (?<Type>.*)",
                new ParsedDataStub(
                    new ParsedValue<String>("Text", new Span(0, 4)),
                    new ParsedValue<TraceEventType>(TraceEventType.Error, new Span(5, 5))
                )
            };
        }

        [Fact]
        public void EmptyValueExceptionValueType()
        {
            var value = new ParsedValue<Int32>();
            Action action = () =>
            {
                // ReSharper disable once UnusedVariable
                var i = (Int32) value;
            };
            action.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void EmptyValueExceptionReferenceType()
        {
            var value = new ParsedValue<String>();
            Action action = () =>
            {
                // ReSharper disable once UnusedVariable
                var s = (String) value;
            };
            action.ShouldThrow<InvalidOperationException>();
        }
    }
}