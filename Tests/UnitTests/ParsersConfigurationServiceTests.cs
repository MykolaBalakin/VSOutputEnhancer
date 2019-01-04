using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Parsers.BowerMessage;
using Balakin.VSOutputEnhancer.Parsers.BuildFileRelatedMessage;
using Balakin.VSOutputEnhancer.Parsers.BuildProjectHeader;
using Balakin.VSOutputEnhancer.Parsers.BuildResult;
using Balakin.VSOutputEnhancer.Parsers.DebugException;
using Balakin.VSOutputEnhancer.Parsers.DebugTraceMessage;
using Balakin.VSOutputEnhancer.Parsers.NpmMessage;
using Balakin.VSOutputEnhancer.Parsers.NpmResult;
using Balakin.VSOutputEnhancer.Parsers.PublishResult;
using Balakin.VSOutputEnhancer.Tests.Stubs;
using FluentAssertions;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class ParsersConfigurationServiceTests
    {
        [Fact]
        public void BuildOutput()
        {
            TestBuildOutput(ContentType.BuildOutput);
        }

        [Fact]
        public void BuildOrderOutput()
        {
            TestBuildOutput(ContentType.BuildOrderOutput);
        }

        private void TestBuildOutput(String contentType)
        {
            var expectedResult = new[]
            {
                new ParserConfiguration(typeof(BuildProjectHeaderParser), typeof(BuildProjectHeaderData), typeof(BuildProjectHeaderProcessor)),
                new ParserConfiguration(typeof(BuildResultParser), typeof(BuildResultData), typeof(BuildResultDataProcessor)),
                new ParserConfiguration(typeof(BuildFileRelatedMessageParser), typeof(BuildFileRelatedMessageData), typeof(BuildFileRelatedMessageDataProcessor)),
                new ParserConfiguration(typeof(PublishResultParser), typeof(PublishResultData), typeof(PublishResultDataProcessor)),
                new ParserConfiguration(typeof(NpmMessageParser), typeof(NpmMessageData), typeof(NpmMessageDataProcessor))
            };

            var service = Utils.CreateParsersConfigurationService();
            var parsers = service.GetParsers(new ContentTypeStub(contentType));

            parsers.ShouldAllBeEquivalentTo(expectedResult);
        }

        [Fact]
        public void DebugOutput()
        {
            var expectedResult = new[]
            {
                new ParserConfiguration(typeof(DebugExceptionParser), typeof(DebugExceptionData), typeof(DebugExceptionDataProcessor)),
                new ParserConfiguration(typeof(DebugTraceMessageParser), typeof(DebugTraceMessageData), typeof(DebugTraceMessageDataProcessor))
            };

            var service = Utils.CreateParsersConfigurationService();
            var contentType = new ContentTypeStub(ContentType.DebugOutput);
            var parsers = service.GetParsers(contentType);

            parsers.ShouldAllBeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GeneralOutput()
        {
            var expectedResult = new[]
            {
                new ParserConfiguration(typeof(NpmMessageParser), typeof(NpmMessageData), typeof(NpmMessageDataProcessor)),
                new ParserConfiguration(typeof(NpmResultParser), typeof(NpmResultData), typeof(NpmResultDataProcessor)),
                new ParserConfiguration(typeof(BowerMessageParser), typeof(BowerMessageData), typeof(BowerMessageDataProcessor))
            };

            var service = Utils.CreateParsersConfigurationService();
            var contentType = new ContentTypeStub(ContentType.Output);
            var parsers = service.GetParsers(contentType);

            parsers.ShouldAllBeEquivalentTo(expectedResult);
        }
    }
}