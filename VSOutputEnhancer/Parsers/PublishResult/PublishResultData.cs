using System;

namespace Balakin.VSOutputEnhancer.Parsers.PublishResult
{
    internal class PublishResultData : ParsedData
    {
        // This properties filled using reflection
        // ReSharper disable UnusedAutoPropertyAccessor.Local
        public ParsedValue<Int32> Succeeded { get; private set; }
        public ParsedValue<Int32> Failed { get; private set; }
        public ParsedValue<Int32> Skipped { get; private set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}