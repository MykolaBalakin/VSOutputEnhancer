using System;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Classifiers
{
    public interface ITestCase
    {
        ISpanClassifier CreateClassifier();
        String Input { get; }
        ProcessedParsedData ExpectedResult { get; }
    }
}