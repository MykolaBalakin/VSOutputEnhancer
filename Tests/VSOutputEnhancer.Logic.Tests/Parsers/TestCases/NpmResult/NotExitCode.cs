using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.NpmResult;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.NpmResult
{
    [ExcludeFromCodeCoverage]
    public class NotExitCode : TestCaseBase
    {
        public override String Input { get; } = "====npm command completed with exit code asdf====\r\n";
        public override NpmResultData ExpectedResult { get; } = null;
    }
}