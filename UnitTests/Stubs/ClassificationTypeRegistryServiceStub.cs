using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.UnitTests.Stubs {
    [ExcludeFromCodeCoverage]
    internal class ClassificationTypeRegistryServiceStub : IClassificationTypeRegistryService {
        public IClassificationType GetClassificationType(String type) {
            return new ClassificationTypeStub(type);
        }

        public IClassificationType CreateClassificationType(String type, IEnumerable<IClassificationType> baseTypes) {
            throw new NotImplementedException();
        }

        public IClassificationType CreateTransientClassificationType(IEnumerable<IClassificationType> baseTypes) {
            throw new NotImplementedException();
        }

        public IClassificationType CreateTransientClassificationType(params IClassificationType[] baseTypes) {
            throw new NotImplementedException();
        }
    }
}
