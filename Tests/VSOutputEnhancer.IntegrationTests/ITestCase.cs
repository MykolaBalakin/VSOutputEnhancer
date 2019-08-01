using System;
using System.Collections.Generic;

namespace Balakin.VSOutputEnhancer.IntegrationTests
{
    public interface ITestCase
    {
        String ContentType { get; }
        IReadOnlyList<String> SourceText { get; }
        IReadOnlyList<ClassifiedText> ExpectedResult { get; }
    }
}