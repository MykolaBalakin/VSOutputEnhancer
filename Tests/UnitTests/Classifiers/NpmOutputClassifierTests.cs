using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Tests.Stubs;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Classifiers
{
    [ExcludeFromCodeCoverage]
    public class NpmOutputClassifierTests : ClassifierTestsBase
    {
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

        [Fact]
        public void NpmResultSuccess()
        {
            const String npmExitCode0 = "====npm command completed with exit code 0====\r\n";
            var span = Utils.CreateSpan(npmExitCode0);

            var expectedResult = new[]
            {
                new ClassificationSpan(span, new ClassificationTypeStub(ClassificationType.NpmResultSucceeded))
            };

            Test(span, expectedResult);
        }

        [Fact]
        public void NpmResultFailed()
        {
            const String npmExitCode1 = "====npm command completed with exit code 1====\r\n";
            var span = Utils.CreateSpan(npmExitCode1);

            var expectedResult = new[]
            {
                new ClassificationSpan(span, new ClassificationTypeStub(ClassificationType.NpmResultFailed))
            };

            Test(span, expectedResult);
        }

        protected override IClassifier CreateClassifier()
        {
            return Utils.CreateNpmOutputClassifier();
        }
    }
}