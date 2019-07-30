using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.NpmMessage;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.NpmMessage
{
    [ExcludeFromCodeCoverage]
    public abstract class TestCaseBase : ITestCase<NpmMessageData>
    {
        public IParser<NpmMessageData> CreateParser() => new NpmMessageParser();

        public abstract String Input { get; }
        public abstract NpmMessageData ExpectedResult { get; }
    }
}