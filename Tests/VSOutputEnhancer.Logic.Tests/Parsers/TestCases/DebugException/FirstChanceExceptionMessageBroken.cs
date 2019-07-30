using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers.DebugException;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.DebugException
{
    [ExcludeFromCodeCoverage]
    public class FirstChanceExceptionMessageBroken : TestCaseBase
    {
        public override String Input { get; } = "A first chance exception of type 'blablabla\r\n";
        public override DebugExceptionData ExpectedResult { get; } = null;
    }
}