using System.Collections.Generic;
using Balakin.VSOutputEnhancer.Logic.Events;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers
{
    public abstract class ParserBasedSpanClassifier<TParsedData> : ISpanClassifier
        where TParsedData : ParsedData
    {
        protected static readonly IEnumerable<ProcessedParsedData> EmptyClassification = new ProcessedParsedData[0];

        private readonly IParser<TParsedData> _parser;

        public ParserBasedSpanClassifier(IParser<TParsedData> parser)
        {
            _parser = parser;
        }

        public abstract IEnumerable<string> ContentTypes { get; }

        protected abstract IEnumerable<ProcessedParsedData> Classify(SnapshotSpan span, TParsedData parsedData, DataContainer data);

        public IEnumerable<ProcessedParsedData> Classify(SnapshotSpan span, IDispatcher dispatcher, DataContainer data)
        {
            if (!_parser.TryParse(span, out var parsedData))
            {
                return EmptyClassification;
            }

            dispatcher.Dispatch(new SpanParsedEvent<TParsedData>(span, parsedData), data);

            return Classify(span, parsedData, data);
        }
    }
}