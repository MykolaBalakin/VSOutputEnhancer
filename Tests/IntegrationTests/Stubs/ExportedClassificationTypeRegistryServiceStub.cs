using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Tests.Stubs;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Tests.IntegrationTests.Stubs
{
    [Export(typeof(IClassificationTypeRegistryService))]
    [ExcludeFromCodeCoverage]
    public class ExportedClassificationTypeRegistryServiceStub : ClassificationTypeRegistryServiceStub
    {
    }
}