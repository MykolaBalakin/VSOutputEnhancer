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
        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void DisplayName(Type formatType)
        {
            var styleManager = Utils.CreateStyleManager();
            var expectedDisplayName = GetCorrectName(formatType);

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

        public static IEnumerable<object[]> CreateTestData()
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

        private String GetCorrectName(Type formatType)
        {
            if (formatType == typeof(BuildMessageErrorFormatDefinition))
            {
                return "Output enhancer: Build error message";
            }
            if (formatType == typeof(BuildMessageWarningFormatDefinition))
            {
                return "Output enhancer: Build warning message";
            }
            if (formatType == typeof(BuildResultFailedFormatDefinition))
            {
                return "Output enhancer: Build failed";
            }
            if (formatType == typeof(BuildResultSucceededFormatDefinition))
            {
                return "Output enhancer: Build succeeded";
            }
            if (formatType == typeof(PublishResultSucceededFormatDefinition))
            {
                return "Output enhancer: Publish succeeded";
            }
            if (formatType == typeof(PublishResultFailedFormatDefinition))
            {
                return "Output enhancer: Publish failed";
            }
            if (formatType == typeof(DebugExceptionFormatDefinition))
            {
                return "Output enhancer: Debug exception message";
            }
            if (formatType == typeof(DebugTraceErrorFormatDefinition))
            {
                return "Output enhancer: Trace error message";
            }
            if (formatType == typeof(DebugTraceWarningFormatDefinition))
            {
                return "Output enhancer: Trace warning message";
            }

            if (formatType == typeof(NpmResultSucceededFormatDefinition))
            {
                return "Output enhancer: npm execution succeeded";
            }
            if (formatType == typeof(NpmResultFailedFormatDefinition))
            {
                return "Output enhancer: npm execution failed";
            }
            if (formatType == typeof(NpmMessageErrorFormatDefinition))
            {
                return "Output enhancer: npm error message";
            }
            if (formatType == typeof(NpmMessageWarningFormatDefinition))
            {
                return "Output enhancer: npm warning message";
            }
            if (formatType == typeof(BowerMessageErrorFormatDefinition))
            {
                return "Output enhancer: Bower error message";
            }

            throw new ArgumentOutOfRangeException(nameof(formatType), formatType, "Unknown format type");
        }
    }
}