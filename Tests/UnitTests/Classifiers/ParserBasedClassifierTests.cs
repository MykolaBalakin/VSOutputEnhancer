using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Parsers.Fakes;
using Balakin.VSOutputEnhancer.Tests.Stubs;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Classifiers
{
    [ExcludeFromCodeCoverage]
    public class ParserBasedClassifierTests
    {
        [Fact]
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
            var actualResult = classifier.GetClassificationSpans(span);
            actualResult.Should().BeEmpty();
        }

        [Fact]
        public void Parsed()
        {
            var span = Utils.CreateSpan("");
            var expectedResult = new[]
            {
                new ClassificationSpan(span, new ClassificationTypeStub("TestClassification")),
                new ClassificationSpan(span, new ClassificationTypeStub("TestClassification2"))
            };

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

            var actualResult = classifier.GetClassificationSpans(span);
            actualResult.ShouldAllBeEquivalentTo(expectedResult);
        }
    }
}