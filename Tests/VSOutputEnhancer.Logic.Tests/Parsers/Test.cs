using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Tests.Base;
using FluentAssertions;
using Xunit;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers
{
    [ExcludeFromCodeCoverage]
    public class Test
    {
        public static IEnumerable<Object[]> TestCases { get; } = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ITestCase<>)))
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Select(t => new Object[] { t })
            .ToList();

        [Theory]
        [MemberData(nameof(TestCases))]
        public void TryParseReturnsExpectedResult(Type testCaseType)
        {
            var testCaseInterface = testCaseType.GetInterfaces().Single(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ITestCase<>));
            var parsedDataType = testCaseInterface.GetGenericArguments().Single();

            var runTestMethod = GetType()
                .GetMethod(nameof(RunTest), BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(parsedDataType, testCaseType);

            runTestMethod.Invoke(this, new Object[0]);
        }

        private void RunTest<TParsedData, TTestCase>()
            where TTestCase : ITestCase<TParsedData>, new()
            where TParsedData : ParsedData
        {
            var testCase = new TTestCase();
            var parser = testCase.CreateParser();
            var span = testCase.Input.ToSnapshotSpan();

            var parsed = parser.TryParse(span, out var actualResult);

            if (testCase.ExpectedResult == null)
            {
                parsed.Should().BeFalse();
            }
            else
            {
                parsed.Should().BeTrue();
            }

            actualResult.Should().BeEquivalentTo(testCase.ExpectedResult);
        }
    }
}