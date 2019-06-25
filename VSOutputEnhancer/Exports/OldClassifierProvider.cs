using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Classifiers;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Balakin.VSOutputEnhancer.Exports
{
    [Export(typeof(OldClassifierProvider))]
    public class OldClassifierProvider : IClassifierProvider
    {
        private readonly IClassifierFactory classifierFactory;

        [ImportingConstructor]
        public OldClassifierProvider(IClassifierFactory classifierFactory)
        {
            this.classifierFactory = classifierFactory;
        }

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            return classifierFactory.GetClassifierForContentType(buffer.ContentType);
        }
    }
}