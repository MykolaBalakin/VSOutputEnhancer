using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balakin.VSOutputEnhancer.Classifiers;
using Balakin.VSOutputEnhancer.Parsers;
using Balakin.VSOutputEnhancer.UnitTests.Stubs;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.UnitTests {
    [ExcludeFromCodeCoverage]
    internal static class Utils {
        public static SnapshotSpan CreateSpan(String text) {
            var snapshot = new TextSnapshotStub(text);
            return new SnapshotSpan(snapshot, new Span(0, snapshot.Length));
        }

        public static ITextBuffer CreateTextBuffer(String contentType) {
            return new TextBufferStub(contentType);
        }

        public static IClassificationTypeRegistryService CreateClassificationTypeRegistryService() {
            return new ClassificationTypeRegistryServiceStub();
        }

        public static ClassifierFactory CreateClassifierFactory() {
            return new ClassifierFactory(CreateClassificationTypeRegistryService());
        }

        public static IClassifier CreateBuildOutputClassifier() {
            var classificationTypeRegistryService = Utils.CreateClassificationTypeRegistryService();
            var classifier = new BuildOutputClassifier(classificationTypeRegistryService);
            return classifier;
        }

        public static IClassifier CreateDebugClassifier() {
            var classificationTypeRegistryService = Utils.CreateClassificationTypeRegistryService();
            var classifier = new DebugClassifier(classificationTypeRegistryService);
            return classifier;
        }

        public static IClassifier CreateParserBasedClassifier<T>(IParser<T> parser, IParsedDataProcessor<T> processor)
            where T : ParsedData {
            return new ParserBasedClassifier<T>(parser, processor);
        }
    }
}
