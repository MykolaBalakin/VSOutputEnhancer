using System.Collections.Generic;
using Balakin.VSOutputEnhancer.Logic.Events;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Logic.Classifiers
{
    public abstract class ParserBasedSpanClassifier<TParsedData> : ISpanClassifier
        where TParsedData : ParsedData
    {
        private static readonly IEnumerable<ProcessedParsedData> Empty = new ProcessedParsedData[0];

        private readonly IParser<TParsedData> _parser;

        public ParserBasedSpanClassifier(IParser<TParsedData> parser)
        {
            _parser = parser;
        }

        public abstract IEnumerable<string> ContentTypes { get; }

        protected abstract IEnumerable<ProcessedParsedData> Classify(SnapshotSpan span, TParsedData parsedData);

        public IEnumerable<ProcessedParsedData> Classify(SnapshotSpan span, IDispatcher dispatcher)
        {
            if (!_parser.TryParse(span, out var parsedData))
            {
                return Empty;
            }

            dispatcher.Dispatch(new SpanParsedEvent<TParsedData>(parsedData));

            return Classify(span, parsedData);
        }
    }
}