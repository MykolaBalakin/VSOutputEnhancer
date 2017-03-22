using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Classifiers
{
    [ExcludeFromCodeCoverage]
    public class BuildOrderOutputClassifierTests : BuildOutputClassifierTestsBase
    {
        protected override IClassifier CreateClassifier()
        {
            return Utils.CreateBuildOrderOutputClassifier();
        }
    }
}