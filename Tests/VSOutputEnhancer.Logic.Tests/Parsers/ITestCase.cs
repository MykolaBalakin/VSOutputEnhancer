using System;
using Balakin.VSOutputEnhancer.Logic.Classifiers;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers
{
    public interface ITestCase<TParsedData> where TParsedData : ParsedData
    {
        IParser<TParsedData> CreateParser();
        String Input { get; }
        TParsedData ExpectedResult { get; }
    }
}