using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Tests.Stubs
{
    [ExcludeFromCodeCoverage]
    public class TextBufferStub : ITextBuffer
    {
        public TextBufferStub(String contentType)
        {
            ContentType = new ContentTypeStub(contentType);
            Properties = new PropertyCollection();
        }

        public PropertyCollection Properties { get; }

        public ITextEdit CreateEdit(EditOptions options, Int32? reiteratedVersionNumber, Object editTag)
        {
            throw new NotImplementedException();
        }

        public ITextEdit CreateEdit()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyRegionEdit CreateReadOnlyRegionEdit()
        {
            throw new NotImplementedException();
        }

        public void TakeThreadOwnership()
        {
            throw new NotImplementedException();
        }

        public Boolean CheckEditAccess()
        {
            throw new NotImplementedException();
        }

        public void ChangeContentType(IContentType newContentType, Object editTag)
        {
            throw new NotImplementedException();
        }

        public ITextSnapshot Insert(Int32 position, String text)
        {
            throw new NotImplementedException();
        }

        public ITextSnapshot Delete(Span deleteSpan)
        {
            throw new NotImplementedException();
        }

        public ITextSnapshot Replace(Span replaceSpan, String replaceWith)
        {
            throw new NotImplementedException();
        }

        public Boolean IsReadOnly(Int32 position)
        {
            throw new NotImplementedException();
        }

        public Boolean IsReadOnly(Int32 position, Boolean isEdit)
        {
            throw new NotImplementedException();
        }

        public Boolean IsReadOnly(Span span)
        {
            throw new NotImplementedException();
        }

        public Boolean IsReadOnly(Span span, Boolean isEdit)
        {
            throw new NotImplementedException();
        }

        public NormalizedSpanCollection GetReadOnlyExtents(Span span)
        {
            throw new NotImplementedException();
        }

        public IContentType ContentType { get; }

        public ITextSnapshot CurrentSnapshot
        {
            get { throw new NotImplementedException(); }
        }

        public Boolean EditInProgress
        {
            get { throw new NotImplementedException(); }
        }

#pragma warning disable CS0067
        public event EventHandler<SnapshotSpanEventArgs> ReadOnlyRegionsChanged;
        public event EventHandler<TextContentChangedEventArgs> Changed;
        public event EventHandler<TextContentChangedEventArgs> ChangedLowPriority;
        public event EventHandler<TextContentChangedEventArgs> ChangedHighPriority;
        public event EventHandler<TextContentChangingEventArgs> Changing;
        public event EventHandler PostChanged;
        public event EventHandler<ContentTypeChangedEventArgs> ContentTypeChanged;
#pragma warning restore CS0067
    }
}