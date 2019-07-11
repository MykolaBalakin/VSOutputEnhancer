using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Tests.Stubs
{
    [Export(typeof(IClassificationTypeRegistryService))]
    [ExcludeFromCodeCoverage]
    public class ClassificationTypeRegistryServiceStub : IClassificationTypeRegistryService
    {
        public IClassificationType GetClassificationType(String type)
        {
            return new ClassificationTypeStub(type);
        }

        public IClassificationType CreateClassificationType(String type, IEnumerable<IClassificationType> baseTypes)
        {
            throw new NotImplementedException();
        }

        public IClassificationType CreateTransientClassificationType(IEnumerable<IClassificationType> baseTypes)
        {
            throw new NotImplementedException();
        }

        public IClassificationType CreateTransientClassificationType(params IClassificationType[] baseTypes)
        {
            throw new NotImplementedException();
        }
    }
}