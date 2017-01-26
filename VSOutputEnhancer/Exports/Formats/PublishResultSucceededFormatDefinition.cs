using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.PublishResultSucceeded)]
    [Name(ClassificationType.PublishResultSucceeded)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class PublishResultSucceededFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public PublishResultSucceededFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}