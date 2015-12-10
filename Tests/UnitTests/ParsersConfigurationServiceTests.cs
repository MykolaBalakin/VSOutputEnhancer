using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Balakin.VSOutputEnhancer.Parsers.BuildResult;
using Balakin.VSOutputEnhancer.Parsers.BuildFileRelatedMessage;
using Balakin.VSOutputEnhancer.Parsers.DebugException;
using Balakin.VSOutputEnhancer.Parsers.DebugTraceMessage;
using Balakin.VSOutputEnhancer.Parsers.PublishResult;
using Balakin.VSOutputEnhancer.UnitTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ParsersConfigurationServiceTests {
        [TestMethod]
        public void BuildOutput() {
            var service = Utils.CreateParsersConfigurationService();
            var contentType = new ContentTypeStub(ContentType.BuildOutput);
            var parsers = service.GetParsers(contentType).ToList();
            Assert.AreEqual(3, parsers.Count);
            Assert.AreEqual(1, parsers.Count(p => p.Parser == typeof(BuildResultParser) && p.DataProcessor == typeof(BuildResultDataProcessor)));
            Assert.AreEqual(1, parsers.Count(p => p.Parser == typeof(BuildFileRelatedMessageParser) && p.DataProcessor == typeof(BuildFileRelatedMessageDataProcessor)));
            Assert.AreEqual(1, parsers.Count(p => p.Parser == typeof(PublishResultParser) && p.DataProcessor == typeof(PublishResultDataProcessor)));
        }

        [TestMethod]
        public void DebugOutput() {
            var service = Utils.CreateParsersConfigurationService();
            var contentType = new ContentTypeStub(ContentType.DebugOutput);
            var parsers = service.GetParsers(contentType).ToList();
            Assert.AreEqual(2, parsers.Count);
            Assert.AreEqual(1, parsers.Count(p => p.Parser == typeof(DebugExceptionParser) && p.DataProcessor == typeof(DebugExceptionDataProcessor)));
            Assert.AreEqual(1, parsers.Count(p => p.Parser == typeof(DebugTraceMessageParser) && p.DataProcessor == typeof(DebugTraceMessageDataProcessor)));
        }
    }
}
