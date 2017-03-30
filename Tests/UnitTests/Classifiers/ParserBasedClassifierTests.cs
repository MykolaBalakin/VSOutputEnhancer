using System.Diagnostics.CodeAnalysis;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.Tests.Stubs;
using FluentAssertions;
using Microsoft.VisualStudio.Text.Classification;
using NSubstitute;
using Xunit;

namespace Balakin.VSOutputEnhancer.Tests.UnitTests.Classifiers
{
    [ExcludeFromCodeCoverage]
    public class ParserBasedClassifierTests
    {
        [Fact]
        public void NotParsed()
        {
            var span = Utils.CreateSpan("");

            var parser = Substitute.For<IParser<ParsedData>>();
            ParsedData outParameter;
            parser.TryParse(span, out outParameter)
                .Returns(ci =>
                {
                    ci[1] = new ParsedData();
                    return false;
                });

            var processor = Substitute.For<IParsedDataProcessor<ParsedData>>();
            processor.ProcessData(span, Arg.Any<ParsedData>())
                .Returns(new[]
                {
                    new ProcessedParsedData(span, "TestClassification"),
                });

            var classifier = Utils.CreateParserBasedClassifier(parser, processor);

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

            var parser = Substitute.For<IParser<ParsedData>>();
            ParsedData outParameter;
            parser.TryParse(span, out outParameter)
                .Returns(ci =>
                {
                    ci[1] = new ParsedData();
                    return true;
                });

            var processor = Substitute.For<IParsedDataProcessor<ParsedData>>();
            processor.ProcessData(span, Arg.Any<ParsedData>())
                .Returns(new[]
                {
                    new ProcessedParsedData(span, "TestClassification"),
                    new ProcessedParsedData(span, "TestClassification2"),
                });

            var classifier = Utils.CreateParserBasedClassifier(parser, processor);

            var actualResult = classifier.GetClassificationSpans(span);
            actualResult.ShouldAllBeEquivalentTo(expectedResult);
        }
    }
}