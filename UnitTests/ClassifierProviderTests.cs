using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.Classifiers;
using Balakin.VSOutputEnhancer.Exports;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ClassifierProviderTests {
        [TestMethod]
        public void BuildOutput() {
            var provider = CreateClassifierProvider();
            var textBuffer = Utils.CreateTextBuffer(ContentType.BuildOutput);
            var classifier = provider.GetClassifier(textBuffer);
            Assert.IsNotNull(classifier);
        }

        [TestMethod]
        public void Debug() {
            var provider = CreateClassifierProvider();
            var textBuffer = Utils.CreateTextBuffer(ContentType.DebugOutput);
            var classifier = provider.GetClassifier(textBuffer);
            Assert.IsNotNull(classifier);
        }

        [TestMethod]
        public void UnknownContentType() {
            var provider = CreateClassifierProvider();
            var textBuffer = Utils.CreateTextBuffer("UnknownContentType");
            var classifier = provider.GetClassifier(textBuffer);
            Assert.IsNull(classifier);
        }

        ClassifierProvider CreateClassifierProvider() {
            var factory = Utils.CreateClassifierFactory();
            var provider = new ClassifierProvider(factory);
            return provider;
        }
    }
}
