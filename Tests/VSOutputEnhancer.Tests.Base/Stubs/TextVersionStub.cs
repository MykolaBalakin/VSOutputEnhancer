using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Text;

namespace Balakin.VSOutputEnhancer.Tests.Base.Stubs
{
    [ExcludeFromCodeCoverage]
    public class TextVersionStub : ITextVersion
    {
        public TextVersionStub(ITextBuffer textBuffer)
        {
            TextBuffer = textBuffer;
            VersionNumber = 0;
        }

        public ITrackingPoint CreateTrackingPoint(Int32 position, PointTrackingMode trackingMode)
        {
            throw new NotImplementedException();
        }

        public ITrackingPoint CreateTrackingPoint(Int32 position, PointTrackingMode trackingMode, TrackingFidelityMode trackingFidelity)
        {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateTrackingSpan(Span span, SpanTrackingMode trackingMode)
        {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateTrackingSpan(Span span, SpanTrackingMode trackingMode, TrackingFidelityMode trackingFidelity)
        {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateTrackingSpan(Int32 start, Int32 length, SpanTrackingMode trackingMode)
        {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateTrackingSpan(Int32 start, Int32 length, SpanTrackingMode trackingMode, TrackingFidelityMode trackingFidelity)
        {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateCustomTrackingSpan(Span span, TrackingFidelityMode trackingFidelity, Object customState, CustomTrackToVersion behavior)
        {
            throw new NotImplementedException();
        }

        public ITextVersion Next
        {
            get { throw new NotImplementedException(); }
        }

        public Int32 Length
        {
            get { throw new NotImplementedException(); }
        }

        public INormalizedTextChangeCollection Changes
        {
            get { throw new NotImplementedException(); }
        }

        public ITextBuffer TextBuffer { get; }
        public Int32 VersionNumber { get; }

        public Int32 ReiteratedVersionNumber
        {
            get { throw new NotImplementedException(); }
        }
    }
}