using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class ClassificationTypeTests
    {
        [Fact]
        public void All()
        {
            var classificationType = typeof(ClassificationType);
            var allTypes = classificationType.GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(f => (f.Attributes & FieldAttributes.Literal) == FieldAttributes.Literal)
                .Where(f => f.FieldType == typeof(String))
                .Select(f => f.GetValue(null))
                .ToList();
            var typesThatAllNotContains = allTypes.Except(ClassificationType.All).ToList();
            typesThatAllNotContains.Should().BeEmpty("ClassificationType.All not contains types: " + String.Join(", ", typesThatAllNotContains));
        }
    }
}