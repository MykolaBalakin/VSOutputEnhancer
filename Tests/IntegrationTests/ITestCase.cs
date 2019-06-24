using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Balakin.VSOutputEnhancer.Tests.IntegrationTests
{
    public interface ITestCase
    {
        String ContentType { get; }
        IReadOnlyList<String> SourceText { get; }
        IReadOnlyList<ClassifiedText> ExpectedResult { get; }
    }
}