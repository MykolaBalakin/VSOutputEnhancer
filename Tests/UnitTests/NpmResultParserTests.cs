using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Balakin.VSOutputEnhancer.Parsers.NpmResult;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class NpmResultParserTests
    {
        [TestMethod]
        public void NotParsed()
        {
            const String messageString = "Some message\r\n";
            const String messageString2 = "====npm command completed with exit code asdf====\r\n";

            var span = Utils.CreateSpan(messageString);
            var parser = new NpmResultParser();
            NpmResultData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsFalse(parsed);
            Assert.IsNull(data);

            span = Utils.CreateSpan(messageString2);
            parser = new NpmResultParser();
            parsed = parser.TryParse(span, out data);
            Assert.IsFalse(parsed);
            Assert.IsNull(data);
        }

        [TestMethod]
        public void ExitCode()
        {
            const String messageString = "====npm command completed with exit code 1823====\r\n";

            var span = Utils.CreateSpan(messageString);
            var parser = new NpmResultParser();
            NpmResultData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsTrue(parsed);
            Assert.IsNotNull(data);

            Assert.IsTrue(data.ExitCode.HasValue);

            Assert.AreEqual(1823, data.ExitCode);

            Assert.AreEqual(new Span(41, 4), data.ExitCode.Span);
        }

        [TestMethod]
        public void NegativeExitCode()
        {
            const String messageString = "====npm command completed with exit code -8====\r\n";

            var span = Utils.CreateSpan(messageString);
            var parser = new NpmResultParser();
            NpmResultData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsTrue(parsed);
            Assert.IsNotNull(data);

            Assert.IsTrue(data.ExitCode.HasValue);

            Assert.AreEqual(-8, data.ExitCode);

            Assert.AreEqual(new Span(41, 2), data.ExitCode.Span);
        }
    }
}