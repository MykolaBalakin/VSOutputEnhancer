using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Classifiers
{
    [ExcludeFromCodeCoverage]
    public class BuildOutputClassifierTests : BuildOutputClassifierTestsBase
    {
        protected override String GetContentType() => ContentType.BuildOutput;
    }
}