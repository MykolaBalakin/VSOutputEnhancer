using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Classifiers
{
    public interface IClassifierFactory
    {
        IClassifier GetClassifierForContentType(IContentType contentType);
    }
}