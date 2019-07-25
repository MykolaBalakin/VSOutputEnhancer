using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.DebugException;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers.TestCases.DebugException
{
    [ExcludeFromCodeCoverage]
    public abstract class TestCaseBase : ITestCase
    {
        public ISpanClassifier CreateClassifier()
        {
            var parser = new DebugExceptionParser();
            var classifier = new DebugExceptionClassifier(parser);
            return classifier;
        }

        public abstract String Input { get; }
        public abstract ProcessedParsedData ExpectedResult { get; }
    }
}