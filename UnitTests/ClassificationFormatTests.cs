using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Balakin.VSOutputEnhancer.Exports.Formats;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ClassificationFormatTests {
        [TestMethod]
        public void AllFormatsHasDisplayName() {
            var formatTypes = GetAllExportedFormats().ToList();

            var styleManager = Utils.CreateStyleManager();

            foreach (var formatType in formatTypes) {
                var format = (ClassificationFormatDefinition)Activator.CreateInstance(formatType, styleManager);
                Assert.IsNotNull(format.DisplayName, format.GetType().Name);
                Assert.IsFalse(String.IsNullOrEmpty(format.DisplayName), format.GetType().Name);
            }
        }

        [TestMethod]
        public void DisplayNames() {
            var formatTypes = GetAllExportedFormats().ToList();

            var styleManager = Utils.CreateStyleManager();

            foreach (var formatType in formatTypes) {
                var format = (ClassificationFormatDefinition)Activator.CreateInstance(formatType, styleManager);
                Assert.AreEqual(GetCorrectName(formatType), format.DisplayName);
            }
        }

        [TestMethod]
        public void NameClassNameAndClassificationNameEquals() {
            var formatTypes = GetAllExportedFormats().ToList();
            var incorrectClassNames = formatTypes.Where(t => {
                var classificationType = t.GetCustomAttribute<ClassificationTypeAttribute>().ClassificationTypeNames;
                return !t.Name.Equals(classificationType + "FormatDefinition", StringComparison.Ordinal);
            }).Select(t => t.Name).ToList();
            if (incorrectClassNames.Any()) {
                Assert.Fail("Classification formats with invalid class name: " + String.Join(", ", incorrectClassNames));
            }
            var incorrectNames = formatTypes.Where(t => {
                var classificationType = t.GetCustomAttribute<ClassificationTypeAttribute>().ClassificationTypeNames;
                var name = t.GetCustomAttribute<NameAttribute>().Name;
                return !name.Equals(classificationType, StringComparison.Ordinal);
            }).Select(t => t.Name).ToList();
            if (incorrectNames.Any()) {
                Assert.Fail("Classification formats with invalid NameAttribute value: " + String.Join(", ", incorrectNames));
            }
        }


        private IEnumerable<Type> GetAllExportedFormats() {
            var formatBaseType = typeof(ClassificationFormatDefinition);

            var assembly = typeof(StyleManager).Assembly;
            var formatTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(formatBaseType));
            formatTypes = formatTypes.Where(t => t.GetCustomAttribute<ExportAttribute>() != null);
            return formatTypes;
        }

        private String GetCorrectName(Type formatType) {
            if (formatType == typeof(BuildMessageErrorFormatDefinition)) {
                return "Output enhancer: Build error message";
            }
            if (formatType == typeof(BuildMessageWarningFormatDefinition)) {
                return "Output enhancer: Build warning message";
            }
            if (formatType == typeof(BuildResultFailedFormatDefinition)) {
                return "Output enhancer: Build failed";
            }
            if (formatType == typeof(BuildResultSucceededFormatDefinition)) {
                return "Output enhancer: Build succeeded";
            }
            if (formatType == typeof(PublishResultSucceededFormatDefinition)) {
                return "Output enhancer: Publish succeeded";
            }
            if (formatType == typeof(PublishResultFailedFormatDefinition)) {
                return "Output enhancer: Publish failed";
            }
            if (formatType == typeof(DebugExceptionFormatDefinition)) {
                return "Output enhancer: Debug exception message";
            }
            if (formatType == typeof(DebugTraceErrorFormatDefinition)) {
                return "Output enhancer: Trace error message";
            }
            if (formatType == typeof(DebugTraceWarningFormatDefinition)) {
                return "Output enhancer: Trace warning message";
            }

            throw new ArgumentOutOfRangeException(nameof(formatType));
        }
    }
}
