using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Tests.Stubs {
    [ExcludeFromCodeCoverage]
    public class ClassificationTypeStub : IClassificationType {
        private readonly String type;

        public ClassificationTypeStub(String type) {
            this.type = type;
        }

        public Boolean IsOfType(String type) {
            return this.type.Equals(type, StringComparison.OrdinalIgnoreCase);
        }

        public String Classification => type;

        public IEnumerable<IClassificationType> BaseTypes
        {
            get { throw new NotImplementedException(); }
        }
    }
}
