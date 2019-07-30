using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Balakin.VSOutputEnhancer.Tests.Base.Stubs;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using NSubstitute;
using Xunit;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers
{
    [ExcludeFromCodeCoverage]
    public class Test
    {
        public static IEnumerable<Object[]> TestCases { get; } = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => typeof(ITestCase).IsAssignableFrom(t))
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Select(t => new Object[] { t })
            .ToList();

        [Theory]
        [MemberData(nameof(TestCases))]
        public void ClassifyReturnsExpectedResult(Type testCaseType)
        {
            var testCase = (ITestCase)Activator.CreateInstance(testCaseType);

            var classifier = testCase.CreateClassifier();
            var dispatcher = Substitute.For<IDispatcher>();


            var snapshot = new TextSnapshotStub(testCase.Input);
            var span = new SnapshotSpan(snapshot, new Span(0, snapshot.Length));
            var actualResult = classifier.Classify(span, dispatcher).ToList();

            if (testCase.ExpectedResult == null)
            {
                actualResult.Should().BeEmpty();
            }
            else
            {
                actualResult.Should().HaveCount(1);
                var actualProcessedData = actualResult.Single();
                actualProcessedData.Should().BeEquivalentTo(testCase.ExpectedResult);
            }
        }
    }
}