using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Tests.Stubs;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Classifiers
{
    [ExcludeFromCodeCoverage]
    public class DebugClassifierTests : ClassifierTestsBase
    {
        [Fact]
        public void TraceError()
        {
            const String traceErrorMessage = "VSOutputEnhancerDemo.vshost.exe Error: 0 : Trace error message\r\n";

            var span = Utils.CreateSpan(traceErrorMessage);

            var expectedResult = new[]
            {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, 32, 30), new ClassificationTypeStub(ClassificationType.DebugTraceError))
            };

            Test(span, expectedResult);
        }

        [Fact]
        public void TraceError2()
        {
            const String fullText = "Some other text\r\nVSOutputEnhancerDemo.vshost.exe Error: 0 : Trace error message\r\n";
            const String traceErrorMessage = "VSOutputEnhancerDemo.vshost.exe Error: 0 : Trace error message\r\n";
            const String highlightedMessage = "Error: 0 : Trace error message";

            var span = Utils.CreateSpan(fullText);
            span = new SnapshotSpan(span.Snapshot, fullText.Length - traceErrorMessage.Length, traceErrorMessage.Length);

            var expectedSnapshot = new SnapshotSpan(span.Snapshot, 49, 30);
            var expectedResult = new[]
            {
                new ClassificationSpan(expectedSnapshot, new ClassificationTypeStub(ClassificationType.DebugTraceError))
            };

            expectedSnapshot.GetText().Should().Be(highlightedMessage);

            Test(span, expectedResult);
        }

        [Fact]
        public void TraceInformation()
        {
            const String traceErrorMessage = "VSOutputEnhancerDemo.vshost.exe Information: 0 : Trace information message\r\n";

            var span = Utils.CreateSpan(traceErrorMessage);

            var expectedResult = new[]
            {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, 32, 42), new ClassificationTypeStub(ClassificationType.DebugTraceInformation))
            };

            Test(span, expectedResult);
        }

        [Fact]
        public void TraceWarning()
        {
            const String traceErrorMessage = "VSOutputEnhancerDemo.vshost.exe Warning: 0 : Trace warning message\r\n";

            var span = Utils.CreateSpan(traceErrorMessage);

            var expectedResult = new[]
            {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, 32, 34), new ClassificationTypeStub(ClassificationType.DebugTraceWarning))
            };

            Test(span, expectedResult);
        }

        [Fact]
        public void TraceException()
        {
            const String traceErrorMessage = "Exception thrown: 'System.Exception' in VSOutputEnhancerDemo.exe\r\n";

            var span = Utils.CreateSpan(traceErrorMessage);

            var expectedResult = new[]
            {
                new ClassificationSpan(span, new ClassificationTypeStub(ClassificationType.DebugException))
            };

            Test(span, expectedResult);
        }

        protected override String GetContentType() => ContentType.DebugOutput;
    }
}