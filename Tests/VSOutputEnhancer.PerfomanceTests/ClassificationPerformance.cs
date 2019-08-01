using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.Logic;
using Balakin.VSOutputEnhancer.Logic.Tests;
using Balakin.VSOutputEnhancer.Tests.Base;
using Balakin.VSOutputEnhancer.Tests.Base.Stubs;
using FluentAssertions;
using Microsoft.VisualStudio.Text.Classification;
using Xunit;
using Xunit.Abstractions;

namespace Balakin.VSOutputEnhancer.PerfomanceTests
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
        public async Task EntityFramework()
        {
            // ~ 570 000 lines of log
            // Small amount of classified text

            var content = await ReadTestData("EntityFrameworkBuild");
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
        public async Task LotOfClassifiedMessages()
        {
            // 100 000 warning/error messages

            var content = await ReadTestData("RandomBuildOutput");
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

        private async Task<IReadOnlyList<String>> ReadTestData(String name)
        {
            var result = new List<String>();

            var zipFilePath = Path.Combine("Resources", name + ".zip");
            using (var zipFile = ZipFile.OpenRead(zipFilePath))
            {
                var testData = zipFile.Entries.Single();

                using (var stream = testData.Open())
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        result.Add(line + "\r\n");
                    }
                }
            }

            return result;
        }
    }
}