using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Events
{
    public class SpanParsedEvent<TParsedData> : IEvent
    {
        public TParsedData ParsedData { get; }
        public SnapshotSpan Span { get; }

        public SpanParsedEvent(SnapshotSpan span, TParsedData parsedData)
        {
            Span = span;
            ParsedData = parsedData;
        }
    }
}