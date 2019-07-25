using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BowerMessage;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Parsers
{
    [ExcludeFromCodeCoverage]
    public class BowerMessageParserTests
    {
        [Theory]
        [InlineData("Some message\r\n")]
        [InlineData("bower \r\n")]
        public void NotParsed(String message)
        {
            var span = Utils.CreateSpan(message);
            var parser = new BowerMessageParser();
            BowerMessageData data;
            var parsed = parser.TryParse(span, out data);
            parsed.Should().BeFalse();
            data.Should().BeNull();
        }

        [Fact]
        public void NotFound()
        {
            const String notFoundError = "bower bootstrap1#3.3.5       ENOTFOUND Package bootstrap1 not found\r\n";
            var expectedResult = new BowerMessageData
            {
                PackageName = new ParsedValue<String>("bootstrap1", new Span(6, 10)),
                PackageVersion = new ParsedValue<String>("3.3.5", new Span(17, 5)),
                Type = new ParsedValue<MessageType>(MessageType.Error, new Span(29, 9)),
                ErrorCode = new ParsedValue<String>("ENOTFOUND", new Span(29, 9)),
                Message = new ParsedValue<String>("Package bootstrap1 not found", new Span(39, 28))
            };

            var span = Utils.CreateSpan(notFoundError);
            var parser = new BowerMessageParser();
            BowerMessageData data;
            var parsed = parser.TryParse(span, out data);

            parsed.Should().BeTrue();
            data.ShouldBeEquivalentTo(expectedResult);
        }
    }
}