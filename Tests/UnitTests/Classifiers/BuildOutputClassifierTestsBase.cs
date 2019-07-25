using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic;
using Balakin.VSOutputEnhancer.Tests.Base.Stubs;
using Balakin.VSOutputEnhancer.Tests.Stubs;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Classifiers
{
    [ExcludeFromCodeCoverage]
    public abstract class BuildOutputClassifierTestsBase : ClassifierTestsBase
    {
        [Fact]
        public void BuildFailed()
        {
            const String buildCompleteMessage = "========== Build: 5 succeeded, 1 failed, 3 up-to-date, 2 skipped ==========\r\n";
            var span = Utils.CreateSpan(buildCompleteMessage);

            var expectedResult = new[]
            {
                new ClassificationSpan(span, new ClassificationTypeStub(ClassificationType.BuildResultFailed))
            };

            Test(span, expectedResult);
        }

        [Fact]
        public void BuildSucceeded()
        {
            const String buildCompleteMessage = "========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========\r\n";
            var span = Utils.CreateSpan(buildCompleteMessage);

            var expectedResult = new[]
            {
                new ClassificationSpan(span, new ClassificationTypeStub(ClassificationType.BuildResultSucceeded))
            };

            Test(span, expectedResult);
        }

        [Fact]
        public void BuildSucceededOrUpToDate()
        {
            const String buildCompleteMessage = "========== Build: 3 succeeded or up-to-date, 0 failed, 0 skipped ==========\r\n";
            var span = Utils.CreateSpan(buildCompleteMessage);

            var expectedResult = new[]
            {
                new ClassificationSpan(span, new ClassificationTypeStub(ClassificationType.BuildResultSucceeded))
            };

            Test(span, expectedResult);
        }

        [Fact]
        public void PublishFailed()
        {
            const String publishCompleteMessage = "========== Publish: 0 succeeded, 1 failed, 0 skipped ==========\r\n";
            var span = Utils.CreateSpan(publishCompleteMessage);

            var expectedResult = new[]
            {
                new ClassificationSpan(span, new ClassificationTypeStub(ClassificationType.PublishResultFailed))
            };

            Test(span, expectedResult);
        }

        [Fact]
        public void PublishSucceeded()
        {
            const String publishCompleteMessage = "========== Publish: 1 succeeded, 0 failed, 0 skipped ==========\r\n";
            var span = Utils.CreateSpan(publishCompleteMessage);

            var expectedResult = new[]
            {
                new ClassificationSpan(span, new ClassificationTypeStub(ClassificationType.PublishResultSucceeded))
            };

            Test(span, expectedResult);
        }

        [Fact]
        public void CompilationWarning()
        {
            const String compilationWarningMessage = "1>C:\\Sources\\GitHub\\VSOutputEnhancer\\VSOutputEnhancer\\ClassificationType.cs(29,53,29,83): warning CS0169: The field 'ClassificationType.BuildResultSucceededDefinition' is never used\r\n";
            var span = Utils.CreateSpan(compilationWarningMessage);

            var expectedResult = new[]
            {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, 90, 91), new ClassificationTypeStub(ClassificationType.BuildMessageWarning))
            };

            Test(span, expectedResult);
        }

        [Fact]
        public void CompilationError()
        {
            const String compilationErrorMessage = "1>C:\\Sources\\GitHub\\VSOutputEnhancer\\UnitTests\\BuildOutputClassifierTests.cs(91,64,91,65): error CS1026: ) expected\r\n";
            var span = Utils.CreateSpan(compilationErrorMessage);

            var expectedResult = new[]
            {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, 91, 24), new ClassificationTypeStub(ClassificationType.BuildMessageError))
            };

            Test(span, expectedResult);
        }

        [Fact]
        public void BowerError()
        {
            const String errorMessage = "C:\\Program Files (x86)\\MSBuild\\Microsoft\\VisualStudio\\v14.0\\Web\\Microsoft.DNX.Publishing.targets(152,5): Error : bower bootstrap1#3.3.5       ENOTFOUND Package bootstrap1 not found\r\n";
            var span = Utils.CreateSpan(errorMessage);

            var expectedResult = new[]
            {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, 105, 75), new ClassificationTypeStub(ClassificationType.BuildMessageError))
            };

            Test(span, expectedResult);
        }

        [Fact]
        public void NpmWarning()
        {
            const String npmWarnMessage = "npm WARN package.json ASP.NET@0.0.0 No description\r\n";
            var span = Utils.CreateSpan(npmWarnMessage);

            var expectedResult = new[]
            {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, 9, 41), new ClassificationTypeStub(ClassificationType.NpmMessageWarning))
            };

            Test(span, expectedResult);
        }

        [Fact]
        public void NpmError()
        {
            const String npmWarnMessage = "npm ERR! 404 Not Found\r\n";
            var span = Utils.CreateSpan(npmWarnMessage);

            var expectedResult = new[]
            {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, 9, 13), new ClassificationTypeStub(ClassificationType.NpmMessageError))
            };

            Test(span, expectedResult);
        }
    }
}