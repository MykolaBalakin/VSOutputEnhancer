using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Tests.Base;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Xunit;

namespace Balakin.VSOutputEnhancer.Logic.Tests
{
    [ExcludeFromCodeCoverage]
    public class ParsedDataTests
    {
        public class ParsedDataStub : ParsedData
        {
            // TODO: Refactor ParsedData builder to get rid of this constructor
            public ParsedDataStub()
            {
            }

            public ParsedDataStub(ParsedValue<String> message, ParsedValue<TraceEventType> type)
            {
                Message = message;
                Type = type;
            }

            public ParsedValue<String> Message { get; private set; }
            public ParsedValue<TraceEventType> Type { get; private set; }
        }

        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void Create(String message, String regex, ParsedDataStub expectedResult)
        {
            var span = message.ToSnapshotSpan();

            var match = Regex.Match(message, regex);
            var actualResult = ParsedData.Create<ParsedDataStub>(match, span.Span);
            actualResult.Should().BeEquivalentTo(expectedResult);
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
    }
}