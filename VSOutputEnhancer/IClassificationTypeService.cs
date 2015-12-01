using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer {
    internal interface IClassificationTypeService {
        IClassificationType GetClassificationType(System.String name);
    }
}