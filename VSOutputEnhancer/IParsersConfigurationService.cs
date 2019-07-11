using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer
{
    [Obsolete]
    public interface IParsersConfigurationService
    {
        IReadOnlyCollection<ParserConfiguration> GetParsers(IContentType contentType);
    }
}