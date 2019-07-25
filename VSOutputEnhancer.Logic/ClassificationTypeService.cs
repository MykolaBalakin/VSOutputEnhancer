using System;
using System.Collections.Concurrent;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Logic
{
    [Export(typeof(IClassificationTypeService))]
    public class ClassificationTypeService : IClassificationTypeService
    {
        private readonly IClassificationTypeRegistryService classificationTypeRegistryService;
        private readonly ConcurrentDictionary<String, IClassificationType> classificationTypes;

        [ImportingConstructor]
        public ClassificationTypeService(IClassificationTypeRegistryService classificationTypeRegistryService)
        {
            this.classificationTypeRegistryService = classificationTypeRegistryService;
            classificationTypes = new ConcurrentDictionary<String, IClassificationType>();
        }

        public IClassificationType GetClassificationType(String name)
        {
            return classificationTypes.GetOrAdd(name, classificationTypeRegistryService.GetClassificationType);
        }
    }
}