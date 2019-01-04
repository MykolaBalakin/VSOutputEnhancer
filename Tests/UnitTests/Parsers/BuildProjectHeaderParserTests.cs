using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Parsers.BuildProjectHeader;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Parsers
{
    [ExcludeFromCodeCoverage]
    public class BuildProjectHeaderParserTests
    {
        [Theory]
        [InlineData("Message\r\n")]
        [InlineData("1>------ Publish: 10 succeeded, 3 failed, 122 skipped\r\n")]
        [InlineData("------ Anything ------\r\n")]
        [InlineData("Anything ------\r\n")]
        public void NotParsed(String message)
        {
            var span = Utils.CreateSpan(message);
            var parser = new BuildProjectHeaderParser();
            BuildProjectHeaderData parsedData;
            var parsed = parser.TryParse(span, out parsedData);

            parsed.Should().BeFalse();
            parsedData.Should().BeNull();
        }

        [Fact]
        public void Build()
        {
            const String publishCompleteMessage = "1>------ Rebuild All started: Project: VSOutputEnhancer, Configuration: Debug Any CPU ------\r\n";
            var expectedResult = new BuildProjectHeaderData(
                new ParsedValue<Int32>(1, new Span(0, 1)),
                new ParsedValue<String>("Rebuild All started: Project: VSOutputEnhancer, Configuration: Debug Any CPU", new Span(9, 76))
            );

            var span = Utils.CreateSpan(publishCompleteMessage);
            BuildProjectHeaderData parsedData;
            var parser = new BuildProjectHeaderParser();
            var parsed = parser.TryParse(span, out parsedData);

            parsed.Should().BeTrue();
            parsedData.ShouldBeEquivalentTo(expectedResult);
        }
    }
}