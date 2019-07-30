using System;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Logic.Classifiers;
using Balakin.VSOutputEnhancer.Logic.Classifiers.DebugException;

namespace Balakin.VSOutputEnhancer.Logic.Tests.Parsers.TestCases.DebugException
{
    [ExcludeFromCodeCoverage]
    public abstract class TestCaseBase : ITestCase<DebugExceptionData>
    {
        public IParser<DebugExceptionData> CreateParser() => new DebugExceptionParser();

        public abstract String Input { get; }
        public abstract DebugExceptionData ExpectedResult { get; }
    }
}