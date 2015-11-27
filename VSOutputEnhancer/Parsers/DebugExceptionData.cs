using System;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    internal class DebugExceptionData : ParsedData {
        // This properties filled by reflection
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        public ParsedValue<String> Exception { get; private set; }
        public ParsedValue<String> Assembly { get; private set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}