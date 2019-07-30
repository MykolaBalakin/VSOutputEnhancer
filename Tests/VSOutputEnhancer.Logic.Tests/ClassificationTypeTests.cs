using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Balakin.VSOutputEnhancer.Logic.Tests
{
    [ExcludeFromCodeCoverage]
    public class ClassificationTypeTests
    {
        [Fact]
        public void AllIncludesAllClassificationTypes()
        {
            var classificationType = typeof(ClassificationType);
            var allTypes = classificationType.GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(f => (f.Attributes & FieldAttributes.Literal) == FieldAttributes.Literal)
                .Where(f => f.FieldType == typeof(String))
                .Select(f => f.GetValue(null));
            var typesThatAllNotContains = allTypes.Except(ClassificationType.All);
            typesThatAllNotContains.Should().BeEmpty("All classification types should be added to ClassificationType.All");
        }
    }
}