using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.NpmMessageError)]
    [Name(ClassificationType.NpmMessageError)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class NpmMessageErrorFormatDefinition : StyledClassificationFormatDefinition {
        [ImportingConstructor]
        public NpmMessageErrorFormatDefinition(IStyleManager styleManager) : base(styleManager) { }
    }
}
