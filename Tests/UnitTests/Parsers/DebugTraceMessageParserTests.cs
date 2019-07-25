using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.DebugTraceMessage;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Parsers
{
    [ExcludeFromCodeCoverage]
    public class DebugTraceMessageParserTests
    {
        [Fact]
        public void NotParsed()
        {
            const String messageString = "Some message\r\n";

            var span = Utils.CreateSpan(messageString);
            var parser = new DebugTraceMessageParser();
            DebugTraceMessageData actualResult;
            var parsed = parser.TryParse(span, out actualResult);

            parsed.Should().BeFalse();
            actualResult.Should().BeNull();
        }

        [Fact]
        public void Error()
        {
            const String messageString = "VSOutputEnhancerDemo.vshost.exe Information: 10 : Trace information message\r\n";
            var expectedResult = new DebugTraceMessageData(
                new ParsedValue<String>("VSOutputEnhancerDemo.vshost.exe", new Span(0, 31)),
                new ParsedValue<TraceEventType>(TraceEventType.Information, new Span(32, 11)),
                new ParsedValue<Int32>(10, new Span(45, 2)),
                new ParsedValue<String>("Trace information message", new Span(50, 25)),
                new ParsedValue<String>("Information: 10 : Trace information message", new Span(32, 43))
            );

            var span = Utils.CreateSpan(messageString);
            var parser = new DebugTraceMessageParser();
            DebugTraceMessageData actualResult;
            var parsed = parser.TryParse(span, out actualResult);

            parsed.Should().BeTrue();
            actualResult.ShouldBeEquivalentTo(expectedResult);
        }
    }
}