using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.PublishResultSucceeded)]
    [Name(ClassificationType.PublishResultSucceeded)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class PublishResultSucceededClassifierFormat : StyledClassificationFormatDefinition {
        [ImportingConstructor]
        public PublishResultSucceededClassifierFormat(IStyleManager styleManager) : base(styleManager) { }
    }
}