using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Tests.Base.Stubs
{
    [ExcludeFromCodeCoverage]
    public class ContentTypeStub : IContentType
    {
        public ContentTypeStub(String typeName)
        {
            TypeName = typeName;
            DisplayName = $"Display name: {typeName}";
            BaseTypes = new List<IContentType>().AsReadOnly();
        }

        public Boolean IsOfType(String type)
        {
            return String.Equals(type, TypeName, StringComparison.OrdinalIgnoreCase);
        }

        public String TypeName { get; }
        public String DisplayName { get; }
        public IEnumerable<IContentType> BaseTypes { get; }
    }
}