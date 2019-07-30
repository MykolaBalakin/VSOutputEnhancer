using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildResult;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Parsers
{
    [ExcludeFromCodeCoverage]
    public class BuildResultParserTests
    {
        [Theory]
        [InlineData("Message\r\n")]
        [InlineData("========== Publish: 10 succeeded, 3 failed, 122 skipped ==========\r\n")]
        [InlineData("========== Build: bla bla\r\n")]
        [InlineData("========== Build: bla bla ==========\r\n")]
        public void NotParsed(String message)
        {
            var span = Utils.CreateSpan(message);
            var parser = new BuildResultParser();
            BuildResultData parsedData;
            var parsed = parser.TryParse(span, out parsedData);

            parsed.Should().BeFalse();
            parsedData.Should().BeNull();
        }

        [Fact]
        public void Build()
        {
            const String publishCompleteMessage = "========== Build: 302 succeeded, 41 failed, 16 up-to-date, 5 skipped ==========\r\n";
            var expectedResult = new BuildResultData(
                new ParsedValue<Int32>(302, new Span(18, 3)),
                new ParsedValue<Int32>(41, new Span(33, 2)),
                new ParsedValue<Int32>(16, new Span(44, 2)),
                new ParsedValue<Int32>(5, new Span(59, 1))
            );

            var span = Utils.CreateSpan(publishCompleteMessage);
            BuildResultData parsedData;
            var parser = new BuildResultParser();
            var parsed = parser.TryParse(span, out parsedData);

            parsed.Should().BeTrue();
            parsedData.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void BuildDnx()
        {
            const String publishCompleteMessage = "========== Build: 10 succeeded or up-to-date, 3 failed, 43 skipped ==========\r\n";
            var expectedResult = new BuildResultData(
                new ParsedValue<Int32>(10, new Span(18, 2)),
                new ParsedValue<Int32>(3, new Span(46, 1)),
                new ParsedValue<Int32>(),
                new ParsedValue<Int32>(43, new Span(56, 2))
            );

            var span = Utils.CreateSpan(publishCompleteMessage);
            BuildResultData parsedData;
            var parser = new BuildResultParser();
            var parsed = parser.TryParse(span, out parsedData);

            parsed.Should().BeTrue();
            parsedData.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Rebuild()
        {
            const String publishCompleteMessage = "========== Rebuild All: 2 succeeded, 135 failed, 86 skipped ==========\r\n";
            var expectedResult = new BuildResultData(
                new ParsedValue<Int32>(2, new Span(24, 1)),
                new ParsedValue<Int32>(135, new Span(37, 3)),
                new ParsedValue<Int32>(),
                new ParsedValue<Int32>(86, new Span(49, 2))
            );

            var span = Utils.CreateSpan(publishCompleteMessage);
            BuildResultData parsedData;
            var parser = new BuildResultParser();
            var parsed = parser.TryParse(span, out parsedData);

            parsed.Should().BeTrue();
            parsedData.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void Clean()
        {
            const String cleanCompleteMessage = "========== Clean: 15 succeeded, 13 failed, 1 skipped ==========\r\n";
            var expectedResult = new BuildResultData(
                new ParsedValue<Int32>(15, new Span(18, 2)),
                new ParsedValue<Int32>(13, new Span(32, 2)),
                new ParsedValue<Int32>(),
                new ParsedValue<Int32>(1, new Span(43, 1))
            );

            var span = Utils.CreateSpan(cleanCompleteMessage);
            BuildResultData parsedData;
            var parser = new BuildResultParser();
            var parsed = parser.TryParse(span, out parsedData);

            parsed.Should().BeTrue();
            parsedData.Should().BeEquivalentTo(expectedResult);
        }
    }
}