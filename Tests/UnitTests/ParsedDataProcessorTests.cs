using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Parsers.BuildFileRelatedMessage;
using Balakin.VSOutputEnhancer.Parsers.DebugTraceMessage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ParsedDataProcessorTests
    {
        [TestMethod]
        public void EmptyEnumerableOnNullData()
        {
            var emptySpan = Utils.CreateSpan("");

            var dataProcessorInterface = typeof(IParsedDataProcessor<>);
            var assembly = typeof(ClassificationType).Assembly;
            var dataProcessors = assembly.GetTypes()
                .Where(t => !t.IsAbstract)
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == dataProcessorInterface))
                .ToList();
            foreach (var dataProcessorType in dataProcessors)
            {
                var dataProcessor = Activator.CreateInstance(dataProcessorType);
                var processDataMethod = dataProcessorType.GetMethod("ProcessData");
                var result = (IEnumerable<ProcessedParsedData>) processDataMethod.Invoke(dataProcessor, new Object[] { emptySpan, null });
                Assert.IsFalse(result.Any(), dataProcessorType.Name);
            }
        }

        [TestMethod]
        public void NotClassifiedTraceEventType()
        {
            var span = Utils.CreateSpan("-1");
            var match = Regex.Match(span.GetText(), "(?<Type>.*)");

            var data = ParsedData.Create<DebugTraceMessageData>(match, span.Span);
            var dataProcessor = new DebugTraceMessageDataProcessor();
            var result = dataProcessor.ProcessData(span, data);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void NotClassifiedBuildMessageType()
        {
            var span = Utils.CreateSpan("-1");
            var match = Regex.Match(span.GetText(), "(?<Type>.*)");

            var data = ParsedData.Create<BuildFileRelatedMessageData>(match, span.Span);
            var dataProcessor = new BuildFileRelatedMessageDataProcessor();
            var result = dataProcessor.ProcessData(span, data);

            Assert.IsFalse(result.Any());
        }
    }
}