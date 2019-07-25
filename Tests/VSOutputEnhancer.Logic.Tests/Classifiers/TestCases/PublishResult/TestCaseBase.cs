using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.PublishResult;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.PublishResult
{
    [ExcludeFromCodeCoverage]
    public abstract class TestCaseBase : ITestCase
    {
        public ISpanClassifier CreateClassifier()
        {
            var parser = new PublishResultParser();
            var classifier = new PublishResultClassifier(parser);
            return classifier;
        }

        public abstract String Input { get; }
        public abstract ProcessedParsedData ExpectedResult { get; }
    }
}