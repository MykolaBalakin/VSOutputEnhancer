using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Parsers.PublishResult;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class PublishResultParserTests
    {
        [Theory]
        [InlineData("Message\r\n")]
        [InlineData("========== Build: 5 succeeded, 1 failed, 3 up-to-date, 2 skipped ==========\r\n")]
        [InlineData("========== Publish: bla bla\r\n")]
        [InlineData("========== Publish: bla bla ==========\r\n")]
        public void NotParsed(String message)
        {
            var span = Utils.CreateSpan(message);
            var parser = new PublishResultParser();

            PublishResultData actualResult;
            var parsed = parser.TryParse(span, out actualResult);

            parsed.Should().BeFalse();
            actualResult.Should().BeNull();
        }

        [Fact]
        public void Publish()
        {
            const String publishCompleteMessage = "========== Publish: 10 succeeded, 3 failed, 122 skipped ==========\r\n";
            var expectedResult = new PublishResultData(
                new ParsedValue<Int32>(10, new Span(20, 2)),
                new ParsedValue<Int32>(3, new Span(34, 1)),
                new ParsedValue<Int32>(122, new Span(44, 3))
            );

            var span = Utils.CreateSpan(publishCompleteMessage);
            PublishResultData actualData;

            var parser = new PublishResultParser();
            var parsed = parser.TryParse(span, out actualData);

            parsed.Should().BeTrue();
            actualData.ShouldBeEquivalentTo(expectedResult);
        }
    }
}