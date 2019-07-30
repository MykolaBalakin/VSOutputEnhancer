using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.DebugException;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.DebugException
{
    [ExcludeFromCodeCoverage]
    public class NotParsed : TestCaseBase
    {
        public override String Input { get; } = "Some message\r\n";
        public override DebugExceptionData ExpectedResult { get; } = null;
    }
}