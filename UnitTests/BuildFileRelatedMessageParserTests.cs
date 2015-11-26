using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BuildFileRelatedMessageParserTests {
        [TestMethod]
        public void Warning1() {
            const String warningMessage = "1>C:\\Sources\\GitHub\\VSOutputEnhancer\\VSOutputEnhancer\\ClassificationType.cs(29,53,29,83): warning CS0169: The field 'ClassificationType.BuildResultSucceededDefinition' is never used\r\n";

            var expectedMessage = "The field 'ClassificationType.BuildResultSucceededDefinition' is never used";
            var expectedMessageType = BuildMessageType.Warning;
            var expectedMessageStart = 90;
            var expectedMessageLength = 91;

            TestMessageParsed(warningMessage, expectedMessage, expectedMessageType, expectedMessageStart, expectedMessageLength);
        }

        [TestMethod]
        public void Warning2() {
            const String warningMessage = "9>C:\\Sources\\Atea\\Application Manager\\Development\\Dev\\Src\\Installer\\Atea.AppMgr.Installer.Site\\AppPoolDlg.wxs(9,0): warning CNDL1000: The Binary/@Id attribute's value, 'GetUserCredentialsCA', is 20 characters long.  It will be too long if modularized.  The identifier shouldn't be longer than 18 characters long to allow for modularization (appending a guid for merge modules).\r\n";

            var expectedMessage = "The Binary/@Id attribute's value, 'GetUserCredentialsCA', is 20 characters long.  It will be too long if modularized.  The identifier shouldn't be longer than 18 characters long to allow for modularization (appending a guid for merge modules).";
            var expectedMessageType = BuildMessageType.Warning;
            var expectedMessageStart = 116;
            var expectedMessageLength = 261;

            TestMessageParsed(warningMessage, expectedMessage, expectedMessageType, expectedMessageStart, expectedMessageLength);
        }

        private void TestMessageParsed(String fullText, String expectedMessage, BuildMessageType expectedType, Int32 expectedMessageStart, Int32 expectedMessageLength) {
            var span = Utils.CreateSpan(fullText);
            BuildFileRelatedMessage message;
            var parsed = BuildFileRelatedMessage.TryParse(span, out message);
            Assert.IsTrue(parsed, "Parsed");
            Assert.AreEqual(expectedMessage, message.Message, "Message text");
            Assert.AreEqual(expectedType, message.MessageType, "Message type");
            Assert.AreEqual(expectedMessageStart, message.Span.Start, "Message offset");
            Assert.AreEqual(expectedMessageLength, message.Span.Length, "Message length");
        }
    }
}
