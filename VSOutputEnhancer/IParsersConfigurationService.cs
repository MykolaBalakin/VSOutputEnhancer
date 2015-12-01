using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer {
    internal interface IParsersConfigurationService {
        IEnumerable<ParserConfiguration> GetParsers(IContentType contentType);
    }
}