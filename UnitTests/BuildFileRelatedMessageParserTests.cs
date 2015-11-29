using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Parsers.BuildFileRelatedMessage;
using Balakin.VSOutputEnhancer.Parsers.BuildMessage;
using Balakin.VSOutputEnhancer.Parsers.DebugTraceMessage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BuildFileRelatedMessageParserTests {
        [TestMethod]
        public void NotParsed() {
            const String messageString = "Some message\r\n";

            var span = Utils.CreateSpan(messageString);
            var parser = new BuildFileRelatedMessageParser();
            BuildFileRelatedMessageData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsFalse(parsed);
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Warning1() {
            const String warningMessage = "1>C:\\Sources\\GitHub\\VSOutputEnhancer\\VSOutputEnhancer\\ClassificationType.cs(29,53,29,83): warning CS0169: The field 'ClassificationType.BuildResultSucceededDefinition' is never used\r\n";

            var span = Utils.CreateSpan(warningMessage);
            var parser = new BuildFileRelatedMessageParser();
            BuildFileRelatedMessageData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsTrue(parsed);
            Assert.IsNotNull(data);

            Assert.IsTrue(data.FilePath.HasValue);
            Assert.IsTrue(data.Type.HasValue);
            Assert.IsTrue(data.Code.HasValue);
            Assert.IsTrue(data.Message.HasValue);
            Assert.IsTrue(data.FullMessage.HasValue);

            Assert.AreEqual("C:\\Sources\\GitHub\\VSOutputEnhancer\\VSOutputEnhancer\\ClassificationType.cs", data.FilePath);
            Assert.AreEqual(BuildMessageType.Warning, data.Type);
            Assert.AreEqual("CS0169", data.Code);
            Assert.AreEqual("The field 'ClassificationType.BuildResultSucceededDefinition' is never used", data.Message);
            Assert.AreEqual("warning CS0169: The field 'ClassificationType.BuildResultSucceededDefinition' is never used", data.FullMessage);

            Assert.AreEqual(new Span(2, 73), data.FilePath.Span);
            Assert.AreEqual(new Span(90, 7), data.Type.Span);
            Assert.AreEqual(new Span(98, 6), data.Code.Span);
            Assert.AreEqual(new Span(106, 75), data.Message.Span);
            Assert.AreEqual(new Span(90, 91), data.FullMessage.Span);
        }

        [TestMethod]
        public void Warning2() {
            const String warningMessage = "9>C:\\Sources\\Atea\\Application Manager\\Development\\Dev\\Src\\Installer\\Atea.AppMgr.Installer.Site\\AppPoolDlg.wxs(9,0): warning CNDL1000: The Binary/@Id attribute's value, 'GetUserCredentialsCA', is 20 characters long.  It will be too long if modularized.  The identifier shouldn't be longer than 18 characters long to allow for modularization (appending a guid for merge modules).\r\n";

            var span = Utils.CreateSpan(warningMessage);
            var parser = new BuildFileRelatedMessageParser();
            BuildFileRelatedMessageData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsTrue(parsed);
            Assert.IsNotNull(data);

            Assert.IsTrue(data.FilePath.HasValue);
            Assert.IsTrue(data.Type.HasValue);
            Assert.IsTrue(data.Code.HasValue);
            Assert.IsTrue(data.Message.HasValue);
            Assert.IsTrue(data.FullMessage.HasValue);

            Assert.AreEqual("C:\\Sources\\Atea\\Application Manager\\Development\\Dev\\Src\\Installer\\Atea.AppMgr.Installer.Site\\AppPoolDlg.wxs", data.FilePath);
            Assert.AreEqual(BuildMessageType.Warning, data.Type);
            Assert.AreEqual("CNDL1000", data.Code);
            Assert.AreEqual("The Binary/@Id attribute's value, 'GetUserCredentialsCA', is 20 characters long.  It will be too long if modularized.  The identifier shouldn't be longer than 18 characters long to allow for modularization (appending a guid for merge modules).", data.Message);
            Assert.AreEqual("warning CNDL1000: The Binary/@Id attribute's value, 'GetUserCredentialsCA', is 20 characters long.  It will be too long if modularized.  The identifier shouldn't be longer than 18 characters long to allow for modularization (appending a guid for merge modules).", data.FullMessage);

            Assert.AreEqual(new Span(2, 107), data.FilePath.Span);
            Assert.AreEqual(new Span(116, 7), data.Type.Span);
            Assert.AreEqual(new Span(124, 8), data.Code.Span);
            Assert.AreEqual(new Span(134, 243), data.Message.Span);
            Assert.AreEqual(new Span(116, 261), data.FullMessage.Span);
        }
    }
}
