using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ClassifierProviderTests {
        [TestMethod]
        public void BuildOutput() {
            var provider = CreateClassifierProvider();
            var textBuffer = Utils.CreateTextBuffer("BuildOutput");
            var classifier = provider.GetClassifier(textBuffer);
            Assert.IsNotNull(classifier);
            Assert.IsInstanceOfType(classifier, typeof(BuildOutputClassifier));
        }

        [TestMethod]
        public void Debug() {
            var provider = CreateClassifierProvider();
            var textBuffer = Utils.CreateTextBuffer("Debug");
            var classifier = provider.GetClassifier(textBuffer);
            Assert.IsNotNull(classifier);
            Assert.IsInstanceOfType(classifier, typeof(DebugClassifier));
        }

        ClassifierProvider CreateClassifierProvider() {
            var registry = Utils.CreateClassificationTypeRegistryService();
            var provider = new ClassifierProvider(registry);
            return provider;
        }
    }
}
