using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.UnitTests.Stubs {
    internal class TextSnapshotStub : ITextSnapshot {
        public TextSnapshotStub(String text) {
            this.text = text;
        }

        private readonly String text;

        #region ITextSnapshot
        public String GetText(Span span) {
            return GetText(span.Start, span.Length);
        }

        public String GetText(Int32 startIndex, Int32 length) {
            return text.Substring(startIndex, length);
        }

        public String GetText() {
            return text;
        }

        public Char[] ToCharArray(Int32 startIndex, Int32 length) {
            throw new NotImplementedException();
        }

        public void CopyTo(Int32 sourceIndex, Char[] destination, Int32 destinationIndex, Int32 count) {
            throw new NotImplementedException();
        }

        public ITrackingPoint CreateTrackingPoint(Int32 position, PointTrackingMode trackingMode) {
            throw new NotImplementedException();
        }

        public ITrackingPoint CreateTrackingPoint(Int32 position, PointTrackingMode trackingMode, TrackingFidelityMode trackingFidelity) {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateTrackingSpan(Span span, SpanTrackingMode trackingMode) {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateTrackingSpan(Span span, SpanTrackingMode trackingMode, TrackingFidelityMode trackingFidelity) {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateTrackingSpan(Int32 start, Int32 length, SpanTrackingMode trackingMode) {
            throw new NotImplementedException();
        }

        public ITrackingSpan CreateTrackingSpan(Int32 start, Int32 length, SpanTrackingMode trackingMode, TrackingFidelityMode trackingFidelity) {
            throw new NotImplementedException();
        }

        public ITextSnapshotLine GetLineFromLineNumber(Int32 lineNumber) {
            throw new NotImplementedException();
        }

        public ITextSnapshotLine GetLineFromPosition(Int32 position) {
            throw new NotImplementedException();
        }

        public Int32 GetLineNumberFromPosition(Int32 position) {
            throw new NotImplementedException();
        }

        public void Write(TextWriter writer, Span span) {
            throw new NotImplementedException();
        }

        public void Write(TextWriter writer) {
            throw new NotImplementedException();
        }

        public ITextBuffer TextBuffer {
            get {
                throw new NotImplementedException();
            }
        }

        public IContentType ContentType {
            get {
                throw new NotImplementedException();
            }
        }

        public ITextVersion Version {
            get {
                throw new NotImplementedException();
            }
        }

        public Int32 Length {
            get { return text.Length; }
        }

        public Int32 LineCount {
            get {
                throw new NotImplementedException();
            }
        }

        public Char this[Int32 position] {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<ITextSnapshotLine> Lines {
            get {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
