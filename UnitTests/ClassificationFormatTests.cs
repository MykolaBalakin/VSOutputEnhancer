using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.Exports.Formats;
using Balakin.VSOutputEnhancer.UnitTests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ClassificationFormatTests {
        [TestMethod]
        public void AllFormatsHasDisplayName() {
            var formatTypes = GetAllExportedFormats().ToList();

            var styleManager = CreateStyleManager();

            foreach (var formatType in formatTypes) {
                var format = (ClassificationFormatDefinition)Activator.CreateInstance(formatType, styleManager);
                Assert.IsNotNull(format.DisplayName, format.GetType().Name);
                Assert.IsFalse(String.IsNullOrEmpty(format.DisplayName), format.GetType().Name);
            }
        }

        [TestMethod]
        public void DisplayNames() {
            var formatTypes = GetAllExportedFormats().ToList();

            var styleManager = CreateStyleManager();

            foreach (var formatType in formatTypes) {
                var format = (ClassificationFormatDefinition)Activator.CreateInstance(formatType, styleManager);
                Assert.AreEqual(GetCorrectName(formatType), format.DisplayName);
            }
        }

        private IStyleManager CreateStyleManager() {
            var styleManager = new StyleManagerStub();
            return styleManager;
        }

        private IEnumerable<Type> GetAllExportedFormats() {
            var formatBaseType = typeof(ClassificationFormatDefinition);

            var assembly = typeof(StyleManager).Assembly;
            var formatTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(formatBaseType));
            formatTypes = formatTypes.Where(t => t.GetCustomAttribute<ExportAttribute>() != null);
            return formatTypes;
        }

        private String GetCorrectName(Type formatType) {
            if (formatType == typeof(BuildMessageErrorClassifierFormat)) {
                return "Output enhancer: Build error message";
            }
            if (formatType == typeof(BuildMessageWarningClassifierFormat)) {
                return "Output enhancer: Build warning message";
            }
            if (formatType == typeof(BuildResultFailedClassifierFormat)) {
                return "Output enhancer: Build failed";
            }
            if (formatType == typeof(BuildResultSucceededClassifierFormat)) {
                return "Output enhancer: Build succeeded";
            }
            if (formatType == typeof(PublishResultSucceededClassifierFormat)) {
                return "Output enhancer: Publish succeeded";
            }
            if (formatType == typeof(PublishResultFailedClassifierFormat)) {
                return "Output enhancer: Publish failed";
            }

            throw new ArgumentOutOfRangeException(nameof(formatType));
        }
    }
}
