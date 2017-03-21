using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Balakin.VSOutputEnhancer.Parsers.DebugTraceMessage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Parsers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DebugTraceMessageParserTests
    {
        [TestMethod]
        public void NotParsed()
        {
            const String messageString = "Some message\r\n";

            var span = Utils.CreateSpan(messageString);
            var parser = new DebugTraceMessageParser();
            DebugTraceMessageData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsFalse(parsed);
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Error()
        {
            const String messageString = "VSOutputEnhancerDemo.vshost.exe Information: 10 : Trace information message\r\n";

            var span = Utils.CreateSpan(messageString);
            var parser = new DebugTraceMessageParser();
            DebugTraceMessageData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsTrue(parsed);
            Assert.IsNotNull(data);

            Assert.IsTrue(data.Source.HasValue);
            Assert.IsTrue(data.Type.HasValue);
            Assert.IsTrue(data.Id.HasValue);
            Assert.IsTrue(data.Message.HasValue);
            Assert.IsTrue(data.PrettyMessage.HasValue);

            Assert.AreEqual("VSOutputEnhancerDemo.vshost.exe", data.Source);
            Assert.AreEqual(TraceEventType.Information, data.Type);
            Assert.AreEqual(10, data.Id);
            Assert.AreEqual("Trace information message", data.Message);
            Assert.AreEqual("Information: 10 : Trace information message", data.PrettyMessage);

            Assert.AreEqual(new Span(0, 31), data.Source.Span);
            Assert.AreEqual(new Span(32, 11), data.Type.Span);
            Assert.AreEqual(new Span(45, 2), data.Id.Span);
            Assert.AreEqual(new Span(50, 25), data.Message.Span);
            Assert.AreEqual(new Span(32, 43), data.PrettyMessage.Span);
        }
    }
}