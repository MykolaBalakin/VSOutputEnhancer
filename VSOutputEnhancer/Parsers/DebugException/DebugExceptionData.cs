using System;

namespace Balakin.VSOutputEnhancer.Parsers.DebugException
{
    internal class DebugExceptionData : ParsedData
    {
        // This properties filled using reflection
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        public ParsedValue<String> Exception { get; private set; }
        public ParsedValue<String> Assembly { get; private set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}