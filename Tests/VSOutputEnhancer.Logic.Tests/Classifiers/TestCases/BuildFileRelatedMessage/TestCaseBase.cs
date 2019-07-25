using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.BuildFileRelatedMessage;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.BuildFileRelatedMessage
{
    [ExcludeFromCodeCoverage]
    public abstract class TestCaseBase : ITestCase
    {
        public ISpanClassifier CreateClassifier()
        {
            var parser = new BuildFileRelatedMessageParser();
            var classifier = new BuildFileRelatedMessageClassifier(parser);
            return classifier;
        }

        public abstract String Input { get; }
        public abstract ProcessedParsedData ExpectedResult { get; }
    }
}