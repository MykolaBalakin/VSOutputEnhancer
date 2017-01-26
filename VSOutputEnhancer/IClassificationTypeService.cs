using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer
{
    public interface IClassificationTypeService
    {
        IClassificationType GetClassificationType(System.String name);
    }
}