using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ClassificationTypeTests
    {
        [TestMethod]
        public void All()
        {
            var classificationType = typeof(ClassificationType);
            var allTypes = classificationType.GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(f => (f.Attributes & FieldAttributes.Literal) == FieldAttributes.Literal)
                .Where(f => f.FieldType == typeof(String))
                .Select(f => f.GetValue(null))
                .ToList();
            var typesThatAllNotContains = allTypes.Except(ClassificationType.All).ToList();
            if (typesThatAllNotContains.Any())
            {
                Assert.Fail("ClassificationType.All not contains types: " + String.Join(", ", typesThatAllNotContains));
            }
        }
    }
}