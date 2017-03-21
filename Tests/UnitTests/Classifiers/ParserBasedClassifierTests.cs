using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Parsers.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Classifiers
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ParserBasedClassifierTests
    {
        [TestMethod]
        public void NotParsed()
        {
            var parser = new StubIParser<ParsedData>();
            parser.TryParseSnapshotSpanT0Out = delegate(SnapshotSpan s, out ParsedData r)
            {
                r = new ParsedData();
                return false;
            };
            var processor = new StubIParsedDataProcessor<ParsedData>();
            processor.ProcessDataSnapshotSpanT0 = (s, d) => new List<ProcessedParsedData>
            {
                new ProcessedParsedData(s, "TestClassification")
            };

            var classifier = Utils.CreateParserBasedClassifier(parser, processor);

            var span = Utils.CreateSpan("");
            var classificationSpans = classifier.GetClassificationSpans(span);
            Assert.AreEqual(0, classificationSpans.Count);
        }

        [TestMethod]
        public void Parsed()
        {
            var parser = new StubIParser<ParsedData>();
            parser.TryParseSnapshotSpanT0Out = delegate(SnapshotSpan s, out ParsedData r)
            {
                r = new ParsedData();
                return true;
            };
            var processor = new StubIParsedDataProcessor<ParsedData>();
            processor.ProcessDataSnapshotSpanT0 = (s, d) => new List<ProcessedParsedData>
            {
                new ProcessedParsedData(s, "TestClassification"),
                new ProcessedParsedData(s, "TestClassification2"),
            };

            var classifier = Utils.CreateParserBasedClassifier(parser, processor);

            var span = Utils.CreateSpan("");
            var classificationSpans = classifier.GetClassificationSpans(span);
            Assert.AreEqual(2, classificationSpans.Count);
            Assert.AreEqual(1, classificationSpans.Count(s => s.ClassificationType.IsOfType("TestClassification")));
            Assert.AreEqual(1, classificationSpans.Count(s => s.ClassificationType.IsOfType("TestClassification2")));
        }
    }
}