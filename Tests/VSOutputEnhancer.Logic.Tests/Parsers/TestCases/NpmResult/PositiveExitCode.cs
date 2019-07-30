using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.NpmResult;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.NpmResult
{
    [ExcludeFromCodeCoverage]
    public class PositiveExitCode : TestCaseBase
    {
        public override String Input { get; } = "====npm command completed with exit code 1823====\r\n";
        public override NpmResultData ExpectedResult { get; } = new NpmResultData(new ParsedValue<Int32>(1823, new Span(41, 4)));
    }
}