using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Balakin.VSOutputEnhancer.Exports.Formats;
using FluentAssertions;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class ClassificationFormatTests
    {
        private static readonly IReadOnlyDictionary<Type, String> ClassificationTypeExpectedNames = new Dictionary<Type, String>
        {
            { typeof(BuildMessageErrorFormatDefinition), "Output enhancer: Build error message" },
            { typeof(BuildMessageWarningFormatDefinition), "Output enhancer: Build warning message" },
            { typeof(BuildResultFailedFormatDefinition), "Output enhancer: Build failed" },
            { typeof(BuildResultSucceededFormatDefinition), "Output enhancer: Build succeeded" },
            { typeof(PublishResultSucceededFormatDefinition), "Output enhancer: Publish succeeded" },
            { typeof(PublishResultFailedFormatDefinition), "Output enhancer: Publish failed" },
            { typeof(DebugExceptionFormatDefinition), "Output enhancer: Debug exception message" },
            { typeof(DebugTraceErrorFormatDefinition), "Output enhancer: Trace error message" },
            { typeof(DebugTraceWarningFormatDefinition), "Output enhancer: Trace warning message" },
            { typeof(NpmResultSucceededFormatDefinition), "Output enhancer: npm execution succeeded" },
            { typeof(NpmResultFailedFormatDefinition), "Output enhancer: npm execution failed" },
            { typeof(NpmMessageErrorFormatDefinition), "Output enhancer: npm error message" },
            { typeof(NpmMessageWarningFormatDefinition), "Output enhancer: npm warning message" },
            { typeof(BowerMessageErrorFormatDefinition), "Output enhancer: Bower error message" }
        };

        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void DisplayName(Type formatType)
        {
            var styleManager = Utils.CreateStyleManager();
            var expectedDisplayName = GetExpectedName(formatType);

            var format = (ClassificationFormatDefinition) Activator.CreateInstance(formatType, styleManager);
            format.DisplayName.Should().NotBeNullOrEmpty();
            format.DisplayName.Should().Be(expectedDisplayName);
        }

        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void TypeName(Type formatType)
        {
            var classificationType = formatType.GetCustomAttribute<ClassificationTypeAttribute>().ClassificationTypeNames;

            var expectedTypeName = classificationType + "FormatDefinition";

            formatType.Name.Should().Be(expectedTypeName);
        }

        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void NameAttribute(Type formatType)
        {
            var classificationType = formatType.GetCustomAttribute<ClassificationTypeAttribute>().ClassificationTypeNames;

            var expectedName = classificationType;

            var name = formatType.GetCustomAttribute<NameAttribute>().Name;
            name.Should().Be(expectedName);
        }

        public static IEnumerable<Object[]> CreateTestData()
        {
            return GetAllExportedFormats().Select(t => new Object[] { t });
        }

        private static IEnumerable<Type> GetAllExportedFormats()
        {
            var formatBaseType = typeof(ClassificationFormatDefinition);

            var assembly = typeof(StyleManager).Assembly;
            var formatTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(formatBaseType));
            formatTypes = formatTypes.Where(t => t.GetCustomAttribute<ExportAttribute>() != null);
            return formatTypes;
        }

        private String GetExpectedName(Type formatType)
        {
            if (ClassificationTypeExpectedNames.TryGetValue(formatType, out var name))
            {
                return name;
            }

            throw new ArgumentOutOfRangeException(nameof(formatType), formatType, "Unknown format type");
        }
    }
}