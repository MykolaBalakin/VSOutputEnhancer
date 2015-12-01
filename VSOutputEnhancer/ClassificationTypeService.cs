using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer {
    [Export(typeof(IClassificationTypeService))]
    internal class ClassificationTypeService : IClassificationTypeService {
        private readonly IClassificationTypeRegistryService classificationTypeRegistryService;
        private readonly ConcurrentDictionary<String, IClassificationType> classificationTypes;

        [ImportingConstructor]
        public ClassificationTypeService(IClassificationTypeRegistryService classificationTypeRegistryService) {
            this.classificationTypeRegistryService = classificationTypeRegistryService;
            classificationTypes=new ConcurrentDictionary<String, IClassificationType>();
        }

        public IClassificationType GetClassificationType(String name) {
            return classificationTypes.GetOrAdd(name, classificationTypeRegistryService.GetClassificationType);
        }
    }
}
