using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.UnitTests.Stubs {
    [ExcludeFromCodeCoverage]
    internal class ContentTypeStub : IContentType {
        public ContentTypeStub(String typeName) {
            TypeName = typeName;
            DisplayName = $"Display name: {typeName}";
            BaseTypes = new List<IContentType>().AsReadOnly();
        }
        public Boolean IsOfType(String type) {
            return String.Equals(type, TypeName, StringComparison.OrdinalIgnoreCase);
        }

        public String TypeName { get; }
        public String DisplayName { get; }
        public IEnumerable<IContentType> BaseTypes { get; }
    }
}
