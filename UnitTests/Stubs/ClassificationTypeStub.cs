using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.UnitTests.Stubs {
    [ExcludeFromCodeCoverage]
    internal class ClassificationTypeStub : IClassificationType {
        private readonly String type;

        public ClassificationTypeStub(String type) {
            this.type = type;
        }

        public Boolean IsOfType(String type) {
            throw new NotImplementedException();
        }

        public String Classification => type;

        public IEnumerable<IClassificationType> BaseTypes {
            get { throw new NotImplementedException(); }
        }
    }
}
