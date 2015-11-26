using System;
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

        [ImportingConstructor]
        public ClassificationTypeService(IClassificationTypeRegistryService classificationTypeRegistryService) {
            this.classificationTypeRegistryService = classificationTypeRegistryService;
        }

        public IClassificationType GetClassificationType(String name) {
            // TODO: Add cache
            return classificationTypeRegistryService.GetClassificationType(name);
        }
    }
}
