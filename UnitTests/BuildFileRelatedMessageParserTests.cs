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

            Assert.IsTrue(data.BuildTaskId.HasValue);
            Assert.IsTrue(data.FilePath.HasValue);
            Assert.IsTrue(data.Type.HasValue);
            Assert.IsTrue(data.Code.HasValue);
            Assert.IsTrue(data.Message.HasValue);
            Assert.IsTrue(data.FullMessage.HasValue);

            Assert.AreEqual(1, data.BuildTaskId);
            Assert.AreEqual("C:\\Sources\\GitHub\\VSOutputEnhancer\\VSOutputEnhancer\\ClassificationType.cs", data.FilePath);
            Assert.AreEqual(BuildMessageType.Warning, data.Type);
            Assert.AreEqual("CS0169", data.Code);
            Assert.AreEqual("The field 'ClassificationType.BuildResultSucceededDefinition' is never used", data.Message);
            Assert.AreEqual("warning CS0169: The field 'ClassificationType.BuildResultSucceededDefinition' is never used", data.FullMessage);

            Assert.AreEqual(new Span(0, 1), data.BuildTaskId.Span);
            Assert.AreEqual(new Span(2, 73), data.FilePath.Span);
            Assert.AreEqual(new Span(90, 7), data.Type.Span);
            Assert.AreEqual(new Span(98, 6), data.Code.Span);
            Assert.AreEqual(new Span(106, 75), data.Message.Span);
            Assert.AreEqual(new Span(90, 91), data.FullMessage.Span);
        }

        [TestMethod]
        public void Warning2() {
            const String warningMessage = "9>C:\\Sources\\Some project path\\AppPoolDlg.wxs(9,0): warning CNDL1000: The Binary/@Id attribute's value, 'GetUserCredentialsCA', is 20 characters long.  It will be too long if modularized.  The identifier shouldn't be longer than 18 characters long to allow for modularization (appending a guid for merge modules).\r\n";

            var span = Utils.CreateSpan(warningMessage);
            var parser = new BuildFileRelatedMessageParser();
            BuildFileRelatedMessageData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsTrue(parsed);
            Assert.IsNotNull(data);

            Assert.IsTrue(data.BuildTaskId.HasValue);
            Assert.IsTrue(data.FilePath.HasValue);
            Assert.IsTrue(data.Type.HasValue);
            Assert.IsTrue(data.Code.HasValue);
            Assert.IsTrue(data.Message.HasValue);
            Assert.IsTrue(data.FullMessage.HasValue);

            Assert.AreEqual(9, data.BuildTaskId);
            Assert.AreEqual("C:\\Sources\\Some project path\\AppPoolDlg.wxs", data.FilePath);
            Assert.AreEqual(BuildMessageType.Warning, data.Type);
            Assert.AreEqual("CNDL1000", data.Code);
            Assert.AreEqual("The Binary/@Id attribute's value, 'GetUserCredentialsCA', is 20 characters long.  It will be too long if modularized.  The identifier shouldn't be longer than 18 characters long to allow for modularization (appending a guid for merge modules).", data.Message);
            Assert.AreEqual("warning CNDL1000: The Binary/@Id attribute's value, 'GetUserCredentialsCA', is 20 characters long.  It will be too long if modularized.  The identifier shouldn't be longer than 18 characters long to allow for modularization (appending a guid for merge modules).", data.FullMessage);

            Assert.AreEqual(new Span(0, 1), data.BuildTaskId.Span);
            Assert.AreEqual(new Span(2, 43), data.FilePath.Span);
            Assert.AreEqual(new Span(52, 7), data.Type.Span);
            Assert.AreEqual(new Span(60, 8), data.Code.Span);
            Assert.AreEqual(new Span(70, 243), data.Message.Span);
            Assert.AreEqual(new Span(52, 261), data.FullMessage.Span);
        }

        [TestMethod]
        public void SingleBuildTask() {
            const String warningMessage = "C:\\Sources\\Local\\AppConfigWix\\AppConfigWix\\Product.wxs(83,0): warning CNDL1138: The RegistryKey/@Action attribute has been deprecated.  In most cases, you can simply omit @Action.  If you need to force Windows Installer to create an empty key or recursively delete the key, use the ForceCreateOnInstall or ForceDeleteOnUninstall attributes instead.\r\n";

            var span = Utils.CreateSpan(warningMessage);
            var parser = new BuildFileRelatedMessageParser();
            BuildFileRelatedMessageData data;
            var parsed = parser.TryParse(span, out data);
            Assert.IsTrue(parsed);
            Assert.IsNotNull(data);

            Assert.IsFalse(data.BuildTaskId.HasValue);
            Assert.IsTrue(data.FilePath.HasValue);
            Assert.IsTrue(data.Type.HasValue);
            Assert.IsTrue(data.Code.HasValue);
            Assert.IsTrue(data.Message.HasValue);
            Assert.IsTrue(data.FullMessage.HasValue);

            Assert.AreEqual("C:\\Sources\\Local\\AppConfigWix\\AppConfigWix\\Product.wxs", data.FilePath);
            Assert.AreEqual(BuildMessageType.Warning, data.Type);
            Assert.AreEqual("CNDL1138", data.Code);
            Assert.AreEqual("The RegistryKey/@Action attribute has been deprecated.  In most cases, you can simply omit @Action.  If you need to force Windows Installer to create an empty key or recursively delete the key, use the ForceCreateOnInstall or ForceDeleteOnUninstall attributes instead.", data.Message);
            Assert.AreEqual("warning CNDL1138: The RegistryKey/@Action attribute has been deprecated.  In most cases, you can simply omit @Action.  If you need to force Windows Installer to create an empty key or recursively delete the key, use the ForceCreateOnInstall or ForceDeleteOnUninstall attributes instead.", data.FullMessage);

            Assert.AreEqual(new Span(0, 54), data.FilePath.Span);
            Assert.AreEqual(new Span(62, 7), data.Type.Span);
            Assert.AreEqual(new Span(70, 8), data.Code.Span);
            Assert.AreEqual(new Span(80, 268), data.Message.Span);
            Assert.AreEqual(new Span(62, 286), data.FullMessage.Span);
        }
    }
}
