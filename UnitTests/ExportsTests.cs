using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ExportsTests {
        [TestMethod]
        public void AllDefinitionsExported() {
            var exportAttribute = typeof(ExportAttribute);
            var classificationTypeDefinition=typeof(ClassificationTypeDefinition);
            var assembly = typeof(ClassificationType).Assembly;
            var types = assembly.GetTypes();
            var exportedDefinitions = types
                .SelectMany(t => t.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
                .Where(f => f.CustomAttributes.Any(a => a.AttributeType == exportAttribute))
                .Where(f => f.GetCustomAttributes<ExportAttribute>().Any(a => a.ContractType == classificationTypeDefinition))
                .SelectMany(f => f.GetCustomAttributes<NameAttribute>().Select(a => a.Name))
                .Distinct()
                .ToList();

            var allDefinitions = ClassificationType.All;
            var notExportedDefinitions = allDefinitions.Except(exportedDefinitions).ToList();
            if (notExportedDefinitions.Any()) {
                Assert.Fail("Not exported classification definitions: " + String.Join(", ", notExportedDefinitions));
            }
        }
    }
}
