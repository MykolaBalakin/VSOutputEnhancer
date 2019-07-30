using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.NpmResult;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.NpmResult
{
    [ExcludeFromCodeCoverage]
    public class NotParsed : TestCaseBase
    {
        public override String Input { get; } = "Some message\r\n";
        public override NpmResultData ExpectedResult { get; } = null;
    }
}