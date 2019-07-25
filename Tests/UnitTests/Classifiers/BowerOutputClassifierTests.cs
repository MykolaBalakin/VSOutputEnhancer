using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
    public class BowerOutputClassifierTests : ClassifierTestsBase
    {
        protected override String GetContentType() => ContentType.Output;

        [Fact]
        public void PackageNotFound()
        {
            const String notFoundError = "bower bootstrap1#3.3.5       ENOTFOUND Package bootstrap1 not found\r\n";
            var span = Utils.CreateSpan(notFoundError);

            var expectedResult = new[]
            {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, 39, 28), new ClassificationTypeStub(ClassificationType.BowerMessageError))
            };

            Test(span, expectedResult);
        }
    }
}