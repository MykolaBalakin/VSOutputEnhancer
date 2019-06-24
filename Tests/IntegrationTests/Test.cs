using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Balakin.VSOutputEnhancer.Tests.IntegrationTests.TestCases;
using Balakin.VSOutputEnhancer.Tests.Stubs;
using FluentAssertions;
using Microsoft.VisualStudio.Text.Classification;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    public class Test
    {
        [Theory]
        [InlineData(typeof(Smoke))]
        public void ClassificationReturnsExpectedResult(Type testCaseType)
        {
            var testCase = (ITestCase)Activator.CreateInstance(testCaseType);

            var classifierProvider = CreateClassifierProvider();
            var classifier = classifierProvider.GetClassifier(new TextBufferStub(testCase.ContentType));

            var actualResult = new List<ClassifiedText>();
            foreach (var line in testCase.SourceText)
            {
                var classificationSpans = classifier.GetClassificationSpans(Utils.CreateSpan(line));
                foreach (var classificationSpan in classificationSpans)
                {
                    var classifiedText = classificationSpan.Span.GetText();
                    var classificationType = classificationSpan.ClassificationType.Classification;
                    actualResult.Add(new ClassifiedText(classificationType, classifiedText));
                }
            }

            actualResult.ShouldAllBeEquivalentTo(testCase.ExpectedResult);
        }

        private IClassifierProvider CreateClassifierProvider()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ClassificationType).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            var container = new CompositionContainer(catalog);
            var classifierProvider = container.GetExport<IClassifierProvider>();
            return classifierProvider.Value;
        }
    }
}