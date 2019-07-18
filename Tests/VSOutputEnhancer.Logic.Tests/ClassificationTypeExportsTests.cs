using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using Xunit;

namespace Balakin.VSOutputEnhancer.Logic.Tests
{
    [ExcludeFromCodeCoverage]
    public class ClassificationTypeExportsTests
    {
        [Fact]
        public void AllClassificationTypeDefinitionsAreExported()
        {
            var exportedDefinitions = EnumerateClassificationTypes();

            var allDefinitions = ClassificationType.All;
            var notExportedDefinitions = allDefinitions.Except(exportedDefinitions).ToList();
            notExportedDefinitions.Should().BeEmpty("Not exported classification definitions: " + String.Join(", ", notExportedDefinitions));
        }

        [Theory]
        [MemberData(nameof(ClassificationTypesTheoryData))]
        public void AllClassificationTypesHaveFormatDefinitionExported(String classificationType)
        {
            var formatDefinitionType = typeof(ClassificationFormatDefinition);
            var assembly = typeof(ClassificationType).Assembly;
            var allFormatDefinitions = assembly.GetTypes().Where(formatDefinitionType.IsAssignableFrom);

            allFormatDefinitions.Should().ContainSingle(t => t.GetCustomAttributes<ClassificationTypeAttribute>().Any(a => a.ClassificationTypeNames.Contains(classificationType)));
        }

        public static IEnumerable<Object[]> ClassificationTypesTheoryData()
        {
            var classificationTypesWithoutFormat = new[] { ClassificationType.DebugTraceInformation };

            return EnumerateClassificationTypes()
                .Where(t => !classificationTypesWithoutFormat.Contains(t))
                .Select(t => new Object[] { t });
        }

        private static IEnumerable<String> EnumerateClassificationTypes()
        {
            var exportAttribute = typeof(ExportAttribute);
            var classificationTypeDefinition = typeof(ClassificationTypeDefinition);

            var allFields = typeof(ClassificationTypeExports).GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            var exportedDefinitions = allFields
                .SelectMany(t => t.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
                .Where(f => f.CustomAttributes.Any(a => a.AttributeType == exportAttribute))
                .Where(f => f.GetCustomAttributes<ExportAttribute>().Any(a => a.ContractType == classificationTypeDefinition))
                .SelectMany(f => f.GetCustomAttributes<NameAttribute>())
                .Select(a => a.Name)
                .Distinct()
                .ToList();

            var allDefinitions = ClassificationType.All;
            var notExportedDefinitions = allDefinitions.Except(exportedDefinitions).ToList();
            notExportedDefinitions.Should().BeEmpty("All classification types should be exported");
        }
    }
}