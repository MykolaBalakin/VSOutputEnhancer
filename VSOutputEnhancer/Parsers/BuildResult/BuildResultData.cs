using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Parsers.BuildResult {
    internal class BuildResultData : ParsedData {
        // This properties filled using reflection
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        public ParsedValue<Int32> Succeeded { get; private set; }
        public ParsedValue<Int32> Failed { get; private set; }
        public ParsedValue<Int32> UpToDate { get; private set; }
        public ParsedValue<Int32> Skipped { get; private set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}