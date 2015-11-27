using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DebugTraceMessageParserTests {
        [TestMethod]
        public void NotParsed() {
            const String messageString = "Some message\r\n";

            var span = Utils.CreateSpan(messageString);
            var parser = new DebugTraceMessageParser();
            DebugTraceMessageParsedData parsedData;
            var parsed = parser.TryParse(span, out parsedData);
            Assert.IsFalse(parsed);
            Assert.IsNull(parsedData);
        }

        [TestMethod]
        public void Error() {
            const String messageString = "VSOutputEnhancerDemo.vshost.exe Information: 10 : Trace information message\r\n";

            var span = Utils.CreateSpan(messageString);
            var parser = new DebugTraceMessageParser();
            DebugTraceMessageParsedData parsedData;
            var parsed = parser.TryParse(span, out parsedData);
            Assert.IsTrue(parsed);
            Assert.IsNotNull(parsedData);

            Assert.AreEqual("VSOutputEnhancerDemo.vshost.exe", parsedData.Source);
            Assert.AreEqual(TraceEventType.Information, parsedData.Type);
            Assert.AreEqual(10, parsedData.Id);
            Assert.AreEqual("Trace information message", parsedData.Message);
            Assert.AreEqual("Information: 10 : Trace information message", parsedData.PrettyMessage);

            Assert.AreEqual(new Span(0, 31), parsedData.Source.Span);
            Assert.AreEqual(new Span(32, 11), parsedData.Type.Span);
            Assert.AreEqual(new Span(45, 2), parsedData.Id.Span);
            Assert.AreEqual(new Span(50, 25), parsedData.Message.Span);
            Assert.AreEqual(new Span(32, 43), parsedData.PrettyMessage.Span);
        }
    }
}
