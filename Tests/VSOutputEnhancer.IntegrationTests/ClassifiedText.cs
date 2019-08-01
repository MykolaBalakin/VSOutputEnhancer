using System;
using System.Diagnostics.CodeAnalysis;

namespace Balakin.VSOutputEnhancer.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    public struct ClassifiedText
    {
        public String ClassificationType { get; }
        public String Text { get; }

        public ClassifiedText(String classificationType, String text)
        {
            ClassificationType = classificationType;
            Text = text;
        }
    }
}