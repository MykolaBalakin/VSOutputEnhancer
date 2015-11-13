using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.Exports.Formats;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    public class ClassificationFormatTests {
        [TestMethod]
        public void DisplayNames() {
            var formatTypes = GetAllExportedFormats().ToList();

            var styleManager = CreateStyleManager();

            foreach (var formatType in formatTypes) {
                var format = (ClassificationFormatDefinition)Activator.CreateInstance(formatType, styleManager);
                Assert.AreEqual(GetCorrectName(formatType), format);
            }
        }

        private StyleManager CreateStyleManager() {
            var styleManager = new Balakin.VSOutputEnhancer.Fakes.StubStyleManager();
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
                return "Build error message";
            }
            if (formatType == typeof(BuildMessageWarningClassifierFormat)) {
                return "Build warning message";
            }
            if (formatType == typeof(BuildResultFailedClassifierFormat)) {
                return "Build failed";
            }
            if (formatType == typeof(BuildResultSucceededClassifierFormat)) {
                return "Build succeeded";
            }

            throw new ArgumentOutOfRangeException(nameof(formatType));
        }
    }
}
