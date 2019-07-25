using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildFileRelatedMessage;
using Balakin.VSOutputEnhancer.Logic.Classifiers.NpmMessage;
using Balakin.VSOutputEnhancer.Tests.Base.Stubs;
using Microsoft.VisualStudio.Text.Classification;
using Xunit;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.NpmMessage
{
    [ExcludeFromCodeCoverage]
    public abstract class TestCaseBase : ITestCase
    {
        public ISpanClassifier CreateClassifier()
        {
            var parser = new NpmMessageParser();
            var classifier = new NpmMessageClassifier(parser);
            return classifier;
        }

        public abstract String Input { get; }
        public abstract ProcessedParsedData ExpectedResult { get; }
    }
}