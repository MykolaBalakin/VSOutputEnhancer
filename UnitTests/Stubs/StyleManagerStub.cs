using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balakin.VSOutputEnhancer.UnitTests.Stubs {
    [ExcludeFromCodeCoverage]
    internal class StyleManagerStub : IStyleManager {
        public FormatDefinitionStyle GetStyleForClassificationType(String classificationType) {
            return new FormatDefinitionStyle();
        }
    }
}
