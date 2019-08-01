using System.ComponentModel.Composition;
using Balakin.VSOutputEnhancer.Logic;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.UI.FormatDefinitions
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.PublishResultFailed)]
    [Name(ClassificationType.PublishResultFailed)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    public sealed class PublishResultFailedFormatDefinition : StyledClassificationFormatDefinition
    {
        [ImportingConstructor]
        public PublishResultFailedFormatDefinition(IStyleManager styleManager) : base(styleManager)
        {
        }
    }
}