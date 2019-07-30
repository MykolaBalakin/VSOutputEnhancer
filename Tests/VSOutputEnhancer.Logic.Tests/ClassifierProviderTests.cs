using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Tests.Base.Stubs;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Xunit;

namespace Balakin.VSOutputEnhancer.Logic.Tests
{
    [ExcludeFromCodeCoverage]
    public class ClassifierProviderTests : IClassFixture<ServiceProviderFixture>
    {
        private readonly ServiceProviderFixture serviceProvider;

        public ClassifierProviderTests(ServiceProviderFixture serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        [Theory]
        [InlineData(ContentType.BuildOutput)]
        [InlineData(ContentType.DebugOutput)]
        public void GetClassifierReturnsNotNull(String contentType)
        {
            var provider = CreateClassifierProvider();
            var textBuffer = CreateTextBuffer(contentType);
            var classifier = provider.GetClassifier(textBuffer);
            classifier.Should().NotBeNull();
        }

        [Theory]
        [InlineData("UnknownContentType")]
        public void GetClassifierReturnsNull(String contentType)
        {
            var provider = CreateClassifierProvider();
            var textBuffer = CreateTextBuffer(contentType);
            var classifier = provider.GetClassifier(textBuffer);
            classifier.Should().BeNull();
        }

        private IClassifierProvider CreateClassifierProvider()
        {
            return serviceProvider.GetService<IClassifierProvider>();
        }

        private ITextBuffer CreateTextBuffer(String contentType)
        {
            return new TextBufferStub(contentType);
        }
    }
}