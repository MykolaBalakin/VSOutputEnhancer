using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Balakin.VSOutputEnhancer.Exports.Formats {
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = ClassificationType.NpmResultFailed)]
    [Name(ClassificationType.NpmResultFailed)]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class NpmResultFailedFormatDefinition : StyledClassificationFormatDefinition {
        [ImportingConstructor]
        public NpmResultFailedFormatDefinition(IStyleManager styleManager) : base(styleManager) { }
    }
}
