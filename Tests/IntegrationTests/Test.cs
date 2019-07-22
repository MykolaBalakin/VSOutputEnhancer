using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Balakin.VSOutputEnhancer.Logic;
using Balakin.VSOutputEnhancer.Tests.Base.Stubs;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    public class Test
    {
        [Theory]
        [MemberData(nameof(EnumerateTestCases))]
        public void ClassificationReturnsExpectedResult(Type testCaseType)
        {
            var testCase = (ITestCase)Activator.CreateInstance(testCaseType);

            var classifierProvider = CreateClassifierProvider();
            var classifier = classifierProvider.GetClassifier(new TextBufferStub(testCase.ContentType));

            var actualResult = new List<ClassifiedText>();

            var spansToReclassify = new List<SnapshotSpan>();
            classifier.ClassificationChanged += (sender, args) => spansToReclassify.Add(args.ChangeSpan);

            foreach (var line in testCase.SourceText)
            {
                var span = Utils.CreateSpan(line);
                DoClassification(classifier, span, actualResult);
                foreach (var spanToReclassify in spansToReclassify)
                {
                    DoClassification(classifier, spanToReclassify, actualResult);
                }
                spansToReclassify.Clear();
            }

            actualResult.Should().BeEquivalentTo(testCase.ExpectedResult);
        }

        public static IEnumerable<object[]> EnumerateTestCases()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var testCaseInterface = typeof(ITestCase);
            var testCases = assembly.GetTypes().Where(t => !t.IsInterface && !t.IsAbstract && testCaseInterface.IsAssignableFrom(t));
            return testCases.Select(t => new[] { t });
        }

        private IClassifierProvider CreateClassifierProvider()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ClassificationType).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ClassificationTypeRegistryServiceStub).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            var container = new CompositionContainer(catalog);
            var classifierProvider = container.GetExport<IClassifierProvider>();
            return classifierProvider.Value;
        }

        private void DoClassification(IClassifier classifier, SnapshotSpan span, IList<ClassifiedText> result)
        {
            var classificationSpans = classifier.GetClassificationSpans(span);
            foreach (var classificationSpan in classificationSpans)
            {
                var classifiedText = classificationSpan.Span.GetText();
                var classificationType = classificationSpan.ClassificationType.Classification;
                result.Add(new ClassifiedText(classificationType, classifiedText));
            }
        }
    }
}