using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Parsers.NpmMessage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class NpmMessageParserTests
    {
        [TestMethod]
        public void NotParsed()
        {
            const String messageString = "Some message\r\n";
            const String messageString2 = "npm \r\n";

            var span = Utils.CreateSpan(messageString);
            var parser = new NpmMessageParser();
            NpmMessageData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsFalse(parsed);
            Assert.IsNull(data);

            span = Utils.CreateSpan(messageString2);
            parser = new NpmMessageParser();
            parsed = parser.TryParse(span, out data);
            Assert.IsFalse(parsed);
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Warning()
        {
            const String warningMessage = "npm WARN package.json ASP.NET@0.0.0 No README data\r\n";

            var span = Utils.CreateSpan(warningMessage);
            var parser = new NpmMessageParser();
            NpmMessageData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsTrue(parsed);
            Assert.IsNotNull(data);

            Assert.IsTrue(data.Message.HasValue);
            Assert.IsTrue(data.Type.HasValue);

            Assert.AreEqual(MessageType.Warning, data.Type);
            Assert.AreEqual("package.json ASP.NET@0.0.0 No README data", data.Message);

            Assert.AreEqual(new Span(4, 4), data.Type.Span);
            Assert.AreEqual(new Span(9, 41), data.Message.Span);
        }

        [TestMethod]
        public void Error()
        {
            const String errorMessage = "npm ERR! code E404\r\n";

            var span = Utils.CreateSpan(errorMessage);
            var parser = new NpmMessageParser();
            NpmMessageData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsTrue(parsed);
            Assert.IsNotNull(data);

            Assert.IsTrue(data.Message.HasValue);
            Assert.IsTrue(data.Type.HasValue);

            Assert.AreEqual(MessageType.Error, data.Type);
            Assert.AreEqual("code E404", data.Message);

            Assert.AreEqual(new Span(4, 4), data.Type.Span);
            Assert.AreEqual(new Span(9, 9), data.Message.Span);
        }
    }
}