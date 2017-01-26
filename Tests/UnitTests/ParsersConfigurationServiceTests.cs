using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Balakin.VSOutputEnhancer.Parsers.BowerMessage;
using Balakin.VSOutputEnhancer.Parsers.BuildFileRelatedMessage;
using Balakin.VSOutputEnhancer.Parsers.BuildResult;
using Balakin.VSOutputEnhancer.Parsers.DebugException;
using Balakin.VSOutputEnhancer.Parsers.DebugTraceMessage;
using Balakin.VSOutputEnhancer.Parsers.NpmMessage;
using Balakin.VSOutputEnhancer.Parsers.NpmResult;
using Balakin.VSOutputEnhancer.Parsers.PublishResult;
using Balakin.VSOutputEnhancer.Tests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ParsersConfigurationServiceTests
    {
        [TestMethod]
        public void BuildOutput()
        {
            TestBuildOutput(ContentType.BuildOutput);
        }

        [TestMethod]
        public void BuildOrderOutput()
        {
            TestBuildOutput(ContentType.BuildOrderOutput);
        }

        private void TestBuildOutput(String contentType)
        {
            var service = Utils.CreateParsersConfigurationService();
            var parsers = service.GetParsers(new ContentTypeStub(contentType)).ToList();
            Assert.AreEqual(4, parsers.Count);
            Assert.AreEqual(1, parsers.Count(p => p.Parser == typeof(BuildResultParser) && p.DataProcessor == typeof(BuildResultDataProcessor)));
            Assert.AreEqual(1, parsers.Count(p => p.Parser == typeof(NpmMessageParser) && p.DataProcessor == typeof(NpmMessageDataProcessor)));
            Assert.AreEqual(1, parsers.Count(p => p.Parser == typeof(BuildFileRelatedMessageParser) && p.DataProcessor == typeof(BuildFileRelatedMessageDataProcessor)));
            Assert.AreEqual(1, parsers.Count(p => p.Parser == typeof(PublishResultParser) && p.DataProcessor == typeof(PublishResultDataProcessor)));
        }

        [TestMethod]
        public void DebugOutput()
        {
            var service = Utils.CreateParsersConfigurationService();
            var contentType = new ContentTypeStub(ContentType.DebugOutput);
            var parsers = service.GetParsers(contentType).ToList();
            Assert.AreEqual(2, parsers.Count);
            Assert.AreEqual(1, parsers.Count(p => p.Parser == typeof(DebugExceptionParser) && p.DataProcessor == typeof(DebugExceptionDataProcessor)));
            Assert.AreEqual(1, parsers.Count(p => p.Parser == typeof(DebugTraceMessageParser) && p.DataProcessor == typeof(DebugTraceMessageDataProcessor)));
        }

        [TestMethod]
        public void GeneralOutput()
        {
            var service = Utils.CreateParsersConfigurationService();
            var contentType = new ContentTypeStub(ContentType.Output);
            var parsers = service.GetParsers(contentType).ToList();
            Assert.AreEqual(3, parsers.Count);
            Assert.AreEqual(1, parsers.Count(p => p.Parser == typeof(NpmMessageParser) && p.DataProcessor == typeof(NpmMessageDataProcessor)));
            Assert.AreEqual(1, parsers.Count(p => p.Parser == typeof(NpmResultParser) && p.DataProcessor == typeof(NpmResultDataProcessor)));
            Assert.AreEqual(1, parsers.Count(p => p.Parser == typeof(BowerMessageParser) && p.DataProcessor == typeof(BowerMessageDataProcessor)));
        }
    }
}