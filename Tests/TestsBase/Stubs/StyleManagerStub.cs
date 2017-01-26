using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Balakin.VSOutputEnhancer.Tests.Stubs
{
    [ExcludeFromCodeCoverage]
    public class StyleManagerStub : IStyleManager
    {
        public FormatDefinitionStyle GetStyleForClassificationType(String classificationType)
        {
            return new FormatDefinitionStyle();
        }
    }
}