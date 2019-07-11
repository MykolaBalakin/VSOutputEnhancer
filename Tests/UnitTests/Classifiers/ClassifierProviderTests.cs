using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Exports;
using FluentAssertions;
using Microsoft.VisualStudio.Text.Classification;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Classifiers
{
    [ExcludeFromCodeCoverage]
    public class ClassifierProviderTests
    {
        [Fact]
        public void BuildOutput()
        {
            var provider = CreateClassifierProvider();
            var textBuffer = Utils.CreateTextBuffer(ContentType.BuildOutput);
            var classifier = provider.GetClassifier(textBuffer);
            classifier.Should().NotBeNull();
        }

        [Fact]
        public void Debug()
        {
            var provider = CreateClassifierProvider();
            var textBuffer = Utils.CreateTextBuffer(ContentType.DebugOutput);
            var classifier = provider.GetClassifier(textBuffer);
            classifier.Should().NotBeNull();
        }

        [Fact]
        public void UnknownContentType()
        {
            var provider = CreateClassifierProvider();
            var textBuffer = Utils.CreateTextBuffer("UnknownContentType");
            var classifier = provider.GetClassifier(textBuffer);
            classifier.Should().BeNull();
        }

        IClassifierProvider CreateClassifierProvider()
        {
            var exportProvider = ExportProviderFactory.Create();
            return exportProvider.GetExport<IClassifierProvider>().Value;
        }
    }
}