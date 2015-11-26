using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.PublishResultFailed)]
    [Name(ClassificationType.PublishResultFailed)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class PublishResultFailedFormatDefinition : StyledClassificationFormatDefinition {
        [ImportingConstructor]
        public PublishResultFailedFormatDefinition(IStyleManager styleManager) : base(styleManager) { }
    }
}