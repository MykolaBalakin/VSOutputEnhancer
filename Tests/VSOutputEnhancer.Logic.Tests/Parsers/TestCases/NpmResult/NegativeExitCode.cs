using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.NpmResult;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.NpmResult
{
    [ExcludeFromCodeCoverage]
    public class NegativeExitCode : TestCaseBase
    {
        public override String Input { get; } = "====npm command completed with exit code -8====\r\n";
        public override NpmResultData ExpectedResult { get; } = new NpmResultData(new ParsedValue<Int32>(-8, new Span(41, 2)));
    }
}