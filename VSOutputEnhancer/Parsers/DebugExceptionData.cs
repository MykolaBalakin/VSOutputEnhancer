using System;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers {
    internal class DebugExceptionData : ParsedData {
        public DebugExceptionData(String exception, String assembly, Span exceptionSpan, Span assemblySpan) {
            Exception = exception;
            Assembly = assembly;
            ExceptionSpan = exceptionSpan;
            AssemblySpan = assemblySpan;
        }

        public String Exception { get; }
        public String Assembly { get; }

        public Span ExceptionSpan { get; }
        public Span AssemblySpan { get; }
    }
}