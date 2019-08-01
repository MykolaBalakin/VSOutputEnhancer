using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.UI.FormatDefinitions
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.PublishResultSucceeded)]
    [Name(ClassificationType.PublishResultSucceeded)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    public sealed class PublishResultSucceededFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public PublishResultSucceededFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}