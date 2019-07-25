using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.NpmMessage;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Parsers
{
    [ExcludeFromCodeCoverage]
    public class NpmMessageParserTests
    {
        [Theory]
        [InlineData("Some message\r\n")]
        [InlineData("npm \r\n")]
        public void NotParsed(String message)
        {
            var span = Utils.CreateSpan(message);
            var parser = new NpmMessageParser();

            NpmMessageData actualResult;
            var parsed = parser.TryParse(span, out actualResult);

            parsed.Should().BeFalse();
            actualResult.Should().BeNull();
        }

        [Fact]
        public void Warning()
        {
            const String warningMessage = "npm WARN package.json ASP.NET@0.0.0 No README data\r\n";
            var expectedResult = new NpmMessageData(
                new ParsedValue<MessageType>(MessageType.Warning, new Span(4, 4)),
                new ParsedValue<String>("package.json ASP.NET@0.0.0 No README data", new Span(9, 41))
            );

            var span = Utils.CreateSpan(warningMessage);
            var parser = new NpmMessageParser();

            NpmMessageData actualResult;
            var parsed = parser.TryParse(span, out actualResult);

            parsed.Should().BeTrue();
            actualResult.ShouldBeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Error()
        {
            const String errorMessage = "npm ERR! code E404\r\n";
            var expectedResult = new NpmMessageData(
                new ParsedValue<MessageType>(MessageType.Error, new Span(4, 4)),
                new ParsedValue<String>("code E404", new Span(9, 9))
            );

            var span = Utils.CreateSpan(errorMessage);
            var parser = new NpmMessageParser();

            NpmMessageData actualResult;
            var parsed = parser.TryParse(span, out actualResult);

            parsed.Should().BeTrue();
            actualResult.ShouldBeEquivalentTo(expectedResult);
        }
    }
}