using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Balakin.VSOutputEnhancer.Logic;
using Balakin.VSOutputEnhancer.Logic.Tests;
using Balakin.VSOutputEnhancer.Tests.Base;
using Balakin.VSOutputEnhancer.Tests.Base.Stubs;
using Balakin.VSOutputEnhancer.Tests.Stubs;
using FluentAssertions;
using Microsoft.VisualStudio.Text.Classification;
using Xunit;
using Xunit.Abstractions;

namespace Balakin.VSOutputEnhancer.Tests.PerfomanceTests
{
    [ExcludeFromCodeCoverage]
    public class ClassificationPerformance : IClassFixture<ServiceProviderFixture>
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly ServiceProviderFixture serviceProvider;

        public ClassificationPerformance(ITestOutputHelper testOutputHelper, ServiceProviderFixture serviceProvider)
        {
            this.testOutputHelper = testOutputHelper;
            this.serviceProvider = serviceProvider;
        }

        [Fact]
        public void EntityFramework()
        {
            // ~ 570 000 lines of log
            // Small count of classified text

            var content = Utils.ReadLogFile("Resources\\EntityFrameworkBuild.log");
            var spans = content.Select(StringExtensions.ToSnapshotSpan).ToList();
            var classifier = CreateBuildOutputClassifier();
            var totalCount = 0;
            var sw = Stopwatch.StartNew();
            foreach (var span in spans)
            {
                totalCount += classifier.GetClassificationSpans(span).Count;
            }
            sw.Stop();
            WriteMessage("Elapsed: " + sw.Elapsed);
            sw.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(5));
        }

        [Fact]
        public void LotOfClassifiedMessages()
        {
            // 100 000 warning/error messages

            var content = Utils.ReadLogFile("Resources\\RandomBuildOutput.log");
            var spans = content.Select(StringExtensions.ToSnapshotSpan).ToList();
            var classifier = CreateBuildOutputClassifier();
            var totalCount = 0;
            var sw = Stopwatch.StartNew();
            foreach (var span in spans)
            {
                totalCount += classifier.GetClassificationSpans(span).Count;
            }
            sw.Stop();
            WriteMessage("Elapsed: " + sw.Elapsed);
            sw.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(5));
        }

        private IClassifier CreateBuildOutputClassifier()
        {
            var classifierProvider = serviceProvider.GetService<IClassifierProvider>();
            var classifier = classifierProvider.GetClassifier(new TextBufferStub(ContentType.BuildOutput));
            return classifier;
        }

        private void WriteMessage(String message)
        {
            Console.WriteLine(message);
            testOutputHelper.WriteLine(message);
        }
    }
}