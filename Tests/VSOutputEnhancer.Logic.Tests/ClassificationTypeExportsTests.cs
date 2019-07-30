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
            var exportAttribute = typeof(ExportAttribute);
            var classificationTypeDefinition = typeof(ClassificationTypeDefinition);

            var allFields = typeof(ClassificationTypeExports).GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            var exportedDefinitions = allFields
                .Where(f => f.CustomAttributes.Any(a => a.AttributeType == exportAttribute))
                .Where(f => f.GetCustomAttributes<ExportAttribute>().Any(a => a.ContractType == classificationTypeDefinition))
                .SelectMany(f => f.GetCustomAttributes<NameAttribute>())
                .Select(a => a.Name)
                .Distinct();

            var allDefinitions = ClassificationType.All;
            var notExportedDefinitions = allDefinitions.Except(exportedDefinitions);
            notExportedDefinitions.Should().BeEmpty("All classification types should be exported");
        }
    }
}