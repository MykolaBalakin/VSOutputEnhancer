using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Events
{
    public class ClassificationChangedEvent : IEvent
    {
        public SnapshotSpan Span { get; }

        public ClassificationChangedEvent(SnapshotSpan span)
        {
            Span = span;
        }
    }
}